using System.Runtime.Serialization;

namespace Api.Tools;

public class EntityNotFoundException<TKey> : ApplicationException
    where TKey : IEquatable<TKey>
{
    public EntityNotFoundException(string entityName, TKey id)
        : this(entityName, id, $"The entity ({entityName}:{id}) has not been found")
    { }

    public EntityNotFoundException(string entityName, TKey id, string message)
        : this(entityName, id, message, innerException: null)
    { }

    public EntityNotFoundException(string entityName, TKey id, string message, Exception innerException)
        : base(message, innerException)
    {
        this.EntityName = entityName;
        this.Id = id;
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public string EntityName { get; private set; }
    public TKey Id { get; private set; }
}

public class EntityNotFoundException : EntityNotFoundException<int>        
{
    public EntityNotFoundException(string entityName, int id) : base(entityName, id) { }
}