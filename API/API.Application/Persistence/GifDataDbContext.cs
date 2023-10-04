using Microsoft.EntityFrameworkCore;

namespace TechMentor.Persistence
{
    public class GifDataDbContext : DbContext
    {   
        public GifDataDbContext(DbContextOptions<GifDataDbContext> options) : base(options)
        {

        }

    }
}
