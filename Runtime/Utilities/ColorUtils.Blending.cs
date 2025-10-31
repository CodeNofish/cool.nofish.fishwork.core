using UnityEngine;

namespace Fishwork.Core {

  public static partial class ColorUtils {
    public static Color Blend(Color baseColor, Color blendColor, ColorBlendingMode blendMode, Vector2? position = null) {
      // 如果混合色完全透明，直接返回基色
      if (blendColor.a <= 0f)
        return baseColor;

      // 如果混合色完全不透明且不是特殊模式，直接返回混合色
      if (blendColor.a >= 1f && !IsSpecialAlphaMode(blendMode))
        return blendColor;

      return blendMode switch {
        // Normal Group
        ColorBlendingMode.Normal => BlendNormal(baseColor, blendColor),
        ColorBlendingMode.Dissolve => BlendDissolve(baseColor, blendColor, position ?? Vector2.zero),
        ColorBlendingMode.Behind => BlendBehind(baseColor, blendColor),
        ColorBlendingMode.Clear => BlendClear(baseColor, blendColor),
        // Darken Group
        ColorBlendingMode.Darken => BlendDarken(baseColor, blendColor),
        ColorBlendingMode.Multiply => BlendMultiply(baseColor, blendColor),
        ColorBlendingMode.ColorBurn => BlendColorBurn(baseColor, blendColor),
        ColorBlendingMode.LinearBurn => BlendLinearBurn(baseColor, blendColor),
        ColorBlendingMode.DarkerColor => BlendDarkerColor(baseColor, blendColor),
        // Lighten Group
        ColorBlendingMode.Lighten => BlendLighten(baseColor, blendColor),
        ColorBlendingMode.Screen => BlendScreen(baseColor, blendColor),
        ColorBlendingMode.ColorDodge => BlendColorDodge(baseColor, blendColor),
        ColorBlendingMode.LinearDodge => BlendLinearDodge(baseColor, blendColor),
        ColorBlendingMode.LighterColor => BlendLighterColor(baseColor, blendColor),
        // Contrast Group
        ColorBlendingMode.Overlay => BlendOverlay(baseColor, blendColor),
        ColorBlendingMode.SoftLight => BlendSoftLight(baseColor, blendColor),
        ColorBlendingMode.HardLight => BlendHardLight(baseColor, blendColor),
        ColorBlendingMode.VividLight => BlendVividLight(baseColor, blendColor),
        ColorBlendingMode.LinearLight => BlendLinearLight(baseColor, blendColor),
        ColorBlendingMode.PinLight => BlendPinLight(baseColor, blendColor),
        ColorBlendingMode.HardMix => BlendHardMix(baseColor, blendColor),
        // Comparative Group
        ColorBlendingMode.Difference => BlendDifference(baseColor, blendColor),
        ColorBlendingMode.Exclusion => BlendExclusion(baseColor, blendColor),
        ColorBlendingMode.Subtract => BlendSubtract(baseColor, blendColor),
        ColorBlendingMode.Divide => BlendDivide(baseColor, blendColor),
        // HSL Group
        ColorBlendingMode.Hue => BlendHue(baseColor, blendColor),
        ColorBlendingMode.Saturation => BlendSaturation(baseColor, blendColor),
        ColorBlendingMode.Color => BlendColor(baseColor, blendColor),
        ColorBlendingMode.Luminosity => BlendLuminosity(baseColor, blendColor),
        _ => BlendNormal(baseColor, blendColor)
      };
    }

    /// <summary>
    /// 检查是否为需要特殊Alpha处理的模式
    /// </summary>
    private static bool IsSpecialAlphaMode(ColorBlendingMode mode) {
      return mode == ColorBlendingMode.Behind || mode == ColorBlendingMode.Clear ||
             mode == ColorBlendingMode.Dissolve || IsHSLMode(mode);
    }

    /// <summary>
    /// 检查是否为HSL模式
    /// </summary>
    private static bool IsHSLMode(ColorBlendingMode mode) {
      return mode == ColorBlendingMode.Hue || mode == ColorBlendingMode.Saturation ||
             mode == ColorBlendingMode.Color || mode == ColorBlendingMode.Luminosity;
    }


    #region Normal Group

