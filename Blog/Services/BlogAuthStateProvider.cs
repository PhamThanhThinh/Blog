using Blog.ViewModel;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blog.Services
{
  public class BlogAuthStateProvider : AuthenticationStateProvider
  {
    // hằng số
    private const string BlogAuthenKey = "blog_authen";
    // tạo hàm dựng constructor
    private readonly AuthenService _authenService;

    public BlogAuthStateProvider(AuthenService authenService)
    {
      _authenService = authenService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var claimsPrincipal = new ClaimsPrincipal();

      var user = await _authenService.GetInfoUserToBrowserStorageAsync();
      if (user is not null)
      {
        claimsPrincipal = GetClaimsPrincipalFromUser(user.Value);
      }

      var authenState = new AuthenticationState(claimsPrincipal);
      NotifyAuthenticationStateChanged(Task.FromResult(authenState));
      return authenState;

    }

    public async Task<string> LoginAsync(LoginViewModel loginViewModel)
    {
      var userLogin = await _authenService.GetUserAsync(loginViewModel);

      if (userLogin is null)
      {
        return "Email đăng nhập hoặc mật khẩu không đúng";
      }

      var authenState = new AuthenticationState(GetClaimsPrincipalFromUser(userLogin.Value));
      NotifyAuthenticationStateChanged(Task.FromResult(authenState));
      return null;
    }

    public async Task LogoutAsync()
    {
      await _authenService.RemoveInfoUserToBrowserStorageAsync();
      var authenState = new AuthenticationState(new ClaimsPrincipal());
      NotifyAuthenticationStateChanged(Task.FromResult(authenState));
    }

    //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //{
    //  var user = await _authenService.GetUserAsync();
    //}

    private static ClaimsPrincipal GetClaimsPrincipalFromUser(UserLogin userLogin)
    {
      var identity = new ClaimsIdentity(
        new[]
        {
          new Claim(ClaimTypes.NameIdentifier, userLogin.UserId.ToString()),
          new Claim(ClaimTypes.Name, userLogin.DisplayName)
        },
        BlogAuthenKey
        );
      return new ClaimsPrincipal(identity);
    }

  }
}
