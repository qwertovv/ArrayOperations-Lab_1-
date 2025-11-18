using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ArrayOperations
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ArrayOperation _selectedOperation;

        public ObservableCollection<ArrayOperation> Operations { get; }
        public OperationViewModel OperationViewModel { get; }

        public ArrayOperation SelectedOperation
        {
            get => _selectedOperation;
            set
            {
                _selectedOperation = value;
                OperationViewModel.CurrentOperation = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Operations = new ObservableCollection<ArrayOperation>();
            OperationViewModel = new OperationViewModel();

            Operations.Add(new SortOperation());
            Operations.Add(new MaxOperation());
            Operations.Add(new SumOperation());
            SelectedOperation = Operations.First();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}