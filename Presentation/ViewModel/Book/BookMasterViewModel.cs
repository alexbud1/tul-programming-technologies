using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Model;

namespace Presentation.ViewModel;

public class BookMasterViewModel : IViewModel
{
    public static BookMasterViewModel CreateViewModel(BookModelOperation operation, IErrorInformer informer)
    {
        return new BookMasterViewModel(operation, informer);
    }


    public ICommand SwitchToUserMasterPage { get; set; }

    public ICommand SwitchToStateMasterPage { get; set; }

    public ICommand SwitchToEventMasterPage { get; set; }

    public ICommand CreateBook { get; set; }

    public ICommand RemoveBook { get; set; }

    private readonly BookModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private ObservableCollection<BookDetailViewModel> _books;

    public ObservableCollection<BookDetailViewModel> Books
    {
        get => _books;
        set
        {
            _books = value;
            OnPropertyChanged(nameof(Books));
        }
    }

    private string _title;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private string _author;

    public string Author
    {
        get => _author;
        set
        {
            _author = value;
            OnPropertyChanged(nameof(Author));
        }
    }

    private string _genre;

    public string Genre
    {
        get => _genre;
        set
        {
            _genre = value;
            OnPropertyChanged(nameof(Genre));
        }
    }

    private bool _isBookSelected;

    public bool IsBookSelected
    {
        get => _isBookSelected;
        set
        {
            IsBookDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

            _isBookSelected = value;
            OnPropertyChanged(nameof(IsBookSelected));
        }
    }

    private Visibility _isBookDetailVisible;

    public Visibility IsBookDetailVisible
    {
        get => _isBookDetailVisible;
        set
        {
            _isBookDetailVisible = value;
            OnPropertyChanged(nameof(IsBookDetailVisible));
        }
    }

    private BookDetailViewModel _selectedDetailViewModel;

    public BookDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            IsBookSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }

    public BookMasterViewModel(BookModelOperation? model = null, IErrorInformer? informer = null)
    {
        SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
        SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

        CreateBook = new OnClickCommand(e => StoreBook(), c => CanStoreBook());
        RemoveBook = new OnClickCommand(e => DeleteBook());

        Books = new ObservableCollection<BookDetailViewModel>();

        _modelOperation = model ?? BookModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsBookSelected = false;

        Task.Run(this.LoadBooks);
    }

    private bool CanStoreBook()
    {
        return !(
            string.IsNullOrWhiteSpace(this.Title) ||
            string.IsNullOrWhiteSpace(this.Author) ||
            string.IsNullOrWhiteSpace(this.Genre)
        );
    }

    private void StoreBook()
    {
        Task.Run(async () =>
        {
            string lastId = (await this._modelOperation.GetCountAsync() + 1).ToString();

            await this._modelOperation.AddAsync(lastId, Title, Author, Genre);

            LoadBooks();

            _informer.InformSuccess("Book added successfully!");

        });
    }

    private void DeleteBook()
    {
        Task.Run(async () =>
        {
            try
            {
                await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                LoadBooks();

                _informer.InformSuccess("Book deleted successfully!");
            }
            catch (Exception e)
            {
                _informer.InformError("Error while deleting book!");
            }
        });
    }

    private async void LoadBooks()
    {
        Dictionary<string, BookModel> books = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _books.Clear();

            foreach (BookModel b in books.Values)
            {
                _books.Add(new BookDetailViewModel(b.Id, b.Title, b.Author, b.Genre));
            }
        });

        OnPropertyChanged(nameof(Books));
    }
}
