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
    /// Номер в личных соревнованиях или номер команды в командных соревнованиях
    /// </summary>
    internal Byte Number;

    /// <summary>
    /// Номер доски в командных соревнованиях.
    /// В личных соревнованиях равен единице.
    /// </summary>
    internal Byte Board = 1;

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

    /// <summary>
    /// Место
    /// </summary>
    internal Byte Place;

    /// <summary>
    /// Коэффициент Шмульяна
    /// </summary>
    internal double Shmulyan;

    public override string ToString()
    {
      return Name;
    }
  }
}