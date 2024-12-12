using System.Runtime.CompilerServices;

namespace Fishwork.Core {

  public static class PathUtil {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NormalizePath(this string path) {
      return path.Replace('\\', '/').Trim('/');
    }
  }

}
