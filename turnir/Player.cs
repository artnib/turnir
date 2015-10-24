using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

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
    /// Разряд (текст)
    /// </summary>
    internal string Title;

    /// <summary>
    /// Разряд с коэффициентом
    /// </summary>
    [OptionalField]
    internal Title Grade;
    
    /// <summary>
    /// Выполненный разряд
    /// </summary>
    [OptionalField]
    internal Title NewGrade;

    /// <summary>
    /// Турнирный коэффициент
    /// </summary>
    [OptionalField]
    internal Double Coefficient;

    /// <summary>
    /// Разрядные нормы
    /// </summary>
    [OptionalField]
    internal Dictionary<Title, double> Norms;

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

    [OnDeserialized]
    void InitBoard(StreamingContext context)
    {
      if (Board == 0) //предыдущая версия, где номер доски не использовался
        Board = 1;
    }
  }
}