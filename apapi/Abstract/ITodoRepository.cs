using apapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apapi.Abstract
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllTodos();

        Task<Todo> GetTodo(string id);

        // add new Todo document
        Task AddTodo(Todo item);

        // remove a single document / Todo
        Task<bool> RemoveTodo(string id);

        // update just a single document / Todo
        Task<bool> UpdateTodo(Todo todo);
    }
}
