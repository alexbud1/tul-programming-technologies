using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.API;
using Data.Implementation;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DataTests
{
    [TestClass]
    [DeploymentItem("TestDatabase.mdf", "DataTest")]
    public class DataTests
    {
        private static string connectionString;
        private readonly IDataRepository _dataRepository = IDataRepository.CreateDatabase(IDataContext.CreateContext(connectionString));

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string _DBRelativePath = @"DataTest\TestDatabase.mdf";
            string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
            FileInfo _databaseFile = new FileInfo(_DBPath);

            Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
            Console.WriteLine($"Database File Path: {_DBPath}");

            Assert.IsTrue(_databaseFile.Exists, $"Database file not found: {_DBPath}");
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }

        [TestMethod]
        public async Task UserTests()
        {
            string userId = Guid.NewGuid().ToString();

            await _dataRepository.AddUserAsync(userId, "kowalski@gmail.com", "1234567890", "Michael");

            IUser user = await _dataRepository.GetUserAsync(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.Id);
            Assert.AreEqual("Michael", user.Name);
            Assert.AreEqual("kowalski@gmail.com", user.Email);

            Assert.IsNotNull(await _dataRepository.GetAllUsersAsync());
            Assert.IsTrue(int.Parse(await _dataRepository.GetUsersCountAsync()) > 0);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUserAsync("nonexistentId"));

            await _dataRepository.UpdateUserAsync(userId, "updated@gmail.com", "0987654321", "Bart");

            IUser userUpdated = await _dataRepository.GetUserAsync(userId);

            Assert.IsNotNull(userUpdated);
            Assert.AreEqual(userId, userUpdated.Id);
            Assert.AreEqual("Bart", userUpdated.Name);
            Assert.AreEqual("updated@gmail.com", userUpdated.Email);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.UpdateUserAsync("nonexistentId", "email", "phone", "name"));

            await _dataRepository.DeleteUserAsync(userId);
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUserAsync(userId));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.DeleteUserAsync(userId));
        }

        [TestMethod]
        public async Task BookTests()
        {
            string bookId = Guid.NewGuid().ToString();

            await _dataRepository.AddBookAsync(bookId, "1984", "George Orwell", "Dystopian");

            IBook book = await _dataRepository.GetBookAsync(bookId);

            Assert.IsNotNull(book);
            Assert.AreEqual(bookId, book.Id);
            Assert.AreEqual("1984", book.Title);
            Assert.AreEqual("George Orwell", book.Author);
            Assert.AreEqual("Dystopian", book.Genre);

            Assert.IsNotNull(await _dataRepository.GetAllBooksAsync());
            Assert.IsTrue(int.Parse(await _dataRepository.GetBooksCountAsync()) > 0);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetBookAsync("nonexistentId"));

            await _dataRepository.UpdateBookAsync(bookId, "Animal Farm", "George Orwell", "Satire");

            IBook bookUpdated = await _dataRepository.GetBookAsync(bookId);

            Assert.IsNotNull(bookUpdated);
            Assert.AreEqual(bookId, bookUpdated.Id);
            Assert.AreEqual("Animal Farm", bookUpdated.Title);
            Assert.AreEqual("George Orwell", bookUpdated.Author);
            Assert.AreEqual("Satire", bookUpdated.Genre);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.UpdateBookAsync("nonexistentId", "title", "author", "genre"));

            await _dataRepository.DeleteBookAsync(bookId);
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetBookAsync(bookId));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.DeleteBookAsync(bookId));
        }

        [TestMethod]
        public async Task StateTests()
        {
            string bookId = Guid.NewGuid().ToString();
            string stateId = Guid.NewGuid().ToString();

            await _dataRepository.AddBookAsync(bookId, "To Kill a Mockingbird", "Harper Lee", "Fiction");

            IBook book = await _dataRepository.GetBookAsync(bookId);

            await _dataRepository.AddStateAsync(stateId, bookId, true);

            IState state = await _dataRepository.GetStateAsync(stateId);

            Assert.IsNotNull(state);
            Assert.AreEqual(stateId, state.Id);
            Assert.AreEqual(bookId, state.BookId);
            Assert.IsTrue(state.Availability);

            Assert.IsNotNull(await _dataRepository.GetAllStatesAsync());
            Assert.IsTrue(int.Parse(await _dataRepository.GetStatesCountAsync()) > 0);

            await _dataRepository.UpdateStateAsync(stateId, bookId, false);

            IState stateUpdated = await _dataRepository.GetStateAsync(stateId);

            Assert.IsNotNull(stateUpdated);
            Assert.AreEqual(stateId, stateUpdated.Id);
            Assert.AreEqual(bookId, stateUpdated.BookId);
            Assert.IsFalse(stateUpdated.Availability);

            await _dataRepository.DeleteStateAsync(stateId);
            await _dataRepository.DeleteBookAsync(bookId);
        }

        [TestMethod]
        public async Task EventTests()
        {
            string purchaseEventId = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            string bookId = Guid.NewGuid().ToString();
            string stateId = Guid.NewGuid().ToString();

            Console.WriteLine("Adding book...");
            await _dataRepository.AddBookAsync(bookId, "Brave New World", "Aldous Huxley", "Dystopian");
            Console.WriteLine("Book added.");

            await _dataRepository.AddStateAsync(stateId, bookId, true);
            await _dataRepository.AddUserAsync(userId, "kowalski@gmail.com", "1234567890", "Michael");

            IBook book = await _dataRepository.GetBookAsync(bookId);
            IState state = await _dataRepository.GetStateAsync(stateId);
            IUser user = await _dataRepository.GetUserAsync(userId);

            await _dataRepository.AddEventAsync(purchaseEventId, stateId, userId, "Borrow");

            IEvent purchaseEvent = await _dataRepository.GetEventAsync(purchaseEventId);

            Assert.IsNotNull(purchaseEvent);
            Assert.AreEqual(purchaseEventId, purchaseEvent.Id);
            Assert.AreEqual(stateId, purchaseEvent.StateId);
            Assert.AreEqual(userId, purchaseEvent.CustomerId);

            Assert.IsNotNull(await _dataRepository.GetAllEventsAsync());
            Assert.IsTrue(int.Parse(await _dataRepository.GetEventsCountAsync()) > 0);

            await _dataRepository.UpdateEventAsync(purchaseEventId, DateTime.Now, stateId, userId, "Return");

            IEvent eventUpdated = await _dataRepository.GetEventAsync(purchaseEventId);

            Assert.IsNotNull(eventUpdated);
            Assert.AreEqual(purchaseEventId, eventUpdated.Id);
            Assert.AreEqual(stateId, eventUpdated.StateId);
            Assert.AreEqual(userId, eventUpdated.CustomerId);

            await _dataRepository.DeleteEventAsync(purchaseEventId);
            await _dataRepository.DeleteStateAsync(stateId);
            await _dataRepository.DeleteBookAsync(bookId);
            await _dataRepository.DeleteUserAsync(userId);
        }

        [TestMethod]
        public async Task EventsActionTest()
        {
            string userId1 = Guid.NewGuid().ToString();
            string stateId1 = Guid.NewGuid().ToString();
            string purchaseEventId1 = Guid.NewGuid().ToString();
            string bookId1 = Guid.NewGuid().ToString();

            await _dataRepository.AddBookAsync(bookId1, "1984", "George Orwell", "Dystopian");
            await _dataRepository.AddStateAsync(stateId1, bookId1, true);
            await _dataRepository.AddUserAsync(userId1, "kowalski@gmail.com", "1234567890", "Michael");
            await _dataRepository.AddEventAsync(purchaseEventId1, stateId1, userId1, "Borrow");

            // Assuming the action reduces the availability for a purchase event
            Assert.IsFalse((await _dataRepository.GetStateAsync(stateId1)).Availability);

            await _dataRepository.DeleteEventAsync(purchaseEventId1);
            await _dataRepository.DeleteStateAsync(stateId1);
            await _dataRepository.DeleteBookAsync(bookId1);
            await _dataRepository.DeleteUserAsync(userId1);

            // Additional cases for return events, etc.
        }
    }
}
