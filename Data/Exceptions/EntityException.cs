namespace Data.Exceptions;

public class EntityException : Exception
{
    public EntityException()
    {
    }

    public EntityException(string message)
        : base(message)
    {
    }

    public EntityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}