using System;

namespace turnir
{
  /// <summary>
  /// Участник турнира
  /// </summary>
  [Serializable]
  class Player
  {
    /// <summary>
    /// Фамилия, имя
    /// </summary>
    internal string Name;

    /// <summary>
    /// Номер в группе (лиге)
    /// </summary>
    internal Byte Number;

    /// <summary>
    /// Откуда (город или организация)
    /// </summary>
    internal string Location;

    /// <summary>
    /// Группа (лига)
    /// </summary>
    internal string Group;

    /// <summary>
    /// Разряд
    /// </summary>
    internal string Title;

    public override string ToString()
    {
      return Name;
    }
  }
}