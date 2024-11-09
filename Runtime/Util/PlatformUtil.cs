using JetBrains.Annotations;
using UnityEngine;

namespace Fishwork.Core {

  [PublicAPI]
  public static class PlatformUtil {
    /// <summary>
    /// 当前平台是否是Mac
    /// </summary>
    public static readonly bool IsMac;

    static PlatformUtil() {
      IsMac = Application.platform == RuntimePlatform.OSXEditor
              || Application.platform == RuntimePlatform.OSXPlayer
              || Application.platform == RuntimePlatform.OSXServer;
    }
  }

}
