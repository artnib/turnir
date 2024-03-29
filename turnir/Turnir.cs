﻿using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Linq;

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
    /// Определяет, является ли турнир пустым
    /// </summary>
    /// <returns><code>true</code>, если список участников пуст и не заданы
    /// название, главный судья и секретарь</returns>
    internal bool IsEmpty()
    {
      return Players.Count == 0 && string.IsNullOrEmpty(Name) &&
        String.IsNullOrEmpty(Referee) && String.IsNullOrEmpty(Secretary);
    }

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
          CheckNorms(player);
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

    double GameScore(Game game, Player player, out string display)
    {
      display = String.Empty;
      if (game == null) return Double.NaN;
      var score = 0.0;
      switch (game.Result)
      {
        case GameResult.Draw:
          score = 0.5;
          display = "½";
          break;
        case GameResult.White:
          if (game.White == player.Number)
            score = 1.0;
          break;
        case GameResult.Black:
          if (game.Black == player.Number)
            score = 1.0;
          break;
        case GameResult.WhiteForfeit:
          if (game.Black == player.Number)
          {
            display = "+";
            score = 1.0;
          }
          else
            display = "-";
          break;
        case GameResult.BlackForfeit:
          if (game.White == player.Number)
          {
            display = "+";
            score = 1.0;
          }
          else
            display = "-";
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
    /// <param name="display">Строка, отображаемая в турнирной таблице</param>
    /// <returns>1 (выигрыш), 0 (проигрыш), 0.5 (ничья) или NaN (партия не сыграна)</returns>
    internal double GameScore(Player player, byte opponent, out string display)
    {
      return GameScore(FindGame(player, opponent), player, out display);
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
    /// <returns>Количество очков или NaN</returns>
    internal Double TeamScore(Team team, Team opponent)
    {
      var score = Double.NaN;
      var games = TeamGames(team, opponent);
      double gs;
      foreach (Game game in games)
      {
        gs = GameScore(game, team);
        if (Double.IsNaN(gs)) continue;
        if (Double.IsNaN(score)) score = 0.0;
        score += gs;
      }
      return score;
    }

    Double GameScore(Game game, Team team)
    {
      var score = Double.NaN;
      switch (game.Result)
      {
        case GameResult.Draw:
          score = 0.5;
          break;
        case GameResult.White:
          score = game.White == team.Number ? 1.0 : 0.0;
          break;
        case GameResult.Black:
          score = game.Black == team.Number ? 1.0 : 0.0;
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
      for (byte i = 1; i <= BoardNumber; i++)
        UpdateCoefficient(i);
      //"поднимаем вверх" команды, находящиеся в списке ниже удаляемой команды
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

    #region Разрядные нормы

    /// <summary>
    /// Возвращает коэффициент турнира для заданной доски
    /// </summary>
    /// <param name="board">Номер доски</param>
    internal Double Coefficient(int board)
    {
      CheckCoeff();
      if (board < 1)
        return Double.NaN;
      return coefficient[board - 1];
    }

    private void CheckCoeff()
    {
      if (coefficient == null)
      {
        coefficient = new List<double>(BoardNumber);
        for (int i = 0; i < BoardNumber; i++)
          coefficient.Add(Double.NaN);
      }
    }

    /// <summary>
    /// Возвращает наивысший разряд, который можно выполнить
    /// </summary>
    /// <returns></returns>
    internal Title HighestTitle(int board)
    {
      CheckTitles();
      var highest = Math.Floor(coefficient[board - 1]);
      Title hTitle = null;
      for (int i = titles.Count - 1; i >= 0; i--)
        if (titles[i].Coefficient >= highest)
        {
          hTitle = titles[i];
          break;
        }
      return hTitle;
    }

    [NonSerialized]
    Titles TitlesAndNorms;

    private void CheckTitles()
    {
      if (TitlesAndNorms == null)
      {
        TitlesAndNorms = new Titles();
        titles = TitlesAndNorms.GetTitles();
      }
    }

    [OptionalField]
    List<Title> titles;

    [OptionalField]
    List<Double> coefficient;

    /// <summary>
    /// Пересчитывает коэффициент турнира на заданной доске
    /// </summary>
    /// <param name="board">Номер доски</param>
    internal void UpdateCoefficient(byte board)
    {
      double sum = 0.0;
      var players = Players.FindAll(p => p.Board == board);
      CheckCoeff();
      coefficient[board - 1] = Double.NaN;
      const byte MinPlayerCount = 8;
      if (players.Count < MinPlayerCount) return;
      foreach (Player player in players)
        if (player.Grade != null)
          sum += player.Grade.Coefficient;
      coefficient[board - 1] = sum / players.Count;
    }

    /// <summary>
    /// Проверяет возможность выполнения разрядных норм для участника
    /// </summary>
    /// <param name="player">Участник</param>
    void CheckNorms(Player player)
    {
      var games = PlayerGames(player);
      var playedGames = games.FindAll(g => g.Result != GameResult.None);
      var playerNorms = new Dictionary<Title, double>();
      player.Norms = playerNorms;
      const byte MinGameCount= 7;
      var gameCount = playedGames.Count;
      if (gameCount < MinGameCount)
      {
        return;
      }
      else
      {
        CheckTitles();
        var obtainableTitles = titles.FindAll(t =>
          t.Coefficient >= Math.Floor(player.Coefficient) &&
          TitlesAndNorms.GetNorms(t).Count > 0);
        foreach (Title ot in obtainableTitles)
          playerNorms.Add(ot, 0.0);
        var coeffSum = 0.0;
        foreach (Game game in playedGames)
        {
          var oppNumber = game.White == player.Number ? game.Black : game.White;
          var opponent = Players.Find(p => p.Board == player.Board && p.Number == oppNumber);
          if (opponent.Grade != null)
          {
            var oppCoeff = opponent.Grade.Coefficient;
            coeffSum += oppCoeff;
            foreach (Title ot in obtainableTitles)
              playerNorms[ot] += TitlesAndNorms.GetNorm(ot, oppCoeff) / 100.0;
          }
        }
        player.Coefficient = coeffSum / playedGames.Count;
        //foreach (Title title in obtainableTitles)
        //  playerNorms[title] /= playedGames.Count;

        foreach (Title nt in playerNorms.Keys.OrderByDescending(tt => tt.Coefficient))
        {
          player.NewGrade = null;
          if (player.Grade.Coefficient >= nt.Coefficient && PlayerScore(player) >= player.Norms[nt])
          {
            player.NewGrade = nt;
            break;
          }
        }

      }
    }

    #endregion

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