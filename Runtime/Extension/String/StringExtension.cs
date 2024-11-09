using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace Fishwork.Core {

  [PublicAPI]
  public static class StringExtension {
    /// <summary>
    /// 字符串 转换为 UTF8编码的字节数组
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] ToUTF8Bytes(this string text) {
      return Encoding.UTF8.GetBytes(text);
    }

    /// <summary>
    /// UTF8编码的字节数组 转换为 字符串
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToUTF8String(this byte[] bytes) {
      return Encoding.UTF8.GetString(bytes);
    }
  }

}
