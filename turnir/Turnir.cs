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

    internal static int CompareByNumber(Player x, Player y)
    {
      return x.Number - y.Number;
    }

    internal int CompareByScore(Player x, Player y)
    {
      Double xscore = PlayerScore(x);
      Double yscore = PlayerScore(y);
      int diff = Math.Sign(yscore - xscore);
      if (diff != 0)
        return diff;
      diff = Math.Sign(Shmulyan(y) - Shmulyan(x));
      //if (diff != 0)
      return diff;
    }

    /// <summary>
    /// Возвращает список партий участника
    /// </summary>
    /// <param name="player">Участник</param>
    /// <returns>Список партий</returns>
    List<Game> PlayerGames(Player player)
    {
      if (playerGame == null)
        playerGame = new Dictionary<Player, List<Game>>();
      if (playerGame.ContainsKey(player))
        return playerGame[player];
      var games = Games.FindAll(
        g => g.White == player.Number || g.Black == player.Number);
      playerGame.Add(player, games);
      return games;
    }

    /// <summary>
    /// Возвращает очки участника
    /// </summary>
    /// <param name="player">Участник</param>
    /// <returns>Очки участника</returns>
    Double PlayerScore(Player player)
    {
      if (playerScore == null)
        playerScore = new Dictionary<Player, double>();
      if (playerScore.ContainsKey(player))
        return playerScore[player];
      var score = 0.0;
      var games = PlayerGames(player);
      foreach (Game game in games)
      {
        switch (game.Result)
        {
          case GameResult.Draw:
            score += 0.5;
            break;
          case GameResult.White:
            if (game.White == player.Number)
              score += 1;
            break;
          case GameResult.Black:
            if (game.Black == player.Number)
              score += 1;
            break;
        }
      }
      playerScore.Add(player, score);
      return score;
    }

    /// <summary>
    /// Возвращает коэффициент Шмульяна
    /// </summary>
    /// <param name="player">Участник</param>
    /// <returns>Коэффициент Шмульяна</returns>
    internal Double Shmulyan(Player player)
    {
      if (playerShmulyan == null)
        playerShmulyan = new Dictionary<Player, double>();
      if (playerShmulyan.ContainsKey(player))
        return playerShmulyan[player];
      var games = PlayerGames(player);
      Double k = 0.0;
      Byte rivalNumber;
      Player rival;
      Double rivalScore;
      bool rivalBlack;
      foreach (Game game in games)
      {
        if (game.White == player.Number) //участник играл белыми
        {
          rivalNumber = game.Black;
          rivalBlack = true;
        }
        else //противник играл белыми
        {
          rivalNumber = game.White;
          rivalBlack = false;
        }
        rival = Players.Find(p => p.Number == rivalNumber);
        rivalScore = PlayerScore(rival);
        switch (game.Result)
        { 
          case GameResult.White:
            if (rivalBlack)
              k += rivalScore;
            else
              k -= rivalScore;
            break;
          case GameResult.Black:
            if(rivalBlack)
              k -= rivalScore;
            else
              k += rivalScore;
            break;
        }
      }
      playerShmulyan.Add(player, k);
      return k;
    }

    Dictionary<Player, Double> playerScore;
    Dictionary<Player, List<Game>> playerGame;
    Dictionary<Player, Double> playerShmulyan;
  }
}