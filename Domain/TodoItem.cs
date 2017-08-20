namespace Domain
{
    public class TodoItem : EntityBase
    {
        private string _name;
        private bool _isComplete;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool IsComplete  
        {
            get { return _isComplete; }
            set
            {
                _isComplete = value;
                OnPropertyChanged(nameof(IsComplete));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
