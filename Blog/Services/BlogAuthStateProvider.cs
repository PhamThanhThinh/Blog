using Blog.ViewModel;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blog.Services
{
  public class BlogAuthStateProvider : AuthenticationStateProvider, IDisposable // triển khai interface IDisposable
  {
    // hằng số
    private const string BlogAuthenKey = "blog_authen";
    // tạo hàm dựng constructor
    private readonly AuthenService _authenService;

    public BlogAuthStateProvider(AuthenService authenService)
    {
      _authenService = authenService;
      AuthenticationStateChanged += BlogAuthStateProvider_AuthenStateChanged;
    }

    private async void BlogAuthStateProvider_AuthenStateChanged(Task<AuthenticationState> authenticationState)
    {
      var state = await authenticationState;
      if (state is not null)
      {
        var userId = Convert.ToInt32(state.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var name = state.User.FindFirstValue(ClaimTypes.Name);

        UserLogin = new(userId, name);
      }

    }

    public UserLogin UserLogin { get; private set; } = new(0, string.Empty);

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

    public async Task<string?> LoginAsync(LoginViewModel loginViewModel)
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
    public void Dispose() => 
      AuthenticationStateChanged -= BlogAuthStateProvider_AuthenStateChanged;
  }
}
