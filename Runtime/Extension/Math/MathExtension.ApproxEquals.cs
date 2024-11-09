using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Fishwork.Core {

  [PublicAPI]
  public static class MathExtension {
    /// <summary>
    /// 两个单精度浮点数是否近似相等
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ApproxEquals(this float f1, float f2) {
      return Mathf.Approximately(f1, f2);
    }

    /// <summary>
    /// 两个单精度浮点数是否近似不相等
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotApproxEquals(this float f1, float f2) {
      return !Mathf.Approximately(f1, f2);
    }

    /// <summary>
    /// 两个双精度浮点数是否近似相等
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ApproxEquals(this double d1, double d2) {
      return MathUtil.ApproxEquals(d1, d2);
    }

    /// <summary>
    /// 两个双精度浮点数是否近似不相等
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NotApproxEquals(this double d1, double d2) {
      return MathUtil.ApproxEquals(d1, d2);
    }
  }

}
