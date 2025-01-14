namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

        // custom exception
        public NotFoundException(string name, object key) : base($"Entiry \"{name}\" ({key}) was not found.")
        {

        }
    }
}
