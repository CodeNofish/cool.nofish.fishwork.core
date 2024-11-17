namespace Fishwork.Core {

  /// <summary>
  /// 断言相关异常
  /// </summary>
  internal class AssertionException : FishworkException {
    public AssertionException() { }
    public AssertionException(string message) : base(message) { }
  }

}
