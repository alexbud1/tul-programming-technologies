using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class UserDetailViewModel : IViewModel
    {
        public static UserDetailViewModel CreateViewModel(string id, string email, string phone, string name, UserModelOperation model, IErrorInformer informer)
        {
            return new UserDetailViewModel(id, email, phone, name, model, informer);
        }


        public ICommand UpdateUser { get; set; }

        private readonly UserModelOperation _modelOperation;
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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public UserDetailViewModel(UserModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? UserModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        public UserDetailViewModel(string id, string name, string email, string phone, UserModelOperation? model = null, IErrorInformer? informer = null)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;

            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? UserModelOperation.CreateModelOperation();
            this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Id, this.Email, this.Phone, this.Name);
                this._informer.InformSuccess("User successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Name) ||
                string.IsNullOrWhiteSpace(this.Email) ||
                string.IsNullOrWhiteSpace(this.Phone)
            );
        }
    }
}
