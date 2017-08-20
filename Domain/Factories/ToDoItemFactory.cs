namespace Domain.Factories
{
    public class ToDoItemFactory
    {
        public static TodoItem CreateNew()
        {
            return new TodoItem()
            {
                IsComplete = false,
                Name = string.Empty
            };
        }
    }
}
