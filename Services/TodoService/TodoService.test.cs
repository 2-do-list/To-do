using NUnit.Framework;
using TodoProject.Dtos.Todo;
using TodoProject.Services;
using Moq;

namespace TodoProject.Tests.Services
{
    [TestFixture]
    public class TodoServiceTests
    {
        private TodoService _todoService;

        [SetUp]
        public void Setup()
        {
            // create a mock mapper
            var mapperMock = new Mock<IMapper>();

            // create an instance pf mocked dependencies
            _todoService = new TodoService(mapperMock.Object);
        }

        [Test]
        public async Task UpdateTodo_ValidTodo_ReturnsSuccessResponseWithData()
        {
            // arrange
            var updatedTodo = new UpdateTodoDto
            {
                id = 123,
                name = "John Doe",
                todoTitle = "Test Title",
                todoContext = "Test Context",
                createdAt = DateTime.Now
            };

            // act
            var serviceResponse = await _todoService.UpdateTodo(updatedTodo);

            // assert
            Assert.IsTrue(serviceResponse.Success);
            Assert.IsNotNull(serviceResponse.Data);
            Assert.AreEqual(updatedTodo.name, serviceResponse.Data.name);
            Assert.AreEqual(updatedTodo.todoTitle, serviceResponse.Data.todoTitle);
            Assert.AreEqual(updatedTodo.todoContext, serviceResponse.Data.todoContext);
            Assert.AreEqual(updatedTodo.createdAt, serviceResponse.Data.createdAt);
        }

        [Test]
        public async Task UpdateTodo_TodoNotFound_ReturnsErrorResponse()
        {
            // arrange
            var updatedTodo = new UpdateTodoDto
            {
                id = 123,
                name = "John",
                todoTitle = "Test Title",
                todoContext = "Test Context",
                createdAt = DateTime.Now
            };

            // act
            var serviceResponse = await _todoService.UpdateTodo(updatedTodo);

            // assert
            Assert.IsFalse(serviceResponse.Success);
            Assert.IsNull(serviceResponse.Data);
            Assert.IsNotNull(serviceResponse.Message);
            Assert.IsTrue(serviceResponse.Message.Contains(updatedTodo.id.ToString()));
        }

         [Test]
        public async Task DeleteTodo_ExistingId_ReturnsRemainingTodos()
        {
            // arrange
            var id = 123;
            var todos = new List<Todo>
            {
                new Todo { id = 123, name = "John Doe", todoTitle = "Task 1", todoContext = "Context 1", createdAt = DateTime.Now },
                new Todo { id = 456, name = "Jane Smith", todoTitle = "Task 2", todoContext = "Context 2", createdAt = DateTime.Now }
            };

            // mock the data retrieval process and mapper
            var mockDataRetrievalService = new Mock<IDataRetrievalService>();
            mockDataRetrievalService.Setup(service => service.GetTodos()).ReturnsAsync(todos);
            var mockMapper = new Mock<IMapper>();

            var todoService = new TodoService(mockDataRetrievalService.Object, mockMapper.Object);

            // act
            var serviceResponse = await todoService.DeleteTodo(id);

            // assert
            Assert.IsNotNull(serviceResponse);
            Assert.IsTrue(serviceResponse.Success);
            Assert.IsNotNull(serviceResponse.Data);
            Assert.AreEqual(todos.Count - 1, serviceResponse.Data.Count);
        }

        [Test]
        public async Task GetTodoById_ExistingId_ReturnsTodo()
        {
            // arrange
            var id = 123;
            var todo = new Todo
            {
                id = id,
                name = "John Doe",
                todoTitle = "Task 1",
                todoContext = "Context 1",
                createdAt = DateTime.Now
            };

            var mockDataRetrievalService = new Mock<IDataRetrievalService>();
            mockDataRetrievalService.Setup(service => service.GetTodoById(id)).ReturnsAsync(todo);
            var mockMapper = new Mock<IMapper>();

            var todoService = new TodoService(mockDataRetrievalService.Object, mockMapper.Object);

            // act
            var serviceResponse = await todoService.GetTodoById(id);

            // assert
            Assert.IsNotNull(serviceResponse);
            Assert.IsTrue(serviceResponse.Success);
            Assert.IsNotNull(serviceResponse.Data);
            Assert.AreEqual(id, serviceResponse.Data.id);
        }

        
        [Test]
        public async Task GetAllTodos_ReturnsAllTodos()
        {
            // arrange
            var todos = new List<Todo>
            {
                new Todo { id = 1, name = "John Doe", todoTitle = "Task 1", todoContext = "Context 1", createdAt = DateTime.Now },
                new Todo { id = 2, name = "Jane Smith", todoTitle = "Task 2", todoContext = "Context 2", createdAt = DateTime.Now }
            };

            var mockDataRetrievalService = new Mock<IDataRetrievalService>();
            mockDataRetrievalService.Setup(service => service.GetTodos()).ReturnsAsync(todos);
            var mockMapper = new Mock<IMapper>();
       
            var todoService = new TodoService(mockDataRetrievalService.Object, mockMapper.Object);

            // act
            var serviceResponse = await todoService.GetAllTodos();

            // assert
            Assert.IsNotNull(serviceResponse);
            Assert.IsTrue(serviceResponse.Success);
            Assert.IsNotNull(serviceResponse.Data);
            Assert.AreEqual(todos.Count, serviceResponse.Data.Count);
        }
    }
}
