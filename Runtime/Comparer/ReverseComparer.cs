using System;
using System.Collections.Generic;

namespace Fishwork.Core {

  public class ReverseComparer<T> : IComparer<T> where T : IComparable<T> {
    public static ReverseComparer<T> Default = new();

    public int Compare(T x, T y) {
      return y.CompareTo(x);
    }
  }

}
