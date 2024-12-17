using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class RangeIntEqualityComparer : IEqualityComparer<RangeInt> {
    public static RangeIntEqualityComparer Default = new();
    
    public bool Equals(RangeInt self, RangeInt vector) {
      return self.start.Equals(vector.start) && self.length.Equals(vector.length);
    }

    public int GetHashCode(RangeInt obj) {
      return obj.start.GetHashCode() ^ obj.length.GetHashCode() << 2;
    }
  }

}
