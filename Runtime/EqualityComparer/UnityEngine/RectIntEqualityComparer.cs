using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class RectIntEqualityComparer : IEqualityComparer<RectInt> {
    public static RectIntEqualityComparer Default = new();
    
    public bool Equals(RectInt self, RectInt other) {
      return self.x.Equals(other.x) && self.width.Equals(other.width) && self.y.Equals(other.y) && self.height.Equals(other.height);
    }

    public int GetHashCode(RectInt obj) {
      return obj.x.GetHashCode() ^ obj.width.GetHashCode() << 2 ^ obj.y.GetHashCode() >> 2 ^ obj.height.GetHashCode() >> 1;
    }
  }

}
