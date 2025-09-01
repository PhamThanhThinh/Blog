using Blog.ViewModel;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blog.Services
{
  public class AuthenService
  {
    private readonly UserService _userService;
    private readonly ProtectedLocalStorage _protectedLocalStorage;

    public AuthenService(UserService userService, ProtectedLocalStorage protectedLocalStorage)
    {
      _userService = userService;
      _protectedLocalStorage = protectedLocalStorage;
    }

    public async Task<UserLogin?> GetUserAsync(LoginViewModel loginViewModel)
    {
      var userLogin = await _userService.LoginAsync(loginViewModel);

      if (userLogin is not null)
      {
        await SaveInfoUserToBrowserStorageAsync(userLogin.Value);
      }

      return userLogin;
    }

    private const string UserStorageKey = "blog_user";
    //private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {

    };

    public async Task SaveInfoUserToBrowserStorageAsync(UserLogin userLogin) =>
      await _protectedLocalStorage
      .SetAsync(UserStorageKey, JsonSerializer
        .Serialize(userLogin, _jsonSerializerOptions));

    public async Task<UserLogin?> GetInfoUserToBrowserStorageAsync()
    {
      try
      {
        var result = await _protectedLocalStorage.GetAsync<string>(UserStorageKey);

        if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
        {
          var userLogin = JsonSerializer.Deserialize<UserLogin>(result.Value, _jsonSerializerOptions);
          return userLogin;
        }

      }
      catch (Exception)
      {
        throw;
      }
      return null;
    }

    public async Task RemoveInfoUserToBrowserStorageAsync() =>
      await _protectedLocalStorage.DeleteAsync(UserStorageKey);

  }
}
