using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fishwork.Core {

  public static class ApplicationUtil {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetFrameRate(int frameRate) {
      Application.targetFrameRate = frameRate;
    }
  }

}
