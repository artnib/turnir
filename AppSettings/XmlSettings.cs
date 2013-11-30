using System;
using System.Xml.Linq;
using System.IO;

namespace AppSettings
{
  /// <summary>
  /// Средство работы с настройками в XML-файле
  /// </summary>
  public class XmlSettings
  {
    string configPath;
    XElement settings;
    string appName;

    public XmlSettings(string appName)
    {
      this.appName = appName;
    }

    /// <summary>
    /// Загружает настройки из файла
    /// </summary>
    /// <param name="filePath">Путь к файлу настроек</param>
    public void LoadSettings(string filePath)
    {
      configPath = filePath;
      settings = File.Exists(configPath) ? XElement.Load(configPath) : new XElement(appName);
    }

    /// <summary>
    /// Сохраняет настройки
    /// </summary>
    public void Save()
    {
      settings.Save(configPath);
    }

    public string this[string settingName]
    {
      get
      {
        var setting = settings.Element(settingName);
        return null == setting ? String.Empty : setting.Value;
      }
      set
      {
        var setting = settings.Element(settingName);
        if (null == setting)
        {
          settings.Add(new XElement(settingName, value));
        }
        else
        {
          setting.SetValue(value);
        }
      }
    }

    public int ReadSetting(string name, int defaultValue)
    {
      var setting = settings.Element(name);
      if (setting == null)
        return defaultValue;
      int value;
      if (Int32.TryParse(setting.Value, out value))
        return value;
      else
        return defaultValue;
    }

    public bool ReadSetting(string name, bool defaultValue)
    {
      var setting = settings.Element(name);
      if (setting == null)
        return defaultValue;
      bool value;
      if (Boolean.TryParse(setting.Value, out value))
        return value;
      else
        return defaultValue;
    }

    /// <summary>
    /// Возвращает значение настройки
    /// </summary>
    /// <param name="name">Имя настройки</param>
    /// <param name="defaultValue">Значение по умолчанию</param>
    /// <returns></returns>
    public string ReadSetting(string name, string defaultValue)
    {
      var setting = settings.Element(name);
      if (setting == null)
        return defaultValue;
      else
        return setting.Value;
    }

    public void WriteSetting(string name, object value)
    {
      var setting = settings.Element(name);
      if (null == setting)
      {
        settings.Add(new XElement(name, value));
      }
      else
      {
        setting.SetValue(value);
      }
    }
  }
}