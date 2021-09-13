using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MultiAPI.Models
{
    public class TodoItemContext : IdentityDbContext
    {
        public TodoItemContext(DbContextOptions<TodoItemContext> options)
            : base(options)
        {

        }
        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
