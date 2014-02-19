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
    /// Количество досок в командном турнире
    /// </summary>
    internal Byte BoardNumber = 1;

    /// <summary>
    /// Определяет, является ли турнир командным
    /// </summary>
    /// <returns></returns>
    internal bool IsTeam()
    {
      return BoardNumber > 1;
    }

    /// <summary>
    /// Определяет, является ли турнир личным
    /// </summary>
    /// <returns></returns>
    internal bool IsPersonal()
    {
      return BoardNumber < 2;
    }

    /// <summary>
    /// Главный судья
    /// </summary>
    internal string Referee;

    /// <summary>
    /// Главный секретарь
    /// </summary>
    internal string Secretary;

    /// <summary>
    /// Список участников
    /// </summary>
    internal List<Player> Players;

    /// <summary>
    /// Список партий
    /// </summary>
    internal List<Game> Games;

    internal List<Team> Teams;

    internal Turnir()
    {
      Players = new List<Player>();
      Games = new List<Game>();
      Teams = new List<Team>();
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
    
    #region Команды

    /// <summary>
    /// Возвращает список партий команды
    /// </summary>
    /// <param name="team">Команда</param>
    /// <returns>Список партий</returns>
    List<Game> TeamGames(Team team)
    {
      var number = team.Number;
      return Games.FindAll(g => g.White == number || g.Black == number);
    }

    /// <summary>
    /// Возвращает список партий команды с указанным противником
    /// </summary>
    /// <param name="team">Команда</param>
    /// <param name="opponent">Противник</param>
    /// <returns>Список партий</returns>
    List<Game> TeamGames(Team team, Team opponent)
    {
      return Games.FindAll(g =>
        (g.White == team.Number && g.Black == opponent.Number) ||
        (g.White == opponent.Number && g.Black == team.Number));
    }

    /// <summary>
    /// Возвращает список партий команды с указанным противником
    /// на заданной доске
    /// </summary>
    /// <param name="team">Команда</param>
    /// <param name="opponent">Противник</param>
    /// <param name="Board">Номер доски</param>
    /// <returns>Список партий</returns>
    List<Game> TeamGames(Team team, Team opponent, Byte Board)
    {
      return Games.FindAll(g => g.Board==Board &&
        ((g.White == team.Number && g.Black == opponent.Number) ||
        (g.White == opponent.Number && g.Black == team.Number)));
    }
    
    /// <summary>
    /// Удаляет указанную команду и её участников
    /// </summary>
    /// <param name="team">Команда</param>
    internal void RemoveTeam(Team team)
    {
      //удаляем команду и её участников
      var number = team.Number;
      Teams.Remove(team);
      Players.RemoveAll(p => p.Number == number);
      
      //"поднмаем вверх" команды, находящиеся в списке ниже удаляемой команды
      var nextTeams = Teams.FindAll(t => t.Number > number);
      foreach (Team nextTeam in nextTeams)
        nextTeam.Number--;
      var nextPlayers = Players.FindAll(p => p.Number > number);
      foreach (Player nextPlayer in nextPlayers)
        nextPlayer.Number--;
    }

    #endregion

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