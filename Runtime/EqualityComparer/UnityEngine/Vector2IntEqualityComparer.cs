using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class Vector2IntEqualityComparer : IEqualityComparer<Vector2Int> {
    public static Vector2IntEqualityComparer Default = new();
    
    public bool Equals(Vector2Int self, Vector2Int vector) {
      return self.x.Equals(vector.x) && self.y.Equals(vector.y);
    }

    public int GetHashCode(Vector2Int obj) {
      return obj.x.GetHashCode() ^ obj.y.GetHashCode() << 2;
    }
  }

}
