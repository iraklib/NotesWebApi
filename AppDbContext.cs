using Microsoft.EntityFrameworkCore;
using NotesWebApi.Controllers;

namespace NotesWebApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<NoteModel> Notes { get; set; }
    }
}
