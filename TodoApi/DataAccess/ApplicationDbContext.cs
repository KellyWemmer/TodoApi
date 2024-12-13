using Microsoft.EntityFrameworkCore;

namespace TodoApi.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        //Defines a constructor that accepts an instance of DbContextOptions<ApplicationDbContext>
            //Used to configure the DbContext with settings like database provider/connection string
        //DbContext class uses the options to configure itself ie, how it connects to database
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Models.TodoItemModel> TodoItems { get; set; }
    }
}