    /// <summary>
    /// Normal blending mode - simply replaces the base color with the blend color based on alpha.
    /// This is the standard blending mode used by most graphics applications.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend on top</param>
    /// <returns>Normal blended color result</returns>
    public static Color BlendNormal(Color baseColor, Color blendColor) {
      // Standard alpha blending: result = blend * alpha + base * (1 - alpha)
      float alpha = blendColor.a;
      float inverseAlpha = 1f - alpha;

      return new Color(
        blendColor.r * alpha + baseColor.r * inverseAlpha,
        blendColor.g * alpha + baseColor.g * inverseAlpha,
        blendColor.b * alpha + baseColor.b * inverseAlpha,
        Mathf.Min(1f, baseColor.a + blendColor.a * inverseAlpha) // Combined alpha
      );
    }

    /// <summary>
    /// Dissolve blending mode - randomly replaces pixels from base color with blend color.
    /// Creates a noisy, grainy effect where the randomness is based on the blend color's alpha.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to dissolve with</param>
    /// <param name="position">Pixel position used for deterministic randomness</param>
    /// <returns>Dissolved color result</returns>
    public static Color BlendDissolve(Color baseColor, Color blendColor, Vector2 position) {
      return BlendDissolve(baseColor, blendColor, position, Random.value);
    }

    /// <summary>
    /// Dissolve blending mode with custom random value for deterministic results.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to dissolve with</param>
    /// <param name="position">Pixel position used for deterministic randomness</param>
    /// <param name="randomValue">Random value (0-1) for deterministic results</param>
    /// <returns>Dissolved color result</returns>
    public static Color BlendDissolve(Color baseColor, Color blendColor, Vector2 position, float randomValue) {
      // Generate deterministic random value based on position
      float hash = HashPosition(position);
      float threshold = blendColor.a;

      // Use the random value to determine if we should use blend color or base color
      if (randomValue < threshold) {
        return blendColor;
      } else {
        return baseColor;
      }
    }

    /// <summary>
    /// Simple hash function for deterministic randomness based on position
    /// </summary>
    private static float HashPosition(Vector2 position) {
      return Mathf.Abs(Mathf.Sin(position.x * 12.9898f + position.y * 78.233f) * 43758.5453f) % 1f;
    }

    /// <summary>
    /// Behind blending mode - applies the blend color behind the base color, only affecting transparent areas.
    /// Similar to painting on the back side of a semi-transparent surface.
    /// </summary>
    /// <param name="baseColor">The underlying (front) color</param>
    /// <param name="blendColor">The color to apply behind</param>
    /// <returns>Color with blend applied behind base</returns>
    public static Color BlendBehind(Color baseColor, Color blendColor) {
      // If base is fully opaque, blend color is completely hidden
      if (baseColor.a >= 1f)
        return baseColor;

      // Calculate how much of the background is visible
      float visibleBackground = 1f - baseColor.a;

      // Blend the background with the blend color
      Color blendedBackground = BlendNormal(blendColor, baseColor);

      // Combine with the base color
      float finalAlpha = baseColor.a + blendColor.a * visibleBackground;

      return new Color(
        blendedBackground.r,
        blendedBackground.g,
        blendedBackground.b,
        Mathf.Min(1f, finalAlpha)
      );
    }

    /// <summary>
    /// Clear blending mode - makes pixels transparent based on the blend color's alpha.
    /// Useful for creating holes or erasing parts of an image.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color used to determine transparency</param>
    /// <returns>Cleared color result</returns>
    public static Color BlendClear(Color baseColor, Color blendColor) {
      // Reduce alpha based on blend color's alpha
      // The more opaque the blend color, the more transparent the result becomes
      float newAlpha = baseColor.a * (1f - blendColor.a);

      return new Color(
        baseColor.r,
        baseColor.g,
        baseColor.b,
        newAlpha
      );
    }

    /// <summary>
    /// Alternative Clear blending that completely removes color in blended areas
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color used as an eraser</param>
    /// <returns>Fully or partially transparent color</returns>
    public static Color BlendClearEraser(Color baseColor, Color blendColor) {
      // Any non-zero alpha in blend color makes the corresponding area transparent
      if (blendColor.a > 0f) {
        return new Color(baseColor.r, baseColor.g, baseColor.b, 0f);
      }

      return baseColor;
    }

