using System;

namespace turnir
{
  /// <summary>
  /// Партия
  /// </summary>
  [Serializable]
  struct Game
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
    GameResult result;
  }
}