using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiAPI.Models;

namespace MultiAPI.Repositories
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> Get();
        Task<TodoItem> Get(int id);
        Task<TodoItem> Create(TodoItem todoitem);
        Task Update(TodoItem todoitem);
        Task Delete(int id);
    }
}
