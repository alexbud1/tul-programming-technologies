using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model;
using System.Windows;

namespace Presentation.ViewModel
{

    public class BookDetailViewModel : IViewModel
    {
        public static BookDetailViewModel CreateViewModel(string id, string title, string author, string genre,
        BookModelOperation model, IErrorInformer informer)
        {
            return new BookDetailViewModel(id, title, author, genre, model, informer);
        }


        public ICommand UpdateBook { get; set; }

        private readonly BookModelOperation _modelOperation;

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

        public BookDetailViewModel(BookModelOperation? model = null, IErrorInformer? informer = null)
        {
            UpdateBook = new OnClickCommand(e => Update(), c => CanUpdate());

            _modelOperation = model ?? BookModelOperation.CreateModelOperation();
            _informer = informer ?? new PopupErrorInformer();
        }

        public BookDetailViewModel(string id, string title, string author, string genre, BookModelOperation? model = null, IErrorInformer? informer = null)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;

            UpdateBook = new OnClickCommand(e => Update(), c => CanUpdate());

            _modelOperation = model ?? BookModelOperation.CreateModelOperation();
            _informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                _modelOperation.UpdateAsync(Id, Title, Author, Genre);

                _informer.InformSuccess("Book successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Author) ||
                string.IsNullOrWhiteSpace(Genre)
            );
        }
    }
}