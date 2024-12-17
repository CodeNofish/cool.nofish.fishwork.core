using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fishwork.Core {

  public static class EnumerableExtension {
    /// <summary>
    /// 获取IEnumerable元素数量，优先使用非枚举方法
    /// </summary>
    public static int GetCount<T>(this IEnumerable<T> source) {
      return source switch {
        ICollection<T> collection => collection.Count,
        ICollection collection2 => collection2.Count,
        _ => source.Count()
      };
    }
  }

}
