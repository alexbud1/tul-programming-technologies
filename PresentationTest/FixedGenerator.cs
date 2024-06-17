using Presentation.Model;
using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

internal class FixedGenerator : IGenerator
{
    private readonly IErrorInformer _informer = new TextErrorInformer();

    public void GenerateUserModels(UserMasterViewModel viewModel)
    {
        UserModelOperation operation = UserModelOperation.CreateModelOperation(new FakeUserCRUD());

        viewModel.Users.Add(UserDetailViewModel.CreateViewModel("1", "alice@example.com", "1234567890", "Alice", operation, _informer));
        viewModel.Users.Add(UserDetailViewModel.CreateViewModel("2", "bob@example.com", "2345678901", "Bob", operation, _informer));
        viewModel.Users.Add(UserDetailViewModel.CreateViewModel("3", "charlie@example.com", "3456789012", "Charlie", operation, _informer));
        viewModel.Users.Add(UserDetailViewModel.CreateViewModel("4", "diana@example.com", "4567890123", "Diana", operation, _informer));
        viewModel.Users.Add(UserDetailViewModel.CreateViewModel("5", "eve@example.com", "5678901234", "Eve", operation, _informer));
    }

    public void GenerateBookModels(BookMasterViewModel viewModel)
    {
        BookModelOperation operation = BookModelOperation.CreateModelOperation(new FakeBookCRUD());

        viewModel.Books.Add(BookDetailViewModel.CreateViewModel("1", "1984", "George Orwell", "Dystopian", operation, _informer));
        viewModel.Books.Add(BookDetailViewModel.CreateViewModel("2", "To Kill a Mockingbird", "Harper Lee", "Fiction", operation, _informer));
        viewModel.Books.Add(BookDetailViewModel.CreateViewModel("3", "The Great Gatsby", "F. Scott Fitzgerald", "Classic", operation, _informer));
        viewModel.Books.Add(BookDetailViewModel.CreateViewModel("4", "Pride and Prejudice", "Jane Austen", "Romance", operation, _informer));
        viewModel.Books.Add(BookDetailViewModel.CreateViewModel("5", "Moby-Dick", "Herman Melville", "Adventure", operation, _informer));
    }

    public void GenerateStateModels(StateMasterViewModel viewModel)
    {
        StateModelOperation operation = StateModelOperation.CreateModelOperation(new FakeStateCRUD());

        viewModel.States.Add(StateDetailViewModel.CreateViewModel("1", "1", true, DateTime.Now, operation, _informer));
        viewModel.States.Add(StateDetailViewModel.CreateViewModel("2", "2", true, DateTime.Now, operation, _informer));
        viewModel.States.Add(StateDetailViewModel.CreateViewModel("3", "3", false, DateTime.Now, operation, _informer));
        viewModel.States.Add(StateDetailViewModel.CreateViewModel("4", "4", true, DateTime.Now, operation, _informer));
        viewModel.States.Add(StateDetailViewModel.CreateViewModel("5", "5", false, DateTime.Now, operation, _informer));
    }

    public void GenerateEventModels(EventMasterViewModel viewModel)
    {
        EventModelOperation operation = EventModelOperation.CreateModelOperation(new FakeEventCRUD());

        viewModel.Events.Add(EventDetailViewModel.CreateViewModel("1", "1", "1", "Borrow", DateTime.Now, operation, _informer));
        viewModel.Events.Add(EventDetailViewModel.CreateViewModel("2", "2", "2", "Return", DateTime.Now, operation, _informer));
        viewModel.Events.Add(EventDetailViewModel.CreateViewModel("3", "3", "3", "Reserve", DateTime.Now, operation, _informer));
        viewModel.Events.Add(EventDetailViewModel.CreateViewModel("4", "4", "4", "CancelReservation", DateTime.Now, operation, _informer));
        viewModel.Events.Add(EventDetailViewModel.CreateViewModel("5", "5", "5", "Borrow", DateTime.Now, operation, _informer));
    }

    private string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random();
        var randomText = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomText[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomText);
    }

    private string RandomStringWithNumber(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var randomText = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomText[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomText);
    }

    private string RandomEmail()
    {
        return $"{RandomStringWithNumber(10)}@{RandomString(5)}.com";
    }

    private string RandomPhone()
    {
        var random = new Random();
        var phone = new char[10];

        for (int i = 0; i < 10; i++)
        {
            phone[i] = (char)('0' + random.Next(10));
        }

        return new string(phone);
    }

    private bool RandomBool()
    {
        var random = new Random();
        return random.Next(2) == 1;
    }
}
