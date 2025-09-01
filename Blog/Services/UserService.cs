using Blog.Data;
using Blog.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
  public class UserService
  {
    // viết hàm dựng
    // constructor
    private readonly BlogDbContext _db;

    public UserService(BlogDbContext db)
    {
      _db = db;
    }

    public async Task<UserLogin?> LoginAsync(LoginViewModel loginViewModel)
    {
      // viết biểu thức lambda/method syntax (cú pháp theo kiểu gọi phương thức)
      //var dbContextUser = await _db.Users
      //  .AsNoTracking()
      //  .Where(u => u.Email == loginViewModel.UserName).ToListAsync();

      var dbContextUser = await _db.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == loginViewModel.UserName);

      if (dbContextUser is not null){
        return new UserLogin(dbContextUser.Id, $"{dbContextUser.FirstName} {dbContextUser.LastName}".Trim());
      }
      else
      {
        // không thể login
        return null;
      }
    }
  }
}
