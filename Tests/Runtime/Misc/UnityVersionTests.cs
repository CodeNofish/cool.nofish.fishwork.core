using NUnit.Framework;
using UnityEngine;

namespace Fishwork.Core.Tests {

  public class UnityVersionTests {
    [Test]
    public void StaticConstructor_WhenClassInitialized_ThenDontThrowException() {
      Assert.DoesNotThrow(() => {
        var major = UnityVersion.Major;
        var minor = UnityVersion.Minor;
        
        Assert.That(major, Is.GreaterThan(0));
        Assert.That(minor, Is.GreaterThanOrEqualTo(0));
      });
    }
  }

}
