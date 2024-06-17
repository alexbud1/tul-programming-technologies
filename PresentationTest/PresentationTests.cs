using Presentation.Model;
using Presentation.ViewModel;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest
{
    [TestClass]
    public class PresentationTests
    {
        private readonly IErrorInformer _informer = new TextErrorInformer();

       
        [TestMethod]
        public void UserDetailViewModelTests()
        {
            IUserCRUD fakeUserCrud = new FakeUserCRUD();

            UserModelOperation operation = UserModelOperation.CreateModelOperation(fakeUserCrud);

            UserDetailViewModel detail = UserDetailViewModel.CreateViewModel("1", "Test", "test@test.com", "1234567890", operation, _informer);

            Assert.AreEqual("1", detail.Id);
            Assert.AreEqual("Test", detail.Name);
            Assert.AreEqual("test@test.com", detail.Email);
            Assert.AreEqual("1234567890", detail.Phone);

            Assert.IsTrue(detail.UpdateUser.CanExecute(null));
        }

        [TestMethod]
        public void BookMasterViewModelTests()
        {
            IBookCRUD fakeBookCrud = new FakeBookCRUD();

            BookModelOperation operation = BookModelOperation.CreateModelOperation(fakeBookCrud);

            BookMasterViewModel master = BookMasterViewModel.CreateViewModel(operation, _informer);

            master.Title = "Test Book";
            master.Author = "Test Author";
            master.Genre = "Test Genre";

            Assert.IsNotNull(master.CreateBook);
            Assert.IsNotNull(master.RemoveBook);

            Assert.IsTrue(master.CreateBook.CanExecute(null));
            Assert.IsTrue(master.RemoveBook.CanExecute(null));
        }

        [TestMethod]
        public void BookDetailViewModelTests()
        {
            IBookCRUD fakeBookCrud = new FakeBookCRUD();

            BookModelOperation operation = BookModelOperation.CreateModelOperation(fakeBookCrud);

            BookDetailViewModel detail = BookDetailViewModel.CreateViewModel("1", "1984", "George Orwell", "Dystopian", operation, _informer);

            Assert.AreEqual("1", detail.Id);
            Assert.AreEqual("1984", detail.Title);
            Assert.AreEqual("George Orwell", detail.Author);
            Assert.AreEqual("Dystopian", detail.Genre);

            Assert.IsTrue(detail.UpdateBook.CanExecute(null));
        }

        [TestMethod]
        public void StateMasterViewModelTests()
        {
            IStateCRUD fakeStateCrud = new FakeStateCRUD();

            StateModelOperation operation = StateModelOperation.CreateModelOperation(fakeStateCrud);

            StateMasterViewModel master = StateMasterViewModel.CreateViewModel(operation, _informer);

            master.BookId = "1";
            master.Availability = true;

            Assert.IsNotNull(master.CreateState);
            Assert.IsNotNull(master.RemoveState);

            Assert.IsTrue(master.CreateState.CanExecute(null));

            master.Availability = false;

            Assert.IsTrue(master.RemoveState.CanExecute(null));
        }

        [TestMethod]
        public void StateDetailViewModelTests()
        {
            IStateCRUD fakeStateCrud = new FakeStateCRUD();

            StateModelOperation operation = StateModelOperation.CreateModelOperation(fakeStateCrud);

            StateDetailViewModel detail = StateDetailViewModel.CreateViewModel("1", "1", true, DateTime.Now, operation, _informer);

            Assert.AreEqual("1", detail.Id);
            Assert.AreEqual("1", detail.BookId);
            Assert.IsTrue(detail.Availability);

            Assert.IsTrue(detail.UpdateState.CanExecute(null));
        }

        [TestMethod]
        public void EventMasterViewModelTests()
        {
            IEventCRUD fakeEventCrud = new FakeEventCRUD();

            EventModelOperation operation = EventModelOperation.CreateModelOperation(fakeEventCrud);

            EventMasterViewModel master = EventMasterViewModel.CreateViewModel(operation, _informer);

            master.StateId = "1";
            master.CustomerId = "1";
            master.Type = "Borrow";

            Assert.IsNotNull(master.CreateEvent);
            Assert.IsNotNull(master.RemoveEvent);

            Assert.IsTrue(master.CreateEvent.CanExecute(null));
            Assert.IsTrue(master.RemoveEvent.CanExecute(null));
        }

        [TestMethod]
        public void EventDetailViewModelTests()
        {
            IEventCRUD fakeEventCrud = new FakeEventCRUD();

            EventModelOperation operation = EventModelOperation.CreateModelOperation(fakeEventCrud);

            EventDetailViewModel detail = EventDetailViewModel.CreateViewModel("1", "1", "1", "Borrow", DateTime.Now, operation, _informer);

            Assert.AreEqual("1", detail.Id);
            Assert.AreEqual("1", detail.StateId);
            Assert.AreEqual("1", detail.CustomerId);
            Assert.AreEqual("Borrow", detail.Type);

            Assert.IsTrue(detail.UpdateEvent.CanExecute(null));
        }

        [TestMethod]
        public void DataFixedGenerationMethodTests()
        {
            IGenerator fixedGenerator = new FixedGenerator();

            IUserCRUD fakeUserCrud = new FakeUserCRUD();
            UserModelOperation userOperation = UserModelOperation.CreateModelOperation(fakeUserCrud);
            UserMasterViewModel userViewModel = UserMasterViewModel.CreateViewModel(userOperation, _informer);

            IBookCRUD fakeBookCrud = new FakeBookCRUD();
            BookModelOperation bookOperation = BookModelOperation.CreateModelOperation(fakeBookCrud);
            BookMasterViewModel bookViewModel = BookMasterViewModel.CreateViewModel(bookOperation, _informer);

            IStateCRUD fakeStateCrud = new FakeStateCRUD();
            StateModelOperation stateOperation = StateModelOperation.CreateModelOperation(fakeStateCrud);
            StateMasterViewModel stateViewModel = StateMasterViewModel.CreateViewModel(stateOperation, _informer);

            IEventCRUD fakeEventCrud = new FakeEventCRUD();
            EventModelOperation eventOperation = EventModelOperation.CreateModelOperation(fakeEventCrud);
            EventMasterViewModel eventViewModel = EventMasterViewModel.CreateViewModel(eventOperation, _informer);

            fixedGenerator.GenerateUserModels(userViewModel);
            fixedGenerator.GenerateBookModels(bookViewModel);
            fixedGenerator.GenerateStateModels(stateViewModel);
            fixedGenerator.GenerateEventModels(eventViewModel);

            Assert.AreEqual(5, userViewModel.Users.Count);
            Assert.AreEqual(5, bookViewModel.Books.Count);
            Assert.AreEqual(5, stateViewModel.States.Count);
            Assert.AreEqual(5, eventViewModel.Events.Count);
        }

        [TestMethod]
        public void DataRandomGenerationMethodTests()
        {
            IGenerator randomGenerator = new RandomGenerator();

            IUserCRUD fakeUserCrud = new FakeUserCRUD();
            UserModelOperation userOperation = UserModelOperation.CreateModelOperation(fakeUserCrud);
            UserMasterViewModel userViewModel = UserMasterViewModel.CreateViewModel(userOperation, _informer);

            IBookCRUD fakeBookCrud = new FakeBookCRUD();
            BookModelOperation bookOperation = BookModelOperation.CreateModelOperation(fakeBookCrud);
            BookMasterViewModel bookViewModel = BookMasterViewModel.CreateViewModel(bookOperation, _informer);

            IStateCRUD fakeStateCrud = new FakeStateCRUD();
            StateModelOperation stateOperation = StateModelOperation.CreateModelOperation(fakeStateCrud);
            StateMasterViewModel stateViewModel = StateMasterViewModel.CreateViewModel(stateOperation, _informer);

            IEventCRUD fakeEventCrud = new FakeEventCRUD();
            EventModelOperation eventOperation = EventModelOperation.CreateModelOperation(fakeEventCrud);
            EventMasterViewModel eventViewModel = EventMasterViewModel.CreateViewModel(eventOperation, _informer);

            randomGenerator.GenerateUserModels(userViewModel);
            randomGenerator.GenerateBookModels(bookViewModel);
            randomGenerator.GenerateStateModels(stateViewModel);
            randomGenerator.GenerateEventModels(eventViewModel);

            Assert.AreEqual(10, userViewModel.Users.Count);
            Assert.AreEqual(10, bookViewModel.Books.Count);
            Assert.AreEqual(10, stateViewModel.States.Count);
            Assert.AreEqual(10, eventViewModel.Events.Count);
        }
    }
}
