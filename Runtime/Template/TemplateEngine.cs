using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Fishwork.Core {

  /// <summary>
  /// 简单的模板引擎
  /// </summary>
  public class TemplateEngine {
    private readonly string _content;
    private readonly Dictionary<string, string> _parameters;
    private readonly Func<string, string> _formatKeyFunc;

    private TemplateEngine(string content) {
      _content = content;
      _parameters = new Dictionary<string, string>();
      _formatKeyFunc = DefaultFormatKeyFunc;
    }

    private TemplateEngine(string content, Func<string, string> formatKeyFunc) {
      _content = content;
      _parameters = new Dictionary<string, string>();
      _formatKeyFunc = formatKeyFunc;
    }

    public static TemplateEngine Create(string content) {
      return new TemplateEngine(content);
    }

    public TemplateEngine AddParameter(string key, string value) {
      _parameters[key] = value;
      return this;
    }

    public TemplateEngine AddParameters(Dictionary<string, string> parameters) {
      foreach (var parameter in parameters)
        _parameters[parameter.Key] = parameter.Value;
      return this;
    }

    public TemplateEngine AddObjectAsParameters(object value) {
      var parameters = new Dictionary<string, string>();
      foreach (var property in value.GetType().GetProperties()) {
        var obj = property.GetValue(value, null);
        parameters.Add(property.Name, obj.ToString());
      }
      return AddParameters(parameters);
    }

    private static string DefaultFormatKeyFunc(string key) {
      return "{{" + key + "}}";
    }

    public string Render(bool check = false) {
      string result = _content;
      foreach (var parameter in _parameters) {
        result = result.Replace(_formatKeyFunc(parameter.Key), parameter.Value);
      }
      if (check) {
        var matchCollection = Regex.Matches(_content, @"\{\{.+?\}\}");
        foreach (Match match in matchCollection)
          throw new ArgumentException($"模版变量{match.Value}未被使用");
      }
      return result;
    }
  }

}
