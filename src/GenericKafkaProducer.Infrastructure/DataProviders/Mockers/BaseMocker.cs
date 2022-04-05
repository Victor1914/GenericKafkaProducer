namespace GenericKafkaProducer.Infrastructure.DataProviders.Mockers
{
    using System.Collections.Generic;
    using Bogus;
    using Interfaces.DataProviders;

    public abstract class BaseMocker<TContract> : IMocker<TContract>
        where TContract : class
    {
        protected Faker<TContract> Faker = new Faker<TContract>();

        public virtual TContract CreateMock() => Faker.Generate();

        public virtual List<TContract> CreateMocks(int count) => Faker.Generate(count);
    }
}