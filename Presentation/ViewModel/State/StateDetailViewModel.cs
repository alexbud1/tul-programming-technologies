using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class StateDetailViewModel : IViewModel
    {
        public static StateDetailViewModel CreateViewModel(string id, string bookId, bool availability, DateTime date,
        StateModelOperation model, IErrorInformer informer)
        {
            return new StateDetailViewModel(id, bookId, date, availability, model, informer);
        }


        public ICommand UpdateState { get; set; }

        private readonly StateModelOperation _modelOperation;

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

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
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

        public StateDetailViewModel(StateModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? StateModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        public StateDetailViewModel(string id, string bookId, DateTime date, bool availability, StateModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.Id = id;
            this.BookId = bookId;
            this.Date = date;
            this.Availability = availability;

            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? StateModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Id, this.BookId, this.Availability);

                this._informer.InformSuccess("State successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !string.IsNullOrWhiteSpace(this.BookId);
        }
    }
}
