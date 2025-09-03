using Blog.Data;
using Blog.Data.Entities;
using Blog.ViewModel;
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

    //LuuDanhMucBatDongBo
    public async Task<MethodResult> SaveCategoryAsync(Category category)
    {
      try
      {
        if (category.Id > 0)
        {
          _db.Categories.Update(category);
        }
        else
        {
          await _db.Categories.AddAsync(category);
        }
        await _db.SaveChangesAsync();
        return MethodResult.Success();
      }
      catch (Exception ex)
      {
        return MethodResult.Fail(ex.Message);
      }
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