    #endregion


    #region Darken Group

    /// <summary>
    /// Darken blending mode - selects the darker of the base and blend colors for each channel.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>The darker color component for each channel</returns>
    public static Color BlendDarken(Color baseColor, Color blendColor) {
      return new Color(
        Mathf.Min(baseColor.r, blendColor.r),
        Mathf.Min(baseColor.g, blendColor.g),
        Mathf.Min(baseColor.b, blendColor.b),
        Mathf.Min(baseColor.a, blendColor.a)
      );
    }

    /// <summary>
    /// Multiply blending mode - multiplies the base and blend color values, resulting in a darker color.
    /// Similar to placing two photographic slides on top of each other.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Multiplied color result</returns>
    public static Color BlendMultiply(Color baseColor, Color blendColor) {
      return new Color(
        baseColor.r * blendColor.r,
        baseColor.g * blendColor.g,
        baseColor.b * blendColor.b,
        baseColor.a * blendColor.a
      );
    }

    /// <summary>
    /// Color Burn blending mode - darkens the base color by increasing contrast with the blend color.
    /// Produces more intense darkening than Multiply.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Color burned result</returns>
    public static Color BlendColorBurn(Color baseColor, Color blendColor) {
      float BurnChannel(float baseVal, float blendVal) {
        if (blendVal <= 0f) return 0f;
        float result = 1f - (1f - baseVal) / blendVal;
        return Mathf.Clamp01(result);
      }

      return new Color(
        BurnChannel(baseColor.r, blendColor.r),
        BurnChannel(baseColor.g, blendColor.g),
        BurnChannel(baseColor.b, blendColor.b),
        Mathf.Min(baseColor.a, blendColor.a)
      );
    }

    /// <summary>
    /// Linear Burn blending mode - darkens by subtracting the blend color from the base color.
    /// Produces darker results than Multiply but preserves more detail than Color Burn.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Linearly burned color result</returns>
    public static Color BlendLinearBurn(Color baseColor, Color blendColor) {
      float LinearBurnChannel(float baseVal, float blendVal) {
        float result = baseVal + blendVal - 1f;
        return Mathf.Clamp01(result);
      }

      return new Color(
        LinearBurnChannel(baseColor.r, blendColor.r),
        LinearBurnChannel(baseColor.g, blendColor.g),
        LinearBurnChannel(baseColor.b, blendColor.b),
        Mathf.Min(baseColor.a, blendColor.a)
      );
    }

    /// <summary>
    /// Darker Color blending mode - compares the luminance of base and blend colors, 
    /// and selects the color with the darker overall luminance.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>The darker color based on luminance comparison</returns>
    public static Color BlendDarkerColor(Color baseColor, Color blendColor) {
      // Calculate luminance using standard relative luminance formula
      float baseLuminance = 0.2126f * baseColor.r + 0.7152f * baseColor.g + 0.0722f * baseColor.b;
      float blendLuminance = 0.2126f * blendColor.r + 0.7152f * blendColor.g + 0.0722f * blendColor.b;

      return baseLuminance < blendLuminance ? baseColor : blendColor;
    }

    #endregion


    #region Lighten Group

    /// <summary>
    /// Lighten blending mode - selects the lighter of the base and blend colors for each channel.
    /// Opposite of Darken mode.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>The lighter color component for each channel</returns>
    public static Color BlendLighten(Color baseColor, Color blendColor) {
      return new Color(
        Mathf.Max(baseColor.r, blendColor.r),
        Mathf.Max(baseColor.g, blendColor.g),
        Mathf.Max(baseColor.b, blendColor.b),
        Mathf.Max(baseColor.a, blendColor.a)
      );
    }

    /// <summary>
    /// Screen blending mode - inverts, multiplies, and inverts again, resulting in a lighter color.
    /// Opposite of Multiply mode. Similar to projecting multiple slide images onto the same screen.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Screened color result</returns>
    public static Color BlendScreen(Color baseColor, Color blendColor) {
      float ScreenChannel(float baseVal, float blendVal) {
        return 1f - (1f - baseVal) * (1f - blendVal);
      }

      return new Color(
        ScreenChannel(baseColor.r, blendColor.r),
        ScreenChannel(baseColor.g, blendColor.g),
        ScreenChannel(baseColor.b, blendColor.b),
        Mathf.Max(baseColor.a, blendColor.a)
      );
    }

