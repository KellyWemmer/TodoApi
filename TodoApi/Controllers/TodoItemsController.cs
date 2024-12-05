using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    //ControllerBase is a class that provides basic functionality for API controllers in ASP.NET Core
    //TodoItemsController is inheriting from ControllerBase
    public class TodoItemsController : ControllerBase
    {
        //Encapsulation, we want to hide this from the outside, can only be modified through this controller
        private static List<TodoItemModel> _todoItems = new List<TodoItemModel>();

        //GetAll in the list
        [HttpGet]
        public ActionResult<List<TodoItemModel>>GetTodoItems()
        {
            //return type is an action, it allows you to return both the data and the HTTP status code(200-OK, 404 Not Found etc.)
            //Stands for OkObjectResult
            return Ok(_todoItems);

        }

        //Get by Id
        [HttpGet("{Id}")]
        public ActionResult<TodoItemModel> GetTodoItem(int id)
        {
            TodoItemModel? item = _todoItems.FirstOrDefault(x => x.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item);    
        }

        [HttpPost]
        //[FromBody] tells ASP.NET Core's model binding system to take the raw data sent in the request body (usually JSON or XML)
        //and convert it into an object of the specified type (TodoItemModel)
        public ActionResult<TodoItemModel> CreateTodoItem(TodoItemModel newTodoItem) 
        {
            //Generate a new Id based on the max in the list
            newTodoItem.Id = _todoItems.Max(x => x.Id + 1);
            _todoItems.Add(newTodoItem);
            //CreatedAction helps process the return of status 201(created)
            //nameof(Get) get's the item with the created Id
            //"new" creates the object using the newTodoItem.Id for the GetTodoItem.Id
            return CreatedAtAction(nameof(GetTodoItem), new {id = newTodoItem.Id}, newTodoItem);
            
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateTodoItem(int id, TodoItemModel updatedItem)
        {
            TodoItemModel? existingItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            
            existingItem.Title = updatedItem.Title;
            existingItem.IsComplete = updatedItem.IsComplete;

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteTodoItem(int id)
        {
            TodoItemModel? itemToDelete = _todoItems.FirstOrDefault(x => x.Id == id);
            if (itemToDelete == null)
            {
                return NotFound();
            }
            _todoItems.Remove(itemToDelete);

            return NoContent();
        }
    }
}
