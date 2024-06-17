using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class StateMasterViewModel : IViewModel
    {
        public static StateMasterViewModel CreateViewModel(StateModelOperation operation, IErrorInformer informer)
        {
            return new StateMasterViewModel(operation, informer);
        }


        public ICommand SwitchToUserMasterPage { get; set; }
        public ICommand SwitchToBookMasterPage { get; set; }
        public ICommand SwitchToEventMasterPage { get; set; }
        public ICommand CreateState { get; set; }
        public ICommand RemoveState { get; set; }

        private readonly StateModelOperation _modelOperation;
        private readonly IErrorInformer _informer;

        private ObservableCollection<StateDetailViewModel> _states;
        public ObservableCollection<StateDetailViewModel> States
        {
            get => _states;
            set
            {
                _states = value;
                OnPropertyChanged(nameof(States));
            }
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _bookId;
        public string BookId
        {
            get => _bookId;
            set
            {
                _bookId = value;
                OnPropertyChanged(nameof(BookId));
            }
        }

        private bool _availability;
        public bool Availability
        {
            get => _availability;
            set
            {
                _availability = value;
                OnPropertyChanged(nameof(Availability));
            }
        }

        private bool _isStateSelected;
        public bool IsStateSelected
        {
            get => _isStateSelected;
            set
            {
                this.IsStateDetailVisible = value ? Visibility.Visible : Visibility.Hidden;
                _isStateSelected = value;
                OnPropertyChanged(nameof(IsStateSelected));
            }
        }

        private Visibility _isStateDetailVisible;
        public Visibility IsStateDetailVisible
        {
            get => _isStateDetailVisible;
            set
            {
                _isStateDetailVisible = value;
                OnPropertyChanged(nameof(IsStateDetailVisible));
            }
        }

        private StateDetailViewModel _selectedDetailViewModel;
        public StateDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsStateSelected = true;
                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public StateMasterViewModel(StateModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            this.SwitchToBookMasterPage = new SwitchViewCommand("BookMasterView");
            this.SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            this.CreateState = new OnClickCommand(e => this.StoreState(), c => this.CanStoreState());
            this.RemoveState = new OnClickCommand(e => this.DeleteState());

            this.States = new ObservableCollection<StateDetailViewModel>();

            this._modelOperation = model ?? StateModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();

            this.IsStateSelected = false;

            Task.Run(this.LoadStates);
        }

        private bool CanStoreState()
        {
            return !string.IsNullOrWhiteSpace(this.BookId);
        }

        private void StoreState()
        {
            Task.Run(async () =>
            {
                try
                {
                    string newId = Guid.NewGuid().ToString();

                    await this._modelOperation.AddAsync(newId, this.BookId, this.Availability);

                    this.LoadStates();

                    this._informer.InformSuccess("State successfully created!");
                }
                catch (Exception e)
                {
                    this._informer.InformError(e.Message);
                }
            });
        }

        private void DeleteState()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Id);

                    this.LoadStates();

                    this._informer.InformSuccess("State successfully deleted!");
                }
                catch (Exception e)
                {
                    this._informer.InformError("Error while deleting state! Remember to remove all associated events!");
                }
            });
        }

        private async void LoadStates()
        {
            Dictionary<string, StateModel> states = await this._modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._states.Clear();

                foreach (StateModel s in states.Values)
                {
                    this._states.Add(new StateDetailViewModel(s.Id, s.BookId, s.Date, s.Availability));
                }
            });

            OnPropertyChanged(nameof(States));
        }
    }
}
