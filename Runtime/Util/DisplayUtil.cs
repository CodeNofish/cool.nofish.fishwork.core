using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fishwork.Core {

  /// <summary>
  /// 用于访问显示器和屏幕
  /// </summary>
  public static class DisplayUtil {
    /// <summary>
    /// 获取显示器数量
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetDisplayCount() {
      return Display.displays.Length;
    }

    /// <summary>
    /// 获取指定显示器
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Display GetDisplay(int index = 0) {
      index = Math.Clamp(index, 0, Display.displays.Length - 1);
      return Display.displays[index];
    }

    /// <summary>
    /// 激活指定显示器
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Activate(int index = 0) {
      GetDisplay(index).Activate();
    }

    /// <summary>
    /// 激活全部显示器
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ActivateAll() {
      foreach (var display in Display.displays)
        display.Activate();
    }
  }

}
