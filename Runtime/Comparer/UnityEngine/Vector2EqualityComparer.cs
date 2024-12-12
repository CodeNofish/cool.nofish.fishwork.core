using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class Vector2EqualityComparer : IEqualityComparer<Vector2> {
    public static Vector2EqualityComparer Default { get; } = new();
    
    public bool Equals(Vector2 self, Vector2 vector) {
      return self.x.Equals(vector.x) && self.y.Equals(vector.y);
    }

    public int GetHashCode(Vector2 obj) {
      return obj.x.GetHashCode() ^ obj.y.GetHashCode() << 2;
    }
  }

}
