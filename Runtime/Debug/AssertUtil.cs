using System.Diagnostics;

namespace Fishwork.Core {

  public static class AssertUtil {
    #region 断言
    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void Assert(bool condition) {
      if (!condition) throw new AssertionException();
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void Assert(bool condition, string message) {
      if (!condition) throw new AssertionException(message);
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void Assert(bool condition, string format, params object[] args) {
      if (!condition) throw new AssertionException(string.Format(format, args));
    }
    #endregion

    #region 失败断言
    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void Fail(string message) {
      throw new AssertionException(message);
    }

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public static void Fail(string format, params object[] args) {
      throw new AssertionException(string.Format(format, args));
    }
    #endregion
  }

}
