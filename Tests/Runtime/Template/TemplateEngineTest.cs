using NUnit.Framework;

namespace Fishwork.Core.Tests {

  public class TemplateEngineTest {
    [Test]
    public void TestA() {
      var result = TemplateEngine.Create("{{name}}，你好！")
        .AddParameter("name", "Fish")
        .Render();
      Assert.That("Fish，你好！".Equals(result));

      result = TemplateEngine.Create("{{one}},{{two}},{{three}}")
        .AddParameter("one", "1")
        .AddParameter("two", "2")
        .AddParameter("three", "3")
        .Render();
      Assert.That("1,2,3".Equals(result));
    }
  }

}