    /// <summary>
    /// Color Dodge blending mode - brightens the base color by decreasing contrast with the blend color.
    /// Opposite of Color Burn mode. Produces strong lightening effects.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Color dodged result</returns>
    public static Color BlendColorDodge(Color baseColor, Color blendColor) {
      float ColorDodgeChannel(float baseVal, float blendVal) {
        if (blendVal >= 1f) return 1f;
        float result = baseVal / (1f - blendVal);
        return Mathf.Clamp01(result);
      }

      return new Color(
        ColorDodgeChannel(baseColor.r, blendColor.r),
        ColorDodgeChannel(baseColor.g, blendColor.g),
        ColorDodgeChannel(baseColor.b, blendColor.b),
        Mathf.Max(baseColor.a, blendColor.a)
      );
    }

    /// <summary>
    /// Linear Dodge (Add) blending mode - brightens by adding the blend color to the base color.
    /// Opposite of Linear Burn mode. Simple additive blending.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Linearly dodged (added) color result</returns>
    public static Color BlendLinearDodge(Color baseColor, Color blendColor) {
      float LinearDodgeChannel(float baseVal, float blendVal) {
        float result = baseVal + blendVal;
        return Mathf.Clamp01(result);
      }

      return new Color(
        LinearDodgeChannel(baseColor.r, blendColor.r),
        LinearDodgeChannel(baseColor.g, blendColor.g),
        LinearDodgeChannel(baseColor.b, blendColor.b),
        Mathf.Max(baseColor.a, blendColor.a)
      );
    }

    /// <summary>
    /// Lighter Color blending mode - compares the luminance of base and blend colors, 
    /// and selects the color with the lighter overall luminance.
    /// Opposite of Darker Color mode.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>The lighter color based on luminance comparison</returns>
    public static Color BlendLighterColor(Color baseColor, Color blendColor) {
      // Calculate luminance using standard relative luminance formula
      float baseLuminance = 0.2126f * baseColor.r + 0.7152f * baseColor.g + 0.0722f * baseColor.b;
      float blendLuminance = 0.2126f * blendColor.r + 0.7152f * blendColor.g + 0.0722f * blendColor.b;

      return baseLuminance > blendLuminance ? baseColor : blendColor;
    }

    #endregion


    #region Contrast Group

    /// <summary>
    /// Overlay blending mode - applies Multiply to darker areas and Screen to lighter areas.
    /// Uses the base color as the condition for choosing between Multiply and Screen.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Overlay blended color result</returns>
    public static Color BlendOverlay(Color baseColor, Color blendColor) {
      float OverlayChannel(float baseVal, float blendVal) {
        return baseVal < 0.5f
          ? 2f * baseVal * blendVal
          : // Multiply for dark areas
          1f - 2f * (1f - baseVal) * (1f - blendVal); // Screen for light areas
      }

      return new Color(
        OverlayChannel(baseColor.r, blendColor.r),
        OverlayChannel(baseColor.g, blendColor.g),
        OverlayChannel(baseColor.b, blendColor.b),
        baseColor.a
      );
    }

    /// <summary>
    /// Soft Light blending mode - softer version of Overlay, similar to diffused spotlight.
    /// Applies darkening or lightening based on blend color with softer transitions.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Soft light blended color result</returns>
    public static Color BlendSoftLight(Color baseColor, Color blendColor) {
      float SoftLightChannel(float baseVal, float blendVal) {
        if (blendVal <= 0.5f) {
          return baseVal - (1f - 2f * blendVal) * baseVal * (1f - baseVal);
        } else {
          float d = baseVal < 0.25f ? ((16f * baseVal - 12f) * baseVal + 4f) * baseVal : Mathf.Sqrt(baseVal);
          return baseVal + (2f * blendVal - 1f) * (d - baseVal);
        }
      }

      return new Color(
        SoftLightChannel(baseColor.r, blendColor.r),
        SoftLightChannel(baseColor.g, blendColor.g),
        SoftLightChannel(baseColor.b, blendColor.b),
        baseColor.a
      );
    }

