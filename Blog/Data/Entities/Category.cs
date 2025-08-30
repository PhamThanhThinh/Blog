using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities
{
  // danh mục bài viết
  public class Category
  {
    [Key]
    // mã định danh
    public int Id { get; set; }

    [Required, MaxLength(100), Unicode(true)]
    // tiếng Việt ta dùng Unicode để lưu tiếng Việt
    //mặc định là Unicode(true) nvarchar trong sql
    // sửa thành Unicode(false) varchar trong sql
    public string Name { get; set; }

    [Required, MaxLength(100), Unicode(false)]
    // phục vụ cho SEO, link tiếng Việt không dấu
    public string Slug { get; set; }

    // ví dụ

    /*
    Category Name: Công Nghệ Thông Tin

    Slug: cong-nghe-thong-tin

    URL: https://myblog.com/category/cong-nghe-thong-tin
    /category/tin-tuc-cong-nghe
    */

  }
}
