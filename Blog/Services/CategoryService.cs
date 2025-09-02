using Blog.Data;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
  public class CategoryService
  {
    // viết hàm dựng
    // constructor
    private readonly BlogDbContext _db;

    public CategoryService(BlogDbContext db)
    {
      _db = db;
    }

    // method get
    //public async Task<IEnumerable<Category>> GetCategoriesAsync() =>
    //  await _db.Categories
    //  .AsNoTracking()
    //  .ToListAsync();
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      return await _db.Categories
                      .AsNoTracking()
                      .ToListAsync();
    }

  }
}
