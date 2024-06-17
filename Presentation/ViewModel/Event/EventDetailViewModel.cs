using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class EventDetailViewModel : IViewModel
    {
        public static EventDetailViewModel CreateViewModel(string id, string stateId, string customerID, string type, DateTime eventDate, EventModelOperation model, IErrorInformer informer)
        {
            return new EventDetailViewModel(id, stateId, customerID, type, eventDate, model, informer);
        }


        public ICommand UpdateEvent { get; set; }

        private readonly EventModelOperation _modelOperation;
        private readonly IErrorInformer _informer;

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

        public EventDetailViewModel(EventModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? EventModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        public EventDetailViewModel(string id, string stateId, string customerId, string type, DateTime eventDate, EventModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.Id = id;
            this.StateId = stateId;
            this.CustomerId = customerId;
            this.Type = type;
            this.EventDate = eventDate;

            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? EventModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Id, this.EventDate, this.StateId, this.CustomerId, this.Type);
                this._informer.InformSuccess("Event successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.StateId) ||
                string.IsNullOrWhiteSpace(this.CustomerId) ||
                string.IsNullOrWhiteSpace(this.Type)
            );
        }
    }
}