    /// <summary>
    /// Hard Light blending mode - similar to Overlay but uses blend color as the condition.
    /// Effectively combines Multiply and Screen based on blend color luminance.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Hard light blended color result</returns>
    public static Color BlendHardLight(Color baseColor, Color blendColor) {
      float HardLightChannel(float baseVal, float blendVal) {
        return blendVal < 0.5f
          ? 2f * baseVal * blendVal
          : // Multiply for dark blend colors
          1f - 2f * (1f - baseVal) * (1f - blendVal); // Screen for light blend colors
      }

      return new Color(
        HardLightChannel(baseColor.r, blendColor.r),
        HardLightChannel(baseColor.g, blendColor.g),
        HardLightChannel(baseColor.b, blendColor.b),
        baseColor.a
      );
    }

    /// <summary>
    /// Vivid Light blending mode - combines Color Burn and Color Dodge based on blend color luminance.
    /// Creates more intense contrast effects than Overlay or Hard Light.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Vivid light blended color result</returns>
    public static Color BlendVividLight(Color baseColor, Color blendColor) {
      float VividLightChannel(float baseVal, float blendVal) {
        if (blendVal <= 0.5f) {
          // Color Burn for dark areas
          if (blendVal <= 0f) return 0f;
          return 1f - (1f - baseVal) / (2f * blendVal);
        } else {
          // Color Dodge for light areas
          if (blendVal >= 1f) return 1f;
          return baseVal / (2f * (1f - blendVal));
        }
      }

      return new Color(
        Mathf.Clamp01(VividLightChannel(baseColor.r, blendColor.r)),
        Mathf.Clamp01(VividLightChannel(baseColor.g, blendColor.g)),
        Mathf.Clamp01(VividLightChannel(baseColor.b, blendColor.b)),
        baseColor.a
      );
    }

    /// <summary>
    /// Linear Light blending mode - combines Linear Burn and Linear Dodge based on blend color luminance.
    /// Creates strong linear contrast effects.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Linear light blended color result</returns>
    public static Color BlendLinearLight(Color baseColor, Color blendColor) {
      float LinearLightChannel(float baseVal, float blendVal) {
        return blendVal <= 0.5f
          ? baseVal + 2f * blendVal - 1f
          : // Linear Burn
          baseVal + 2f * (blendVal - 0.5f); // Linear Dodge
      }

      return new Color(
        Mathf.Clamp01(LinearLightChannel(baseColor.r, blendColor.r)),
        Mathf.Clamp01(LinearLightChannel(baseColor.g, blendColor.g)),
        Mathf.Clamp01(LinearLightChannel(baseColor.b, blendColor.b)),
        baseColor.a
      );
    }

    /// <summary>
    /// Pin Light blending mode - combines Darken and Lighten based on blend color luminance.
    /// Replaces colors based on threshold comparisons.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Pin light blended color result</returns>
    public static Color BlendPinLight(Color baseColor, Color blendColor) {
      float PinLightChannel(float baseVal, float blendVal) {
        if (blendVal <= 0.5f) {
          return Mathf.Min(baseVal, 2f * blendVal); // Darken variant
        } else {
          return Mathf.Max(baseVal, 2f * (blendVal - 0.5f)); // Lighten variant
        }
      }

      return new Color(
        Mathf.Clamp01(PinLightChannel(baseColor.r, blendColor.r)),
        Mathf.Clamp01(PinLightChannel(baseColor.g, blendColor.g)),
        Mathf.Clamp01(PinLightChannel(baseColor.b, blendColor.b)),
        baseColor.a
      );
    }

    /// <summary>
    /// Hard Mix blending mode - creates extreme posterization by thresholding to pure black or white.
    /// Results in only 8 possible colors (primary colors + black/white).
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Hard mix blended color result (only 0 or 1 values)</returns>
    public static Color BlendHardMix(Color baseColor, Color blendColor) {
      float HardMixChannel(float baseVal, float blendVal) {
        // Use Vivid Light as base, then threshold to 0 or 1
        float vividResult = BlendVividLight(new Color(baseVal, 0, 0, 1), new Color(blendVal, 0, 0, 1)).r;
        return vividResult < 0.5f ? 0f : 1f;
      }

      return new Color(
        HardMixChannel(baseColor.r, blendColor.r),
        HardMixChannel(baseColor.g, blendColor.g),
        HardMixChannel(baseColor.b, blendColor.b),
        baseColor.a
      );
    }

