using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace turnir
{
  /// <summary>
  /// Разряды по шашкам
  /// </summary>
  internal class Titles
  {
    internal List<Title> GetTitles()
    {
      return titles;
      
    }

    /// <summary>
    /// Возвращает нормы для выполнения разряда
    /// </summary>
    /// <param name="title">Разряд</param>
    /// <returns>Словарь "Разрядный коэффициент - очки в % от числа встреч"</returns>
    internal Dictionary<int, byte> GetNorms(Title title)
    {
      return norms[title];
    }

    /// <summary>
    /// Возвращает очки в % от числа встреч
    /// </summary>
    /// <param name="title">Выполняемый разряд</param>
    /// <param name="coefficient">Коэффициент соперника</param>
    /// <returns>Очки в % от числа встреч</returns>
    internal byte GetNorm(Title title, int coefficient)
    {
      if (norms[title].ContainsKey(coefficient))
        return norms[title][coefficient];
      else
        return 0;
    }

    /// <summary>
    /// Возвращает разряд по умолчанию
    /// </summary>
    /// <returns>Первый элемент списка разрядов</returns>
    internal Title DefaultTitle()
    {
      return titles[0];
    }

    internal Title GetTitle(string title)
    {
      return titles.Find(t => t.ShortName == title);
    }

    List<Title> titles;
    /// <summary>
    /// Словарь "Разряд" - словарь "Разрядный коэффициент - очки в % от числа встреч"
    /// </summary>
    Dictionary<Title, Dictionary<int, byte>> norms;

    internal Titles()
    {
      titles = new List<Title>();
      InitNorms();
      foreach (Title t in norms.Keys)
        titles.Add(t);
      //titles.Add(new Title { ShortName = "-", FullName = "юноши и девушки без разряда", Coefficient = 6 });
      //titles.Add(new Title { ShortName = "3 ю.", FullName = "3 юношеский разряд", Coefficient = 5 });
      //titles.Add(new Title { ShortName = "2 ю.", FullName = "2 юношеский разряд", Coefficient = 4 });
      //titles.Add(new Title { ShortName = "1 ю.", FullName = "1 юношеский разряд", Coefficient = 3 });
      //titles.Add(new Title { ShortName = "3", FullName = "3 разряд", Coefficient = 3 });
      //titles.Add(new Title { ShortName = "2", FullName = "2 разряд", Coefficient = 2 });
      //titles.Add(new Title { ShortName = "1", FullName = "1 разряд", Coefficient = 1 });
      //titles.Add(new Title { ShortName = "кмс", FullName = "кандидат в мастера спорта", Coefficient = 0 });
      //titles.Add(new Title { ShortName = "мс", FullName = "мастер спорта", Coefficient = -1 });
      //titles.Add(new Title { ShortName = "гр", FullName = "гроссмейстер", Coefficient = -2 });
    }

    void InitNorms()
    {
      norms = new Dictionary<Title, Dictionary<int, byte>>();
      var xe = XElement.Parse(turnir.Properties.Resources.norms);
      Dictionary<int,byte> scores;
      Title title;
      foreach (XElement t in xe.Elements("Title"))
      {
        title = new Title {
          ShortName = t.Attribute("short").Value,
          Coefficient = Int32.Parse(t.Attribute("coef").Value),
          FullName = t.Attribute("full").Value
        };
        scores = new Dictionary<int, byte>();
        foreach (XElement norm in t.Elements("Norm"))
          scores.Add(Int32.Parse(norm.Attribute("coef").Value), Byte.Parse(norm.Value));
        norms.Add(title, scores);
      }
    }
  }
}