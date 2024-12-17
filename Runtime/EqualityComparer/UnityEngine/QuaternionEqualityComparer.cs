using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class QuaternionEqualityComparer : IEqualityComparer<Quaternion> {
    public static QuaternionEqualityComparer Default = new();

    public bool Equals(Quaternion self, Quaternion vector) {
      return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z) && self.w.Equals(vector.w);
    }

    public int GetHashCode(Quaternion obj) {
      return obj.x.GetHashCode() ^ obj.y.GetHashCode() << 2 ^ obj.z.GetHashCode() >> 2 ^ obj.w.GetHashCode() >> 1;
    }
  }

}
