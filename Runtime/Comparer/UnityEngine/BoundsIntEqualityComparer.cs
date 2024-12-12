using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class BoundsIntEqualityComparer : IEqualityComparer<BoundsInt> {
    public static BoundsIntEqualityComparer Default { get; } = new();
    
    public bool Equals(BoundsInt self, BoundsInt vector) {
      return Vector3IntEqualityComparer.Default.Equals(self.position, vector.position)
             && Vector3IntEqualityComparer.Default.Equals(self.size, vector.size);
    }

    public int GetHashCode(BoundsInt obj) {
      return Vector3IntEqualityComparer.Default.GetHashCode(obj.position) ^ Vector3IntEqualityComparer.Default.GetHashCode(obj.size) << 2;
    }
  }

}
