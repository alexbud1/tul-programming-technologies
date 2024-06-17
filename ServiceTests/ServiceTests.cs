using Data.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    [TestClass]
    public class ServiceTests
    {
        private readonly IDataRepository dataRepository = new FakeDataRepository();

        [TestMethod]
        public async Task UserServiceTests()
        {
            // User CRUD operations
            await dataRepository.AddUserAsync("U1", "john.doe@example.com", "123-456-7890", "John Doe");

            IUser user = await dataRepository.GetUserAsync("U1");

            Assert.IsNotNull(user);
            Assert.AreEqual("U1", user.Id);
            Assert.AreEqual("John Doe", user.Name);
            Assert.AreEqual("john.doe@example.com", user.Email);
            Assert.AreEqual("123-456-7890", user.Phone);

            Assert.IsNotNull(await dataRepository.GetAllUsersAsync());
            Assert.IsTrue(int.Parse(await dataRepository.GetUsersCountAsync()) > 0);

            await dataRepository.UpdateUserAsync("U1", "jane.doe@example.com", "098-765-4321", "Jane Doe");

            IUser updatedUser = await dataRepository.GetUserAsync("U1");

            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("U1", updatedUser.Id);
            Assert.AreEqual("Jane Doe", updatedUser.Name);
            Assert.AreEqual("jane.doe@example.com", updatedUser.Email);
            Assert.AreEqual("098-765-4321", updatedUser.Phone);

            await dataRepository.DeleteUserAsync("U1");
        }

        [TestMethod]
        public async Task BookServiceTests()
        {
            // Book CRUD operations
            await dataRepository.AddBookAsync("B1", "Book Title", "Author Name", "Fiction");

            IBook book = await dataRepository.GetBookAsync("B1");

            Assert.IsNotNull(book);
            Assert.AreEqual("B1", book.Id);
            Assert.AreEqual("Book Title", book.Title);
            Assert.AreEqual("Author Name", book.Author);
            Assert.AreEqual("Fiction", book.Genre);

            Assert.IsNotNull(await dataRepository.GetAllBooksAsync());
            Assert.IsTrue(int.Parse(await dataRepository.GetBooksCountAsync()) > 0);

            await dataRepository.UpdateBookAsync("B1", "Updated Title", "Updated Author", "Non-Fiction");

            IBook updatedBook = await dataRepository.GetBookAsync("B1");

            Assert.IsNotNull(updatedBook);
            Assert.AreEqual("B1", updatedBook.Id);
            Assert.AreEqual("Updated Title", updatedBook.Title);
            Assert.AreEqual("Updated Author", updatedBook.Author);
            Assert.AreEqual("Non-Fiction", updatedBook.Genre);

            await dataRepository.DeleteBookAsync("B1");
        }

        [TestMethod]
        public async Task StateServiceTests()
        {
            // Book CRUD operation for state dependency
            await dataRepository.AddBookAsync("B1", "Book Title", "Author Name", "Fiction");

            IBook book = await dataRepository.GetBookAsync("B1");

            // State CRUD operations
            await dataRepository.AddStateAsync("S1", book.Id, true);

            IState state = await dataRepository.GetStateAsync("S1");

            Assert.IsNotNull(state);
            Assert.AreEqual("S1", state.Id);
            Assert.AreEqual(book.Id, state.BookId);
            Assert.IsTrue(state.Availability);

            Assert.IsNotNull(await dataRepository.GetAllStatesAsync());
            Assert.IsTrue(int.Parse(await dataRepository.GetStatesCountAsync()) > 0);

            await dataRepository.UpdateStateAsync("S1", book.Id, false);

            IState updatedState = await dataRepository.GetStateAsync("S1");

            Assert.IsNotNull(updatedState);
            Assert.AreEqual("S1", updatedState.Id);
            Assert.AreEqual(book.Id, updatedState.BookId);
            Assert.IsFalse(updatedState.Availability);

            await dataRepository.DeleteStateAsync("S1");
            await dataRepository.DeleteBookAsync("B1");
        }

        [TestMethod]
        public async Task EventServiceTests()
        {
            // Book and State CRUD operations for event dependency
            await dataRepository.AddBookAsync("B1", "Book Title", "Author Name", "Fiction");
            await dataRepository.AddUserAsync("U1", "john.doe@example.com", "123-456-7890", "John Doe");

            IBook book = await dataRepository.GetBookAsync("B1");
            await dataRepository.AddStateAsync("S1", book.Id, true);

            IState state = await dataRepository.GetStateAsync("S1");
            IUser user = await dataRepository.GetUserAsync("U1");

            // Event CRUD operations
            await dataRepository.AddEventAsync("E1", state.Id, user.Id, "Checkout");

            IEvent eventObj = await dataRepository.GetEventAsync("E1");

            Assert.IsNotNull(eventObj);
            Assert.AreEqual("E1", eventObj.Id);
            Assert.AreEqual(state.Id, eventObj.StateId);
            Assert.AreEqual(user.Id, eventObj.CustomerId);
            Assert.AreEqual("Checkout", eventObj.Type);

            Assert.IsNotNull(await dataRepository.GetAllEventsAsync());
            Assert.IsTrue(int.Parse(await dataRepository.GetEventsCountAsync()) > 0);

            DateTime updatedDate = DateTime.Now.AddDays(1);
            await dataRepository.UpdateEventAsync("E1", updatedDate, state.Id, user.Id, "Return");

            IEvent updatedEvent = await dataRepository.GetEventAsync("E1");

            Assert.IsNotNull(updatedEvent);
            Assert.AreEqual("E1", updatedEvent.Id);
            Assert.AreEqual(state.Id, updatedEvent.StateId);
            Assert.AreEqual(user.Id, updatedEvent.CustomerId);
            Assert.AreEqual("Return", updatedEvent.Type);
            Assert.AreEqual(updatedDate, updatedEvent.EventDate);

            await dataRepository.DeleteEventAsync("E1");
            await dataRepository.DeleteStateAsync("S1");
            await dataRepository.DeleteBookAsync("B1");
            await dataRepository.DeleteUserAsync("U1");
        }
    }
}
