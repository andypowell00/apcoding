using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apapi.Abstract;
using apapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace apapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _TodoRepository;

        public TodoController(ITodoRepository TodoRepository)
        {
            _TodoRepository = TodoRepository;
        }

        //[NoCache]
        [HttpGet]
        public async Task<IEnumerable<Todo>> Get()
        {
            return await _TodoRepository.GetAllTodos();
        }

        // GET api/Todo/5 - retrieves a specific Todo using InternalId (BSonId)
        [HttpGet("{id}")]
        public async Task<Todo> Get(string id)
        {
            return await _TodoRepository.GetTodo(id) ?? new Todo();

        }

        // POST api/Todo - creates a new Todo
        [HttpPost]
        public void Post([FromBody]Todo newTodo)
        {
            _TodoRepository.AddTodo(new Todo
            {
                title = newTodo.title,
                complete = newTodo.complete,
                description = newTodo.description,
                createDate = newTodo.createDate
            });
        }

        // PUT api/Todo/5 - updates a specific Todo
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            // _TodoRepository.UpdateTodoDocument(id, value);
        }

        // DELETE api/Todo/5 - deletes a specific Todo
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _TodoRepository.RemoveTodo(id);
        }
    }
}
