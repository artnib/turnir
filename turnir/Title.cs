﻿namespace turnir
{
  /// <summary>
  /// Разряд
  /// </summary>
  [System.Serializable]
  internal class Title
  {
    /// <summary>
    /// Обозначение
    /// </summary>
    public string ShortName;

    /// <summary>
    /// Наименование
    /// </summary>
    public string FullName;

    /// <summary>
    /// Разрядный коэффициент
    /// </summary>
    public int Coefficient;

    public override string ToString()
    {
      return ShortName;
    }

    public override bool Equals(object obj)
    {
      if (obj is Title)
      {
        var t = (Title)obj;
        return t.Coefficient == Coefficient && t.ShortName == ShortName;
      }
      else
        return false;
    }
  }
}