    #endregion


    #region Comparative Group

    /// <summary>
    /// Difference blending mode - subtracts the darker color from the lighter color for each channel.
    /// Creates inversion effects where similar colors become black, and different colors become visible.
    /// Useful for comparing images and creating psychedelic effects.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Absolute difference between colors</returns>
    public static Color BlendDifference(Color baseColor, Color blendColor) {
      float DifferenceChannel(float baseVal, float blendVal) {
        return Mathf.Abs(baseVal - blendVal);
      }

      return new Color(
        DifferenceChannel(baseColor.r, blendColor.r),
        DifferenceChannel(baseColor.g, blendColor.g),
        DifferenceChannel(baseColor.b, blendColor.b),
        baseColor.a
      );
    }

    /// <summary>
    /// Exclusion blending mode - similar to Difference but with lower contrast.
    /// Creates softer inversion effects where midtones become gray instead of black.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Exclusion blended color result</returns>
    public static Color BlendExclusion(Color baseColor, Color blendColor) {
      float ExclusionChannel(float baseVal, float blendVal) {
        return baseVal + blendVal - 2f * baseVal * blendVal;
      }

      return new Color(
        ExclusionChannel(baseColor.r, blendColor.r),
        ExclusionChannel(baseColor.g, blendColor.g),
        ExclusionChannel(baseColor.b, blendColor.b),
        baseColor.a
      );
    }

    /// <summary>
    /// Subtract blending mode - subtracts the blend color from the base color.
    /// Creates darkening effects that can produce negative values (clamped to 0).
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Subtracted color result</returns>
    public static Color BlendSubtract(Color baseColor, Color blendColor) {
      float SubtractChannel(float baseVal, float blendVal) {
        return Mathf.Clamp01(baseVal - blendVal);
      }

      return new Color(
        SubtractChannel(baseColor.r, blendColor.r),
        SubtractChannel(baseColor.g, blendColor.g),
        SubtractChannel(baseColor.b, blendColor.b),
        Mathf.Clamp01(baseColor.a - blendColor.a)
      );
    }

    /// <summary>
    /// Divide blending mode - divides the base color by the blend color.
    /// Brightens the image, with extreme results when dividing by dark colors.
    /// White has no effect, black causes extreme brightening (protected against division by zero).
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color to blend with</param>
    /// <returns>Divided color result</returns>
    public static Color BlendDivide(Color baseColor, Color blendColor) {
      float DivideChannel(float baseVal, float blendVal) {
        // Protect against division by zero
        if (Mathf.Approximately(blendVal, 0f)) {
          return blendVal > 0f ? Mathf.Infinity : 0f;
        }
        return Mathf.Clamp01(baseVal / blendVal);
      }

      return new Color(
        DivideChannel(baseColor.r, blendColor.r),
        DivideChannel(baseColor.g, blendColor.g),
        DivideChannel(baseColor.b, blendColor.b),
        baseColor.a
      );
    }

    #endregion


    #region HSL Group

    /// <summary>
    /// Converts RGB color to HSL color space
    /// </summary>
    /// <param name="rgb">RGB color (0-1 range)</param>
    /// <returns>HSL color as Vector4 (x:hue, y:saturation, z:lightness, w:alpha)</returns>
    private static Vector4 RgbToHsl(Color rgb) {
      float r = rgb.r;
      float g = rgb.g;
      float b = rgb.b;

      float max = Mathf.Max(r, Mathf.Max(g, b));
      float min = Mathf.Min(r, Mathf.Min(g, b));
      float delta = max - min;

      float h = 0f;
      float s = 0f;
      float l = (max + min) / 2f;

      if (delta > 0.0001f) {
        s = l < 0.5f ? delta / (max + min) : delta / (2f - max - min);

        if (Mathf.Approximately(max, r))
          h = (g - b) / delta + (g < b ? 6f : 0f);
        else if (Mathf.Approximately(max, g))
          h = (b - r) / delta + 2f;
        else
          h = (r - g) / delta + 4f;

        h /= 6f;
      }

      return new Vector4(h, s, l, rgb.a);
    }

