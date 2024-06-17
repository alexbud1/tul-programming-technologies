using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class EventMasterViewModel : IViewModel
    {

        public static EventMasterViewModel CreateViewModel(EventModelOperation operation, IErrorInformer informer)
        {
            return new EventMasterViewModel(operation, informer);
        }


        public ICommand SwitchToUserMasterPage { get; set; }
        public ICommand SwitchToBookMasterPage { get; set; }
        public ICommand SwitchToStateMasterPage { get; set; }
        public ICommand CreateEvent { get; set; }
        public ICommand RemoveEvent { get; set; }

        private readonly EventModelOperation _modelOperation;
        private readonly IErrorInformer _informer;

        private ObservableCollection<EventDetailViewModel> _events;
        public ObservableCollection<EventDetailViewModel> Events
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private string _stateId;
        public string StateId
        {
            get => _stateId;
            set
            {
                _stateId = value;
                OnPropertyChanged(nameof(StateId));
            }
        }

        private string _customerId;
        public string CustomerId
        {
            get => _customerId;
            set
            {
                _customerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private DateTime _eventDate;
        public DateTime EventDate
        {
            get => _eventDate;
            set
            {
                _eventDate = value;
                OnPropertyChanged(nameof(EventDate));
            }
        }

        private bool _isEventSelected;
        public bool IsEventSelected
        {
            get => _isEventSelected;
            set
            {
                IsEventDetailVisible = value ? Visibility.Visible : Visibility.Hidden;
                _isEventSelected = value;
                OnPropertyChanged(nameof(IsEventSelected));
            }
        }

        private Visibility _isEventDetailVisible;
        public Visibility IsEventDetailVisible
        {
            get => _isEventDetailVisible;
            set
            {
                _isEventDetailVisible = value;
                OnPropertyChanged(nameof(IsEventDetailVisible));
            }
        }

        private EventDetailViewModel _selectedDetailViewModel;
        public EventDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                IsEventSelected = true;
                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public EventMasterViewModel(EventModelOperation? model = null, IErrorInformer? informer = null)
        {
            SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            SwitchToBookMasterPage = new SwitchViewCommand("BookMasterView");
            SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");

            CreateEvent = new OnClickCommand(e => StoreEvent(), c => CanStoreEvent());
            RemoveEvent = new OnClickCommand(e => DeleteEvent());

            Events = new ObservableCollection<EventDetailViewModel>();

            _modelOperation = model ?? EventModelOperation.CreateModelOperation();
            _informer = informer ?? new PopupErrorInformer();

            IsEventSelected = false;

            Task.Run(LoadEvents);
        }

        private bool CanStoreEvent()
        {
            return !(
                string.IsNullOrWhiteSpace(StateId) ||
                string.IsNullOrWhiteSpace(CustomerId) ||
                string.IsNullOrWhiteSpace(Type)
            );
        }

        private void StoreEvent()
        {
            Task.Run(async () =>
            {
                string newId = Guid.NewGuid().ToString();

                await _modelOperation.AddAsync(newId, StateId, CustomerId, Type);

                _informer.InformSuccess("Event successfully created!");

                LoadEvents();
            });
        }

        private void DeleteEvent()
        {
            Task.Run(async () =>
            {
                try
                {
                    await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                    _informer.InformSuccess("Event successfully deleted!");

                    LoadEvents();
                }
                catch (Exception e)
                {
                    _informer.InformError("Error while deleting event! Please try again.");
                }
            });
        }

        private async void LoadEvents()
        {
            Dictionary<string, EventModel> events = await _modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                _events.Clear();

                foreach (EventModel ev in events.Values)
                {
                    _events.Add(new EventDetailViewModel(ev.Id, ev.StateId, ev.CustomerId, ev.Type, ev.EventDate));
                }
            });

            OnPropertyChanged(nameof(Events));
        }
    }
}
