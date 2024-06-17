using Presentation.Model;
using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

internal class RandomGenerator : IGenerator
{
    private readonly IErrorInformer _informer = new TextErrorInformer();

    public void GenerateUserModels(UserMasterViewModel viewModel)
    {
        UserModelOperation operation = UserModelOperation.CreateModelOperation(new FakeUserCRUD());

        for (int i = 1; i <= 10; i++)
        {
            viewModel.Users.Add(UserDetailViewModel.CreateViewModel(
                RandomString(10),
                RandomEmail(),
                RandomPhone(),
                RandomString(10),
                operation,
                _informer
            ));
        }
    }

    public void GenerateBookModels(BookMasterViewModel viewModel)
    {
        BookModelOperation operation = BookModelOperation.CreateModelOperation(new FakeBookCRUD());

        for (int i = 1; i <= 10; i++)
        {
            viewModel.Books.Add(BookDetailViewModel.CreateViewModel(
                RandomString(10),
                RandomString(15),
                RandomString(10),
                RandomString(7),
                operation,
                _informer
            ));
        }
    }

    public void GenerateStateModels(StateMasterViewModel viewModel)
    {
        StateModelOperation operation = StateModelOperation.CreateModelOperation(new FakeStateCRUD());

        for (int i = 1; i <= 10; i++)
        {
            viewModel.States.Add(StateDetailViewModel.CreateViewModel(
                RandomString(10),
                RandomString(10),
                RandomBool(),
                DateTime.Now,
                operation,
                _informer
            ));
        }
    }

    public void GenerateEventModels(EventMasterViewModel viewModel)
    {
        EventModelOperation operation = EventModelOperation.CreateModelOperation(new FakeEventCRUD());

        for (int i = 1; i <= 10; i++)
        {
            viewModel.Events.Add(EventDetailViewModel.CreateViewModel(
                RandomString(10),
                RandomString(10),
                RandomString(10),
                "Borrow",
                DateTime.Now,
                operation,
                _informer
            ));
        }
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
