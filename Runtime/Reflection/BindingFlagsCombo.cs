using System.Reflection;

namespace Fishwork.Core {

  /// <summary>
  /// Provides predefined combinations of BindingFlags for common reflection scenarios.
  /// This class organizes binding flag combinations into three categories based on search scope.
  /// </summary>
  public static class BindingFlagsCombo {
    #region Without specifying a search scope, only consider access modifiers and member types.

    /// <summary>
    /// Combination for public static members
    /// </summary>
    public const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;

    /// <summary>
    /// Combination for public instance members
    /// </summary>
    public const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;

    /// <summary>
    /// Combination for non-public static members
    /// </summary>
    public const BindingFlags NonPublicStatic = BindingFlags.NonPublic | BindingFlags.Static;

    /// <summary>
    /// Combination for non-public instance members
    /// </summary>
    public const BindingFlags NonPublicInstance = BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Combination for members with any visibility (public or non-public)
    /// </summary>
    public const BindingFlags AnyVisibility = BindingFlags.Public | BindingFlags.Instance;

    /// <summary>
    /// Combination for any static members regardless of visibility
    /// </summary>
    public const BindingFlags AnyStatic = AnyVisibility | BindingFlags.Static;

    /// <summary>
    /// Combination for any instance members regardless of visibility
    /// </summary>
    public const BindingFlags AnyInstance = AnyVisibility | BindingFlags.Instance;

    #endregion


    #region Consider the DeclaredOnly search scope.

    /// <summary>
    /// Combination for public static members declared only in the current type (not inherited)
    /// </summary>
    public const BindingFlags PublicStaticDeclaredOnly = PublicStatic | BindingFlags.DeclaredOnly;

    /// <summary>
    /// Combination for public instance members declared only in the current type (not inherited)
    /// </summary>
    public const BindingFlags PublicInstanceDeclaredOnly = PublicInstance | BindingFlags.DeclaredOnly;

    /// <summary>
    /// Combination for non-public static members declared only in the current type (not inherited)
    /// </summary>
    public const BindingFlags NonPublicStaticDeclaredOnly = NonPublicStatic | BindingFlags.DeclaredOnly;

    /// <summary>
    /// Combination for non-public instance members declared only in the current type (not inherited)
    /// </summary>
    public const BindingFlags NonPublicInstanceDeclaredOnly = NonPublicInstance | BindingFlags.DeclaredOnly;

    /// <summary>
    /// Combination for members with any visibility declared only in the current type (not inherited)
    /// </summary>
    public const BindingFlags AnyVisibilityDeclaredOnly = AnyVisibility | BindingFlags.DeclaredOnly;

    /// <summary>
    /// Combination for any static members with any visibility declared only in the current type (not inherited)
    /// </summary>
    public const BindingFlags AnyStaticDeclaredOnly = AnyVisibilityDeclaredOnly | BindingFlags.Static;

    /// <summary>
    /// Combination for any instance members with any visibility declared only in the current type (not inherited)
    /// </summary>
    public const BindingFlags AnyInstanceDeclaredOnly = AnyVisibilityDeclaredOnly | BindingFlags.Instance;

    #endregion


    #region Consider the FlattenHierarchy search scope.

    /// <summary>
    /// Combines PublicStatic with FlattenHierarchy to search for public static members,
    /// including inherited public static members from base classes.
    /// </summary>
    public const BindingFlags PublicStaticFlattenHierarchy = PublicStatic | BindingFlags.FlattenHierarchy;

    /// <summary>
    /// Combines PublicInstance with FlattenHierarchy to search for public instance members,
    /// including inherited public instance members from base classes.
    /// </summary>
    public const BindingFlags PublicInstanceFlattenHierarchy = PublicInstance | BindingFlags.FlattenHierarchy;

    /// <summary>
    /// Combines NonPublicStatic with FlattenHierarchy to search for non-public static members,
    /// including inherited non-public static members from base classes.
    /// </summary>
    public const BindingFlags NonPublicStaticFlattenHierarchy = NonPublicStatic | BindingFlags.FlattenHierarchy;

    /// <summary>
    /// Combines NonPublicInstance with FlattenHierarchy to search for non-public instance members,
    /// including inherited non-public instance members from base classes.
    /// </summary>
    public const BindingFlags NonPublicInstanceFlattenHierarchy = NonPublicInstance | BindingFlags.FlattenHierarchy;

    /// <summary>
    /// Combines AnyVisibility with FlattenHierarchy to search for members of any visibility (public and non-public),
    /// including inherited members from base classes.
    /// </summary>
    public const BindingFlags AnyVisibilityFlattenHierarchy = AnyVisibility | BindingFlags.FlattenHierarchy;

    /// <summary>
    /// Combines AnyVisibilityFlattenHierarchy with Static to search for static members of any visibility,
    /// including inherited static members from base classes.
    /// </summary>
    public const BindingFlags AnyStaticFlattenHierarchy = AnyVisibilityFlattenHierarchy | BindingFlags.Static;

    /// <summary>
    /// Combines AnyVisibilityFlattenHierarchy with Instance to search for instance members of any visibility,
    /// including inherited instance members from base classes.
    /// </summary>
    public const BindingFlags AnyInstanceFlattenHierarchy = AnyVisibilityFlattenHierarchy | BindingFlags.Instance;

    #endregion
  }

}
