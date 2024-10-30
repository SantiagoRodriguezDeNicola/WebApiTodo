using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTodo.Models;

namespace WebApiTodo.Data
{
    public class WebApiTodoContext : DbContext
    {
        public WebApiTodoContext (DbContextOptions<WebApiTodoContext> options)
            : base(options)
        {
        }

        public DbSet<WebApiTodo.Models.ToDo> ToDo { get; set; } = default!;
    }
}
