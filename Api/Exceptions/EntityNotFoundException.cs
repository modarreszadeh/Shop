namespace Api.Exceptions;

public class EntityNotFoundException<T>(params object[] ids)
    : Exception($"Cannot found data in '{typeof(T)}' with this ids: ({string.Join(", ", ids)})");