using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
  public class BlogDbContext : DbContext
  {
    public BlogDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    //protected BlogDbContext()
    //{
    //}
    
    // dùng trong chế độ debug
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
#if DEBUG
      optionsBuilder.LogTo(Console.WriteLine);
#endif
    }

    // data seed (dữ liệu khởi tạo)
    // has data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // code
      modelBuilder.Entity<User>()
        .HasData(
        new User
        {
          // liệt kê các trường dữ liệu
          Id = 1,
          Email = "admin@gmail.com",
          FirstName = "Admin",
          LastName = "Admin",
          Salt = "650d5ec38ac4d21d37f98e97e3610de9",
          Hash = "d04ca2ccc7148f2a72352c458e035b256949b35d"
        });
    }

  }
}
