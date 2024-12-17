using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class Vector3IntEqualityComparer : IEqualityComparer<Vector3Int> {
    public static Vector3IntEqualityComparer Default = new();

    public bool Equals(Vector3Int self, Vector3Int vector) {
      return self.x.Equals(vector.x) && self.y.Equals(vector.y) && self.z.Equals(vector.z);
    }

    public int GetHashCode(Vector3Int obj) {
      return obj.x.GetHashCode() ^ obj.y.GetHashCode() << 2 ^ obj.z.GetHashCode() >> 2;
    }
  }

}
