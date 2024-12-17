using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fishwork.Core {
  
  public static class EventModifiersExtension {
    /// <summary>
    /// 是否是None
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNone(this EventModifiers modifiers) {
      return modifiers == EventModifiers.None;
    }

    /// <summary>
    /// 是否只有shift
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsShift(this EventModifiers modifiers) {
      return modifiers == EventModifiers.Shift;
    }

    /// <summary>
    /// 是否只有ctrl/command
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCtrl(this EventModifiers modifiers) {
      return PlatformUtil.IsMac
        ? modifiers == EventModifiers.Command
        : modifiers == EventModifiers.Control;
    }

    /// <summary>
    /// 是否只有alt
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlt(this EventModifiers modifiers) {
      return modifiers == EventModifiers.Alt;
    }

    /// <summary>
    /// 是否包含shift
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsShift(this EventModifiers modifiers) {
      return (modifiers & EventModifiers.Shift) != 0;
    }

    /// <summary>
    /// 是否包含alt
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAlt(this EventModifiers modifiers) {
      return (modifiers & EventModifiers.Alt) != 0;
    }

    /// <summary>
    /// 是否包含ctrl/command
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsCtrl(this EventModifiers modifiers) {
      return PlatformUtil.IsMac
        ? (modifiers & EventModifiers.Command) != 0
        : (modifiers & EventModifiers.Control) != 0;
    }
  }

}
