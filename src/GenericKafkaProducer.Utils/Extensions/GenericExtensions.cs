namespace GenericKafkaProducer.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public static class GenericExtensions
    {
        public static IEnumerable<TItem> EmptyIfNull<TItem>(this IEnumerable<TItem> enumerable) => enumerable ?? Enumerable.Empty<TItem>();

        public static bool IsNullOrEmpty<TItem>(this IEnumerable<TItem> enumerable) => enumerable == null || !enumerable.Any();

        public static Task AsyncParallelForEach<TItem>(
            this IEnumerable<TItem> source, 
            Func<TItem, Task> body, 
            int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, 
            TaskScheduler scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism };

            if (scheduler != null)
                options.TaskScheduler = scheduler;

            var block = new ActionBlock<TItem>(body, options);

            foreach (var item in source)
                block.Post(item);

            block.Complete();
            return block.Completion;
        }
    }
}