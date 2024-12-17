using System.Runtime.CompilerServices;
using System.Text;

namespace Fishwork.Core {
  
  public static class StringBuilderExtension {
    /// <summary>
    /// StringBuilder中指定字符最后一次出现的位置
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOf(this StringBuilder builder, char value) {
      for (int i = builder.Length - 1; i >= 0; i--) {
        if (builder[i] == value)
          return i;
      }
      return -1;
    }

    /// <summary>
    /// StringBuilder中指定字符首次出现的位置
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOf(this StringBuilder builder, char value) {
      for (int i = 0; i < builder.Length; i++) {
        if (builder[i] == value)
          return i;
      }
      return -1;
    }

    /// <summary>
    /// StringBuilder是否包含某字符
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this StringBuilder builder, char value) {
      for (int i = builder.Length - 1; i >= 0; i--) {
        if (builder[i] == value)
          return true;
      }
      return false;
    }

    /// <summary>
    /// StringBuilder追加一行
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static StringBuilder AddLine(this StringBuilder builder, string line) {
      if (builder.Length > 0)
        builder.Append('\n');
      builder.Append(line);
      return builder;
    }

    /// <summary>
    /// StringBuilder从当前字符串中删除所有尾随出现的字符
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string TrimEnd(this StringBuilder builder, char c) {
      return builder.ToString().TrimEnd(c);
    }
  }

}
