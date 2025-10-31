using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fishwork.Core {

  /// <summary>
  /// Provides extension methods for manipulating Unity Color structures.
  /// </summary>
  public static class ColorExtensions {
    #region Color Component Manipulation

    /// <summary>
    /// Returns a new Color with the red component set to the specified value.
    /// </summary>
    /// <param name="color">The original color</param>
    /// <param name="red">The red component value (0-1)</param>
    /// <returns>A new Color with the modified red component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithRed(this Color color, float red) {
      color.r = red;
      return color;
    }

    /// <summary>
    /// Returns a new Color with the green component set to the specified value.
    /// </summary>
    /// <param name="color">The original color</param>
    /// <param name="green">The green component value (0-1)</param>
    /// <returns>A new Color with the modified green component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithGreen(this Color color, float green) {
      color.g = green;
      return color;
    }

    /// <summary>
    /// Returns a new Color with the blue component set to the specified value.
    /// </summary>
    /// <param name="color">The original color</param>
    /// <param name="blue">The blue component value (0-1)</param>
    /// <returns>A new Color with the modified blue component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithBlue(this Color color, float blue) {
      color.b = blue;
      return color;
    }

    /// <summary>
    /// Returns a new Color with the alpha (transparency) component set to the specified value.
    /// </summary>
    /// <param name="color">The original color</param>
    /// <param name="alpha">The alpha component value (0-1, where 0 is fully transparent, 1 is fully opaque)</param>
    /// <returns>A new Color with the modified alpha component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithAlpha(this Color color, float alpha) {
      color.a = alpha;
      return color;
    }

    #endregion


    #region 颜色操作

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Blend(this Color color, Color other, float wight = 0.5f) {
      return Color.Lerp(color, other, wight);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Invert(this Color color, bool preserveAlpha = true) {
      return new Color(
        1.0f - color.r,
        1.0f - color.g,
        1.0f - color.b,
        preserveAlpha ? color.a : 1.0f - color.a
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Grayscale(this Color color) {
      return 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToGrayscale(this Color color) {
      float gray = color.Grayscale();
      return new Color(gray, gray, gray, color.a);
    }

    #endregion


    #region 颜色和字符串转换

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHex(this Color color, bool includeAlpha = true) {
      return includeAlpha
        ? ColorUtility.ToHtmlStringRGBA(color)
        : ColorUtility.ToHtmlStringRGB(color);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color FromHex(this Color color, string hex) {
      ColorUtility.TryParseHtmlString(hex, out color);
      return color;
    }

    #endregion


    public static bool Approximately(this Color color, Color other, float tolerance = 0.001f) {
      return Mathf.Abs(color.r - other.r) < tolerance &&
             Mathf.Abs(color.g - other.g) < tolerance &&
             Mathf.Abs(color.b - other.b) < tolerance &&
             Mathf.Abs(color.a - other.a) < tolerance;
    }

    // public static string ToString(this Color color) { }
    //
    // private static string TrimFloat(float value) {
    //   var stringValue = value.ToString("F3", CultureInfo.InvariantCulture).TrimEnd("0");
    //
    // }
  }

}
