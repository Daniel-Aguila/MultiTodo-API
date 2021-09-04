using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiAPI.Repositories;
using MultiAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace MultiAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemRepository _todoitemrepository;
        //interact with the database with an instance
        public TodoItemController(ITodoItemRepository todoitemRepository)
        {
            _todoitemrepository = todoitemRepository;
        }

        //method will handle http request
 
        [HttpGet]
        //returns the object into JSON before sending it to the caller
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _todoitemrepository.Get();
        }


        [HttpGet("{id}")]

        //ActionResult can return not found objects       
        public async Task<ActionResult<TodoItem>> GetTodoItems(int id)
        {
            return await _todoitemrepository.Get(id);
        }

        //convert a JSON in the payload to a TODOList object

        [HttpPost]
 
        public async Task<ActionResult<TodoItem>> PostTodoItems([FromBody] TodoItem todoitems)
        {
            var newTodoItem = await _todoitemrepository.Create(todoitems);
            return CreatedAtAction(nameof(GetTodoItems), new { id = newTodoItem.Id }, newTodoItem);
        }


        [HttpPut]
   
        public async Task<ActionResult> PutTodoItems(int id, [FromBody] TodoItem todoitem)
        {
            //Makes sure we are getting the correct todolist base on the id
            if (id != todoitem.Id)
            {
                //returns a 404 code
                return BadRequest();
            }
            await _todoitemrepository.Update(todoitem);
            return NoContent();
        }

        [HttpDelete]
 
        public async Task<ActionResult> Delete(int id)
        {
            var todoitemToDelete = await _todoitemrepository.Get(id);
            if (todoitemToDelete == null)
            {
                return NotFound();
            }

            await _todoitemrepository.Delete(todoitemToDelete.Id);
            return NoContent();
        }
    }
}
