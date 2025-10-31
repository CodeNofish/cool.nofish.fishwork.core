using UnityEngine;

namespace Fishwork.Core {

  /// <summary>
  /// Provides static access to parsed Unity engine version information.
  /// This class automatically parses the current Unity version on initialization
  /// and makes major and minor version numbers available as integer properties.
  /// </summary>
#if UNITY_EDITOR
  [UnityEditor.InitializeOnLoad]
#endif
  public static class UnityVersion {
    /// <summary>
    /// Gets the major version component of the current Unity version.
    /// For example, in Unity "2022.3.61t8", this would return 2022.
    /// </summary>
    public static int Major { get; }

    /// <summary>
    /// Gets the minor version component of the current Unity version.
    /// For example, in Unity "2022.3.61t8", this would return 3.
    /// </summary>
    public static int Minor { get; }

    static UnityVersion() {
      string[] versionSlices = Application.unityVersion.Split('.');
      Major = int.Parse(versionSlices[0]);
      Minor = int.Parse(versionSlices[1]);
    }
  }

}
