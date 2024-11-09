using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Fishwork.Core {

  [PublicAPI]
  public static class CollectionExtension {
    /// <summary>
    /// 集合是否为空
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
      return collection == null || collection.Count == 0;
    }

    /// <summary>
    /// 如果集合不存在该元素，则添加
    /// </summary>
    /// <returns>是否添加了元素</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AddIfAbsent<T>(this ICollection<T> collection, T item) {
      if (collection.Contains(item))
        return false;
      collection.Add(item);
      return true;
    }
  }

}
