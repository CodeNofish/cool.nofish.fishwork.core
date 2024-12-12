using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class Vector4EqualityComparer : IEqualityComparer<Vector4> {
    public static Vector4EqualityComparer Default { get; } = new();
    
    public bool Equals(Vector4 self, Vector4 vector) {
      return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z) && self.w.Equals(vector.w);
    }

    public int GetHashCode(Vector4 obj) {
      return obj.x.GetHashCode() ^ obj.y.GetHashCode() << 2 ^ obj.z.GetHashCode() >> 2 ^ obj.w.GetHashCode() >> 1;
    }
  }

}
