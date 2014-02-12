using System;
using System.Runtime.Serialization;

namespace turnir
{
  /// <summary>
  /// Партия
  /// </summary>
  [Serializable]
  class Game
  {
    /// <summary>
    /// Группа
    /// </summary>
    string Group;

    /// <summary>
    /// Номер участника, играющего белыми
    /// </summary>
    internal Byte White;
    
    /// <summary>
    /// Номер участника, играющего белыми
    /// </summary>
    internal Byte Black;
    
    /// <summary>
    /// Результат партии
    /// </summary>
    internal GameResult Result;

    /// <summary>
    /// Номер доски
    /// </summary>
    internal Byte Board;

    [OnDeserialized]
    void InitBoard(StreamingContext context)
    {
      if (Board == 0) //предыдущая версия, где номер доски не использовался
        Board = 1;
    }
  }
}