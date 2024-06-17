using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class UserMasterViewModel : IViewModel
    {
        public static UserMasterViewModel CreateViewModel(UserModelOperation operation, IErrorInformer informer)
        {
            return new UserMasterViewModel(operation, informer);
        }

        public ICommand SwitchToBookMasterPage { get; set; }
        public ICommand SwitchToStateMasterPage { get; set; }
        public ICommand SwitchToEventMasterPage { get; set; }
        public ICommand CreateUser { get; set; }
        public ICommand RemoveUser { get; set; }

        private readonly UserModelOperation _modelOperation;
        private readonly IErrorInformer _informer;

        private ObservableCollection<UserDetailViewModel> _users;
        public ObservableCollection<UserDetailViewModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
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

        private bool _isUserSelected;
        public bool IsUserSelected
        {
            get => _isUserSelected;
            set
            {
                IsUserDetailVisible = value ? Visibility.Visible : Visibility.Hidden;
                _isUserSelected = value;
                OnPropertyChanged(nameof(IsUserSelected));
            }
        }

        private Visibility _isUserDetailVisible;
        public Visibility IsUserDetailVisible
        {
            get => _isUserDetailVisible;
            set
            {
                _isUserDetailVisible = value;
                OnPropertyChanged(nameof(IsUserDetailVisible));
            }
        }

        private UserDetailViewModel _selectedDetailViewModel;
        public UserDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                IsUserSelected = true;
                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public UserMasterViewModel(UserModelOperation? model = null, IErrorInformer? informer = null)
        {
            SwitchToBookMasterPage = new SwitchViewCommand("BookMasterView");
            SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            CreateUser = new OnClickCommand(e => StoreUser(), c => CanStoreUser());
            RemoveUser = new OnClickCommand(e => DeleteUser());

            Users = new ObservableCollection<UserDetailViewModel>();

            _modelOperation = model ?? UserModelOperation.CreateModelOperation();
            _informer = informer ?? new PopupErrorInformer();

            IsUserSelected = false;

            Task.Run(LoadUsers);
        }

        private bool CanStoreUser()
        {
            return !(
                string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Phone)
            );
        }

        private void StoreUser()
        {
            Task.Run(async () =>
            {
                string newId = Guid.NewGuid().ToString();

                await _modelOperation.AddAsync(newId, Email, Phone, Name);

                _informer.InformSuccess("User successfully created!");

                LoadUsers();
            });
        }

        private void DeleteUser()
        {
            Task.Run(async () =>
            {
                try
                {
                    await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                    _informer.InformSuccess("User successfully deleted!");

                    LoadUsers();
                }
                catch (Exception e)
                {
                    _informer.InformError("Error while deleting user! Remember to remove all associated events!");
                }
            });
        }

        private async void LoadUsers()
        {
            Dictionary<string, UserModel> users = await _modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                _users.Clear();

                foreach (UserModel u in users.Values)
                {
                    _users.Add(new UserDetailViewModel(u.Id, u.Name, u.Email, u.Phone));
                }
            });

            OnPropertyChanged(nameof(Users));
        }
    }
}
