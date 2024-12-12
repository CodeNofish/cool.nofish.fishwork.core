using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class Vector3EqualityComparer : IEqualityComparer<Vector3> {
    public static Vector3EqualityComparer Default { get; } = new();
    
    public bool Equals(Vector3 self, Vector3 vector) {
      return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z);
    }

    public int GetHashCode(Vector3 obj) {
      return obj.x.GetHashCode() ^ obj.y.GetHashCode() << 2 ^ obj.z.GetHashCode() >> 2;
    }
  }

}
