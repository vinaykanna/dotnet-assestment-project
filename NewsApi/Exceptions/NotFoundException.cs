namespace NewsApi.Exceptions;

public class NotFoundException : Exception
{
    public string ResourceName { get; }
    public int ResourceId { get; }

    public NotFoundException(string resourceName, int resourceId) : base($"{resourceName} with id {resourceId} not found")
    {
        ResourceName = resourceName;
        ResourceId = resourceId;
    }
}