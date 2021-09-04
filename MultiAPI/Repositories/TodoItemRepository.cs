using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiAPI.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationDbContext _context;
        //Query the database using the TodoItem context

        //Constructor (it is how we inject it)

        //Add() adds the instance of the todolist
        //SaveChangesAsync() insert the data into the database
        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TodoItem> Get(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }
        public async Task<TodoItem> Create(TodoItem todolist)
        {
            _context.TodoItems.Add(todolist);
            await _context.SaveChangesAsync();

            return todolist;
        }
        public async Task<IEnumerable<TodoItem>> Get()
        {
            return await _context.TodoItems.ToListAsync();
        }
        public async Task Update(TodoItem todolist)
        {
            _context.Entry(todolist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var todolistDelete = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(todolistDelete);
            await _context.SaveChangesAsync();
        }
    }
}