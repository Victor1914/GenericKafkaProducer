namespace GenericKafkaProducer.Infrastructure.Core
{
    using System;
    using System.Collections.Generic;

    public abstract class BaseFactory<TEntity>
        where TEntity : class
    {
        private readonly Dictionary<string, TEntity> _entities = new Dictionary<string, TEntity>(StringComparer.OrdinalIgnoreCase);

        protected BaseFactory(IEnumerable<TEntity> entities)
        {
            PrepareEntities(entities);
        }

        protected TEntity GetEntity<TContract>(string entityType)
            where TContract : class
        {
            return GetEntity(typeof(TContract).Name, entityType);
        }

        protected TEntity GetEntity(string contract, string entityType)
        {
            var key = $"{entityType}_{contract}";

            return _entities.TryGetValue(key, out var entity)
                ? entity
                : default;
        }

        private void PrepareEntities(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                var mainType = entity.GetType();

                var entityType = mainType.Name.Contains("`")
                    ? mainType.Name.Split("`")[0]
                    : mainType.Name;

                var genericType = mainType.IsGenericType
                    ? mainType.GetGenericArguments()[0].Name
                    : string.Empty;

                _entities.Add($"{entityType}_{genericType}", entity);
            }
        }
    }
}
