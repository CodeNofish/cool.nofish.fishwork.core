using System;
using System.Collections.Generic;

namespace Fishwork.Core {

  public class TypeEqualityComparer : IEqualityComparer<Type> {
    public static readonly TypeEqualityComparer Default = new();

    public bool Equals(Type x, Type y) {
      if (ReferenceEquals(x, y)) return true;
      return x == y;
    }

    public int GetHashCode(Type obj) {
      return obj.GetHashCode();
    }
  }

}
