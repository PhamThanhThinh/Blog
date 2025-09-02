using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel
{
  public class LoginViewModel
  {
    [Required, EmailAddress, DataType(DataType.EmailAddress)]
    public string UserName { get; set; }

    [Required, MinLength(1)]
    public string Password { get; set; }
  }
}
