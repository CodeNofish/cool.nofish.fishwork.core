using System;

namespace Fishwork.Core {

  /// <summary>
  /// Fishwork内部异常的基类
  /// </summary>
  internal abstract class FishworkException : Exception {
    protected FishworkException() { }
    internal FishworkException(string message) : base(message) { }
  }

}
