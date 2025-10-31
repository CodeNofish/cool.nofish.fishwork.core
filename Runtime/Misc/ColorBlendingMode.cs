namespace Fishwork.Core {

  // https://helpx.adobe.com/photoshop/desktop/repair-retouch/adjust-light-tone/blending-mode-descriptions.html

  public enum ColorBlendingMode {
    #region Normal Group

    // These modes either replace or randomly mix pixels without complex calculations.

    Normal,

    Dissolve,

    Behind,

    Clear,

    #endregion


    #region Darken Group

    // These modes create darker results by preserving shadows, midtones, or dark colors from either the base or blend color.

    Darken,
    Multiply,
    ColorBurn,
    LinearBurn,
    DarkerColor,

    #endregion


    #region Lighten Group

    // These modes create lighter results by preserving highlights, midtones, or light colors from either the base or blend color.

    Lighten,
    Screen,
    ColorDodge,
    LinearDodge,
    LighterColor,

    #endregion


    #region Contrast Group

    // These modes increase contrast by darkening areas darker than 50% gray and lightening areas lighter than 50% gray.

    Overlay,
    SoftLight,
    HardLight,
    VividLight,
    LinearLight,
    PinLight,
    HardMix,

    #endregion


    #region Comparative Group

    // These modes create results based on comparing or inverting colors between layers.

    Difference,
    Exclusion,
    Subtract,
    Divide,

    #endregion


    #region HSL Group

    // These modes affect specific HSL (Hue, Saturation, Luminosity) components of colors.

    Hue,
    Saturation,
    Color,
    Luminosity,

    #endregion
  }

}
