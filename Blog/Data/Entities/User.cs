using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities
{
  public class User
  {
    [Key]
    public int Id { get; set; }
    // name chia ra thành: first name và last name
    // bắt buộc nhập tên (Required)
    [Required, MaxLength(100), Unicode(true)]
    public string FirstName { get; set; }

    // không bắt buộc nhập họ
    [MaxLength(100), Unicode(true)]
    public string LastName { get; set; }


    [Required, MaxLength(100), Unicode(false)]
    // vidu@gmail.com
    // email chính là username
    public string Email { get; set; }


    // dành cho việc mã hóa mật khẩu
    // random chuỗi
    // chuỗi ngẫu nhiên
    [Required, MaxLength(100), Unicode(false)]
    public string Salt { get; set; }

    // mã hóa
    // hash
    // mã hóa mật khẩu
    [Required, MaxLength(100), Unicode(false)]
    public string Hash { get; set; }
  }
}
