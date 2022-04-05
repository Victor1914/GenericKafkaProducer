namespace GenericKafkaProducer.Interfaces.DataProviders
{
    using System.Collections.Generic;

    public interface IMocker<TContract>
        where TContract : class
    {
        TContract CreateMock();

        List<TContract> CreateMocks(int count);
    }
}