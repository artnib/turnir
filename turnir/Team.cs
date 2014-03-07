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

    public override string ToString()
    {
      return Name;
    }
  }
}