using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

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
    /// Место проведения
    /// </summary>
    internal string Place;

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
    /// Добавляет участника
    /// </summary>
    /// <param name="player">Участник</param>
    internal void AddPlayer(Player player)
    {
      Players.Add(player);
      if (player.Grade != null)
        UpdateCoefficient(player.Board);
    }

    /// <summary>
    /// Определяет возможность выполнения разрядов
    /// </summary>
    /// <returns></returns>
    bool TitlesObtainable()
    {
      if (Players.Count < 10) return false;
      if (IsTeam() && Teams.Count < 8) return false;
      return true;
    }

    /// <summary>
    /// Пересчитывает коэффициент турнира
    /// </summary>
    void UpdateCoefficient(byte board)
    {
      if (TitlesObtainable())
      {
        double sum = 0.0;
        var players = Players.FindAll(p => p.Board == board);
        if (coefficient == null)
        {
          coefficient = new List<double>(BoardNumber);
          for (int i = 0; i < BoardNumber; i++)
            coefficient.Add(Double.NaN);
        }
        foreach (Player player in players)
          if (player.Grade != null)
            sum += player.Grade.Coefficient;
        coefficient[board - 1] = sum / players.Count;
      }
    }

    /// <summary>
    /// Удаляет указанного участника
    /// </summary>
    /// <param name="player">Участник</param>
    internal void RemovePlayer(Player player)
    {
      var number = player.Number;
      Players.Remove(player);
      LiftPlayers(number);
      UpdateCoefficient(player.Board);
    }

    /// <summary>
    /// Список партий
    /// </summary>
    internal List<Game> Games;

    /// <summary>
    /// Возвращает количество сыгранных партий
    /// </summary>
    internal int PlayedGames
    {
      get { return Games.FindAll(g => g.Result != GameResult.None).Count; }
    }

    internal bool Started()
    {
      return Players.Count == 0 && Teams != null;
    }

    internal List<Team> Teams;

    internal Turnir()
    {
      Players = new List<Player>();
      Games = new List<Game>();
      Teams = new List<Team>();
    }

    #region Сортировка

    internal static int CompareByNumber(Player x, Player y)
    {
      return x.Number - y.Number;
    }

    internal static int CompareByNumber(Team x, Team y)
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
    /// Сравнивает команды по количеству очков
    /// </summary>
    /// <param name="x">Команда 1</param>
    /// <param name="y">Команда 2</param>
    /// <returns></returns>
    internal int CompareByScore(Team x, Team y)
    {
      if (x == y) return 0;

      //сравнение по очкам
      Double xscore = TeamScore(x);
      Double yscore = TeamScore(y);
      int diff = Math.Sign(yscore - xscore); 
      if (diff != 0)
        return diff;
      
      //сравнение по результату личной встречи
      xscore = TeamScore(x, y);
      yscore = TeamScore(y, x);
      diff = Math.Sign(yscore - xscore);
      if (diff != 0) return diff;

      //сравнение по количеству побед
      Byte xcount = WinCount(x);
      Byte ycount = WinCount(y);
      return Math.Sign(ycount - xcount);
    }

    #endregion

    /// <summary>
    /// Очищает итоги турнира
    /// </summary>
    internal void ClearResults()
    {
      if (playerScore == null)
        playerScore = new Dictionary<Player, double>();
      else
        playerScore.Clear();
      if (playerShmulyan == null)
        playerShmulyan = new Dictionary<Player, double>();
      else
        playerShmulyan.Clear();
    }

    /// <summary>
    /// Обновляет итоги турнира
    /// </summary>
    internal void UpdateResults()
    {
      ClearResults();
      List<Player> boardPlayers;
      int pcount;
      Player player;
      for (Byte i = 1; i <= BoardNumber; i++)
      {
        boardPlayers = Players.FindAll(p => p.Board == i);
        boardPlayers.Sort(CompareByScore);
        pcount = boardPlayers.Count;
        for (int j = 0; j < pcount; j++)
        {
          player = boardPlayers[j];
          player.Place = (Byte)(j + 1);
          player.Shmulyan = Shmulyan(player);
        }
      }
      if (IsTeam())
      {
        Teams.Sort(CompareByScore);
        for (int i = 0; i < Teams.Count; i++)
          Teams[i].Place = (Byte)(i + 1);
        Teams.Sort(CompareByNumber);
      }
    }

    Game FindGame(Player player, byte opponent)
    {
      return Games.Find(g => g.Board == player.Board &&
        ((g.Black == player.Number && g.White == opponent)
        || (g.Black == opponent && g.White == player.Number)));
    }

    double GameScore(Game game, Player player)
    {
      if (game == null) return Double.NaN;
      var score = 0.0;
      switch (game.Result)
      {
        case GameResult.Draw:
          score = 0.5;
          break;
        case GameResult.White:
          if (game.White == player.Number)
            score = 1.0;
          break;
        case GameResult.Black:
          if (game.Black == player.Number)
            score = 1.0;
          break;
        default:
          score = Double.NaN;
          break;
      }
      return score;
    }

    /// <summary>
    /// Возвращает очки участника, полученные за партию с указанным противником
    /// </summary>
    /// <param name="player">Участник</param>
    /// <param name="opponent">Противник</param>
    /// <returns>1 (выигрыш), 0 (проигрыш), 0.5 (ничья) или NaN (партия не сыграна)</returns>
    internal double GameScore(Player player, byte opponent)
    {
      return GameScore(FindGame(player, opponent), player);
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
      var games = Games.FindAll(g => (g.Board==player.Board) &&
        (g.White == player.Number || g.Black == player.Number));
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

    Byte WinCount(Team team)
    {
      var games = TeamGames(team);
      Byte winCount = 0;

      foreach (Team opponent in Teams)
      {
        if (opponent == team) continue;
        if (TeamScore(team, opponent) > (BoardNumber / 2))
          winCount++;
      }
      return winCount;
    }

    /// <summary>
    /// Возвращает список партий команды
    /// </summary>
    /// <param name="team">Команда</param>
    /// <returns>Список партий</returns>
    internal List<Game> TeamGames(Team team)
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

    [NonSerialized]
    Dictionary<Team,Double> teamScore;

    /// <summary>
    /// Возвращает суммарные очки команды
    /// </summary>
    /// <param name="team">Команда</param>
    /// <returns>Очки команды</returns>
    internal Double TeamScore(Team team)
    {
      if(teamScore == null)
      {
        teamScore = new Dictionary<Team,double>(Teams.Count);
      }
      if(teamScore.ContainsKey(team))
        return teamScore[team];
      var teamGames = TeamGames(team);
      var score = 0.0;
      foreach(Game game in teamGames)
        score += GameScore(game, team);
      return score;
    }

    /// <summary>
    /// Возвращает количество очков за командную партию
    /// </summary>
    /// <param name="team">Команда</param>
    /// <param name="opponent">Команда-противник</param>
    /// <returns></returns>
    internal Double TeamScore(Team team, Team opponent)
    {
      double score = Double.NaN;
      var games = TeamGames(team, opponent);
      if (games.Count > 0)
        score = 0.0;
      foreach (Game game in games)
      {
        score += GameScore(game, team);
      }
      return score;
    }

    Double GameScore(Game game, Team team)
    {
      var score = 0.0;
      switch (game.Result)
      {
        case GameResult.Draw:
          score = 0.5;
          break;
        case GameResult.White:
          if (game.White == team.Number)
            score = 1.0;
          break;
        case GameResult.Black:
          if (game.Black == team.Number)
            score = 1.0;
          break;
      }
      return score;
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
      LiftPlayers(number);
    }

    /// <summary>
    /// Меняет между собой номера двух команд
    /// </summary>
    /// <param name="team1">Команда 1</param>
    /// <param name="team2">Команда 2</param>
    internal void ChangeTeams(Team team1, Team team2)
    {
      var num1 = team1.Number;
      var num2 = team2.Number;
      team1.Number = num2;
      team2.Number = num1;
      var players1 = Players.FindAll(p => p.Number == num1);
      var players2 = Players.FindAll(p => p.Number == num2);
      foreach (Player player1 in players1)
        player1.Number = num2;
      foreach (Player player2 in players2)
        player2.Number = num1;
    }

    /// <summary>
    /// Меняет между собой номера двух участников
    /// </summary>
    /// <param name="player1">Участник 1</param>
    /// <param name="player2">Участник 2</param>
    internal void ChangePlayers(Player player1, Player player2)
    {
      var num1 = player1.Number;
      var num2 = player2.Number;
      player1.Number = num2;
      player2.Number = num1;
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
        if (game.Board != player.Board)
          continue;
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
        rival = Players.Find(p =>
          p.Board == game.Board  && p.Number == rivalNumber);
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

    /// <summary>
    /// Возвращает коэффициент турнира для заданной доски
    /// </summary>
    /// <param name="board">Номер доски</param>
    internal Double Coefficient(int board)
    {
      if (coefficient == null || board < 1)
      {
        return Double.NaN;
      }
      else
        return coefficient[board - 1];
    }
    
    [OptionalField]
    List<Double> coefficient;

    [NonSerialized]
    Dictionary<Player, Double> playerScore;
    [NonSerialized]
    Dictionary<Player, List<Game>> playerGame;
    [NonSerialized]
    Dictionary<Player, Double> playerShmulyan;
    
    void LiftPlayers(byte number)
    {
      var nextPlayers = Players.FindAll(p => p.Number > number);
      foreach (Player nextPlayer in nextPlayers)
        nextPlayer.Number--;
    }
  }
}