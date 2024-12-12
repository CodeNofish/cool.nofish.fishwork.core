using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class RectEqualityComparer : IEqualityComparer<Rect> {
    public static ColorEqualityComparer Default { get; } = new();

    public bool Equals(Rect self, Rect other) {
      return self.x.Equals(other.x) && self.width.Equals(other.width) && self.y.Equals(other.y) && self.height.Equals(other.height);
    }

    public int GetHashCode(Rect obj) {
      return obj.x.GetHashCode() ^ obj.width.GetHashCode() << 2 ^ obj.y.GetHashCode() >> 2 ^ obj.height.GetHashCode() >> 1;
    }
  }

}
