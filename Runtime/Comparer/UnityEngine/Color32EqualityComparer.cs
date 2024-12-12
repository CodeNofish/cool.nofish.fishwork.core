using System.Collections.Generic;
using UnityEngine;

namespace Fishwork.Core {

  public class Color32EqualityComparer : IEqualityComparer<Color32> {
    public static Color32EqualityComparer Default { get; } = new();

    public bool Equals(Color32 self, Color32 vector) {
      return self.a.Equals(vector.a) && self.r.Equals(vector.r) && self.g.Equals(vector.g) && self.b.Equals(vector.b);
    }

    public int GetHashCode(Color32 obj) {
      return obj.a.GetHashCode() ^ obj.r.GetHashCode() << 2 ^ obj.g.GetHashCode() >> 2 ^ obj.b.GetHashCode() >> 1;
    }
  }

}
