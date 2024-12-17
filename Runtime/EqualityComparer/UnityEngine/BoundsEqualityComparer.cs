using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class BoundsEqualityComparer : IEqualityComparer<Bounds> {
    public static BoundsEqualityComparer Default = new();

    public bool Equals(Bounds self, Bounds vector) {
      return self.center.Equals(vector.center) && self.extents.Equals(vector.extents);
    }

    public int GetHashCode(Bounds obj) {
      return obj.center.GetHashCode() ^ obj.extents.GetHashCode() << 2;
    }
  }

}
