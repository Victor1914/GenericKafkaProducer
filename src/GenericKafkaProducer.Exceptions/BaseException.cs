namespace GenericKafkaProducer.Exceptions
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using Models.Errors;
    using Models.Responses;

    public abstract class BaseException : Exception
    {
        protected BaseException(Exception systemException)
        {
            ExtractExceptionData();
            SystemException = systemException;
        }

        public string ClassName { get; private set; }

        public string MethodName { get; private set; }

        public int LineNumber { get; private set; }

        public Exception SystemException { get; }

        public virtual ProducerResponse CreateResponse()
        {
            return new ProducerResponse
            {
                SystemError = new SystemError
                {
                    ClassName = ClassName,
                    MethodName = MethodName,
                    LineNumber = LineNumber,
                    SystemException = SystemException
                }
            };
        }

        private void ExtractExceptionData()
        {
            var stackTrace = new StackTrace(true);
            var exceptionType = GetType();

            var isEntryPoint = false;

            for (var index = 0; index <= stackTrace.FrameCount; index++)
            {
                var frame = stackTrace.GetFrame(index);
                var method = frame.GetMethod();

                if (isEntryPoint)
                {
                    LineNumber = frame.GetFileLineNumber();

                    if (ExtractAsyncMethodData(method))
                        break;

                    ExtractMethodData(method);
                    break;
                }

                if (method.DeclaringType == exceptionType)
                    isEntryPoint = true;
            }
        }

        private void ExtractMethodData(MemberInfo method)
        {
            var className = method.DeclaringType?.Name ?? "";
            ClassName = ExtractClassName(className);
            MethodName = method.Name;
        }

        private bool ExtractAsyncMethodData(MemberInfo method)
        {
            var methodType = method.DeclaringType;

            var isMethodAsync = methodType?.GetInterface("IAsyncStateMachine") != null;
            if (!isMethodAsync)
                return false;

            var className = methodType.ReflectedType?.Name;
            ClassName = ExtractClassName(className);
            MethodName = methodType.FullName?.Split('<', '>')[1];

            return true;
        }

        private static string ExtractClassName(string rawClassName)
        {
            return rawClassName.Contains("`")
                ? rawClassName.Split("`")[0]
                : rawClassName;
        }
    }
}