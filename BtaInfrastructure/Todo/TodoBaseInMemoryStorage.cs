using BtaDomain.ToDo;

namespace BtaInfrastructure.Todo
{
    internal class TodoInMemoryStorage
    {
        private static readonly TodoInMemoryStorage _instance = new ();
        private readonly IList<TodoItem> _list;

        static TodoInMemoryStorage()
        { }

        private TodoInMemoryStorage()
        {
            _list = new List<TodoItem>(0);
        }

        public static TodoInMemoryStorage Instance => _instance;

        public IList<TodoItem> List => _list;
    }
}