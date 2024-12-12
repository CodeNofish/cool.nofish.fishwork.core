using System.Collections.Generic;

namespace Fishwork.Core {

  public class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class  {
    public static ReferenceEqualityComparer<T>  Default { get; } = new();

    public bool Equals(T x, T y) {
      return ReferenceEquals(x, y);
    }

    public int GetHashCode(T obj) {
      try {
        return obj.GetHashCode();
      } catch {
        return -1;
      }
    }
  }

}