    /// <summary>
    /// Converts HSL color to RGB color space
    /// </summary>
    /// <param name="hsl">HSL color as Vector4 (x:hue, y:saturation, z:lightness, w:alpha)</param>
    /// <returns>RGB color (0-1 range)</returns>
    private static Color HslToRgb(Vector4 hsl) {
      float h = hsl.x;
      float s = hsl.y;
      float l = hsl.z;
      float a = hsl.w;

      if (s <= 0.0001f)
        return new Color(l, l, l, a);

      float q = l < 0.5f ? l * (1f + s) : l + s - l * s;
      float p = 2f * l - q;

      float HueToRgb(float t) {
        t = (t < 0f) ? t + 1f : (t > 1f) ? t - 1f : t;

        if (t < 1f / 6f) return p + (q - p) * 6f * t;
        if (t < 1f / 2f) return q;
        if (t < 2f / 3f) return p + (q - p) * (2f / 3f - t) * 6f;
        return p;
      }

      return new Color(
        HueToRgb(h + 1f / 3f),
        HueToRgb(h),
        HueToRgb(h - 1f / 3f),
        a
      );
    }

    /// <summary>
    /// Hue blending mode - applies the hue of the blend color to the base color,
    /// while preserving the saturation and luminosity of the base color.
    /// Useful for colorizing images while maintaining original brightness and intensity.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color providing the hue</param>
    /// <returns>Color with blend's hue and base's saturation/luminosity</returns>
    public static Color BlendHue(Color baseColor, Color blendColor) {
      Vector4 baseHsl = RgbToHsl(baseColor);
      Vector4 blendHsl = RgbToHsl(blendColor);

      // Keep base saturation and lightness, use blend hue
      Vector4 resultHsl = new Vector4(blendHsl.x, baseHsl.y, baseHsl.z, baseHsl.w);
      return HslToRgb(resultHsl);
    }

    /// <summary>
    /// Saturation blending mode - applies the saturation of the blend color to the base color,
    /// while preserving the hue and luminosity of the base color.
    /// Useful for intensifying or reducing color intensity without changing the actual color.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color providing the saturation</param>
    /// <returns>Color with blend's saturation and base's hue/luminosity</returns>
    public static Color BlendSaturation(Color baseColor, Color blendColor) {
      Vector4 baseHsl = RgbToHsl(baseColor);
      Vector4 blendHsl = RgbToHsl(blendColor);

      // Keep base hue and lightness, use blend saturation
      Vector4 resultHsl = new Vector4(baseHsl.x, blendHsl.y, baseHsl.z, baseHsl.w);
      return HslToRgb(resultHsl);
    }

    /// <summary>
    /// Color blending mode (also called "Color" in Photoshop) - applies the hue and saturation 
    /// of the blend color to the base color, while preserving the luminosity of the base color.
    /// Useful for colorizing images while maintaining original brightness levels.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color providing the hue and saturation</param>
    /// <returns>Color with blend's hue/saturation and base's luminosity</returns>
    public static Color BlendColor(Color baseColor, Color blendColor) {
      Vector4 baseHsl = RgbToHsl(baseColor);
      Vector4 blendHsl = RgbToHsl(blendColor);

      // Keep base lightness, use blend hue and saturation
      Vector4 resultHsl = new Vector4(blendHsl.x, blendHsl.y, baseHsl.z, baseHsl.w);
      return HslToRgb(resultHsl);
    }

    /// <summary>
    /// Luminosity blending mode - applies the luminosity of the blend color to the base color,
    /// while preserving the hue and saturation of the base color.
    /// Useful for applying brightness patterns without changing the actual colors.
    /// </summary>
    /// <param name="baseColor">The underlying color</param>
    /// <param name="blendColor">The color providing the luminosity</param>
    /// <returns>Color with blend's luminosity and base's hue/saturation</returns>
    public static Color BlendLuminosity(Color baseColor, Color blendColor) {
      Vector4 baseHsl = RgbToHsl(baseColor);
      Vector4 blendHsl = RgbToHsl(blendColor);

      // Keep base hue and saturation, use blend lightness
      Vector4 resultHsl = new Vector4(baseHsl.x, baseHsl.y, blendHsl.z, baseHsl.w);
      return HslToRgb(resultHsl);
    }

    #endregion
  }

}
