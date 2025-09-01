using Microsoft.AspNetCore.Components.Authorization;

namespace Blog.Services
{
  public class BlogAuthStateProvider : AuthenticationStateProvider
  {
    // tạo hàm dựng constructor
    private readonly AuthenService _authenService;

    public BlogAuthStateProvider(AuthenService authenService)
    {
      _authenService = authenService;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      throw new NotImplementedException();
    }

    //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //{
    //  var user = await _authenService.GetUserAsync();
    //}
  }
}
