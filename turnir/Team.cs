using System;

namespace turnir
{
  /// <summary>
  /// Команда
  /// </summary>
  [Serializable]
  class Team
  {
   /// <summary>
   /// Название
   /// </summary>
    internal string Name;

   /// <summary>
   /// Номер
   /// </summary>
    internal Byte Number;

    /// <summary>
    /// Место
    /// </summary>
    internal Byte Place;

    public override string ToString()
    {
      return Name;
    }
  }
}