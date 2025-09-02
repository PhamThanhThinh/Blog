namespace Blog.ViewModel
{
  public record struct UserLogin(int UserId, string DisplayName)
  {
    public readonly bool IsEmpty => UserId == 0;
  };
}
