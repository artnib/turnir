using System.Collections.Generic;
using System;

namespace turnir
{
  /// <summary>
  /// Сведения о шашечном турнире
  /// </summary>
  [Serializable]
  class Turnir
  {
    public System.DateTime Date;
    public string Name;
    public string FileName;

    /// <summary>
    /// Главный судья
    /// </summary>
    internal string Referee;

    /// <summary>
    /// Список участников
    /// </summary>
    internal List<Player> Players;

    /// <summary>
    /// Список партий
    /// </summary>
    internal List<Game> Games;

    internal Turnir()
    {
      Players = new List<Player>();
      Games = new List<Game>();
    }
  }
}