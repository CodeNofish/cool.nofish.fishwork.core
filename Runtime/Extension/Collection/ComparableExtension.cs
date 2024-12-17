using System;
using System.Runtime.CompilerServices;

namespace Fishwork.Core {

  public static class ComparableExtension {
    /// <summary>
    /// 小于
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IComparable<T> x, T y) {
      return x.CompareTo(y) < 0;
    }

    /// <summary>
    /// 小于等于
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqual<T>(this IComparable<T> x, T y) {
      return x.CompareTo(y) <= 0;
    }

    /// <summary>
    /// 相等
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqual<T>(this IComparable<T> x, T y) {
      return x.CompareTo(y) == 0;
    }

    /// <summary>
    /// 大于
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsGreatThan<T>(this IComparable<T> x, T y) {
      return x.CompareTo(y) > 0;
    }

    /// <summary>
    /// 大于等于
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsGreatThanOrEqual<T>(this IComparable<T> x, T y) {
      return x.CompareTo(y) >= 0;
    }
  }

}
