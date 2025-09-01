using Blog.ViewModel;

namespace Blog.Services
{
  public class AuthenService
  {
    private readonly UserService _userService;

    public AuthenService(UserService userService)
    {
      _userService = userService;
    }

    public async Task<UserLogin?> GetUserAsync(LoginViewModel loginViewModel)
    {
      var userLogin = await _userService.LoginAsync(loginViewModel);
      return userLogin;
    }

  }
}
