using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class ColorEqualityComparer : IEqualityComparer<Color> {
    public static ColorEqualityComparer Default = new();

    public bool Equals(Color self, Color other) {
      return self.r.Equals(other.r) && self.g.Equals(other.g) && self.b.Equals(other.b) && self.a.Equals(other.a);
    }

    public int GetHashCode(Color obj) {
      return obj.r.GetHashCode() ^ obj.g.GetHashCode() << 2 ^ obj.b.GetHashCode() >> 2 ^ obj.a.GetHashCode() >> 1;
    }
  }

}
