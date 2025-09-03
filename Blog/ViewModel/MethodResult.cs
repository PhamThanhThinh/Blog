namespace Blog.ViewModel
{
  public record struct MethodResult(bool Status, string? ErrorMessage = null)
  {
    public static MethodResult Success() => new(true);
    public static MethodResult Fail(string errorMessage) => new(false, errorMessage);
  }
}
