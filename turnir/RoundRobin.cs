using System;
using System.Collections.Generic;

namespace turnir
{
  class RoundRobin
  {
    internal RoundRobin(Turnir tur)
    {
      this.tur = tur;
      players = tur.Players;
    }

    /// <summary>
    /// Возвращает список партий участника
    /// </summary>
    /// <param name="player">Участник</param>
    /// <returns>Список партий</returns>
    internal List<Game> PlayerGames(Player player)
    {
      var opponents = players.FindAll(p => p.Board == player.Board);
      var opCount = (Byte)opponents.Count;
      var pendingGames = new List<Game>(opCount - 1);
      Game game;
      bool even = opCount % 2 == 0;
      bool even1, odd1;
      bool even2, odd2;
      Byte num1, num2;
      Byte min, max;
      foreach (Player opponent in opponents)
      {
        if (opponent == player) continue;
        
        num1 = player.Number;
        num2 = opponent.Number;
        even1 = Even(player.Number);
        odd1 = !even1;
        even2 = Even(opponent.Number);
        odd2 = !even2;

        max = Math.Max(num1, num2);
        min = Math.Min(num1, num2);

        game = new Game() { Board = player.Board };

        if (even && (max == opCount)) //последний номер при четном количестве участников
        {
          if (min <= opCount / 2) //с верхней половиной черными
          {
            game.White = min;
            game.Black = max;
          }
          else //с нижней половиной белыми
          {
            game.White = max;
            game.Black = min;
          }
        }
        else
        {
          if (DoubleOdd(num1, num2) || //оба нечетные
            (DoubleEven(num1, num2)))  //оба четные
          {
            game.White = max; //больший номер играет белыми
            game.Black = min;
          }

          if (even1 != even2) //четный с нечетным
          {
            game.White = min; //меньший номер играет белыми
            game.Black = max;
          }
        }

        if (Even(player.Board)) //на чётных досках цвет шашек меняется
        {
          var temp = game.White;
          game.White = game.Black;
          game.Black = temp;
        }

        if (tur.Games == null)
          tur.Games = new List<Game>((opCount * opCount - opCount) / 2);
        var oldGame = tur.Games.Find(g => g.Board==game.Board &&
          g.White == game.White && g.Black == game.Black);
        if (oldGame != null)
          pendingGames.Add(oldGame);
        else
        {
          tur.Games.Add(game);
          pendingGames.Add(game);
        }
      }
      return pendingGames;
    }

    /// <summary>
    /// Определяет, является ли число чётным
    /// </summary>
    /// <param name="number">Число</param>
    /// <returns></returns>
    bool Even(Byte number)
    {
      return number % 2 == 0;
    }

    bool LastEven(Byte number)
    {
      return Even(number) && number == players.Count;
    }

    bool DoubleOdd(Byte number1, Byte number2)
    {
      return !Even(number1) && !Even(number2);
    }

    bool DoubleEven(Byte number1, Byte number2)
    {
      return Even(number1) && Even(number2);
    }

    List<Player> players;
    List<Game> games;
    Turnir tur;
  }
}