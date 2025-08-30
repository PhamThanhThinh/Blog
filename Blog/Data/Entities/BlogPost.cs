using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities
{
  public class BlogPost
  {
    [Key]
    public int Id { get; set; }

    // tiêu đề của bài viết
    [Required, MaxLength(200)]
    public string Title { get; set; }

    // phục vụ cho SEO, link tiếng Việt không dấu
    [Required, MaxLength(100), Unicode(false)]
    public string Slug { get; set; }
    // ví dụ lưu blazor-basic tạo thành đường link /post/blazor-basic

    // nối bảng, liên kết bảng, khóa ngoại
    public int CategoryId { get; set; }
    public int UserId { get; set; }

    // intro: tóm tắt bài viết (hiển thị trên công cụ tìm kiếm: google, bing, duckduckgo..., yahoo search...)
    [Required, MaxLength(300)]
    public string Introduction { get; set; }
    // content: nội dung bài viết
    // mặc định có length là max: nvarchar(max)
    [Required]
    public string Content { get; set; }

    // nút để đăng bài: publish: on/off, yes/no
    // khi lưu có publish không
    public bool IsPublished { get; set; }

    // ngày đăng/ngày publish bài viết
    public DateTime? PostDate { get; set; }

    // ngày tạo bài viết khác ngày đăng nha
    // ngày tạo bài viết
    public DateTime CreatedDate { get; set; }
    // ngày bài viết được chỉnh sửa
    public DateTime? ModifiedDate { get; set; }

    // dòng code quyết định các bảng có liên kết với nhau không
    public virtual Category Category { get; set; }
    public virtual User User { get; set; }

  }
}
