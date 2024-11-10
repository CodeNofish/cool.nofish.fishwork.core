using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fishwork.Core {
  
  public static class ScreenUtil {
    /// <summary>
    /// 设置屏幕分辨率
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetResolution(int width, int height, bool fullscreen) {
      Screen.SetResolution(width, height, fullscreen);
    }

    /// <summary>
    /// 设置为竖屏
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetOrientationPortrait() {
      Screen.orientation = ScreenOrientation.Portrait;
    }

    /// <summary>
    /// 设置为横屏
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetOrientationLandscape() {
      Screen.orientation = ScreenOrientation.AutoRotation;
      Screen.autorotateToLandscapeRight = true;
      Screen.autorotateToLandscapeLeft = true;
      Screen.autorotateToPortrait = false;
      Screen.autorotateToPortraitUpsideDown = false;
    }
    
    /// <summary>
    /// 设置为自动旋转
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetOrientationAutorotate() {
      Screen.orientation = ScreenOrientation.AutoRotation;
      Screen.autorotateToLandscapeRight = true;
      Screen.autorotateToLandscapeLeft = true;
      Screen.autorotateToPortrait = true;
      Screen.autorotateToPortraitUpsideDown = true;
    }
  }

  public enum ScreenResolution {
    Auto,
    _2048_1536,
    _1920_1440,
    _1920_1200,
    _1920_1080,
    _1680_1050,
    _1600_1200,
    _1600_900,
    _1440_900,
    _1400_1050,
    _1366_768,
    _1280_1024,
    _1280_960,
    _1280_800,
    _1280_720,
    _1024_768,
    _800_600,
  }
  
}
