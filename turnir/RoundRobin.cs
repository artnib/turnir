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
      var playerCount = (Byte)players.Count;
      var pendingGames = new List<Game>(playerCount - 1);
      Game game;
      bool even = playerCount % 2 == 0;
      bool even1, odd1;
      bool even2, odd2;
      Byte num1, num2;
      Byte min, max;
      foreach (Player opponent in players)
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

        game = new Game();

        if (even && (max == playerCount)) //последний номер при четном количестве участников
        {
          if (min <= playerCount / 2) //с верхней половиной черными
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
        if (tur.Games == null)
          tur.Games = new List<Game>((playerCount * playerCount - playerCount) / 2);
        var oldGame = tur.Games.Find(
          g => g.White == game.White && g.Black == game.Black);
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