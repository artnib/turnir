using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace turnir
{
  public partial class GamesForm : Form
  {
    GamesForm()
    {
      InitializeComponent();
    }

    internal GamesForm(Turnir tur)
      : this()
    {
      this.tur = tur;
      players = tur.Players;
    }

    Turnir tur;

    private void GamesForm_Load(object sender, EventArgs e)
    {
      cbPlayer.Items.AddRange(players.ToArray());
    }

    private void cbPlayer_SelectedIndexChanged(object sender, EventArgs e)
    {
      var index = cbPlayer.SelectedIndex;
      if(index == -1) return;
      if (rr == null)
        rr = new RoundRobin(tur);
      var games = rr.PlayerGames((Player)cbPlayer.Items[index]);
      ShowGames(games);
    }

    void ShowGames(List<Game> games)
    {
      lvGames.Items.Clear();
      ListViewItem lvi;
      foreach (Game game in games)
      {
        lvi = new ListViewItem(new string[] {
            players.Find(p => p.Number == game.White).ToString(),
            players.Find(p => p.Number == game.Black).ToString(),
            Result(game.Result)
          }
        );
        lvi.Tag = game;
        lvGames.Items.Add(lvi);
      }
    }

    string Result(GameResult result)
    {
      switch (result)
      { 
        case GameResult.Black:
          return "0-1";
        case GameResult.White:
          return "1-0";
        case GameResult.Draw:
          return "=";
        default:
          return String.Empty;
      }
    }

    RoundRobin rr;
    List<Player> players;

    private void lvGames_DoubleClick(object sender, EventArgs e)
    {

    }

    private void lvGames_SelectedIndexChanged(object sender, EventArgs e)
    {
      var selected = lvGames.SelectedItems;
      cmenu.Enabled = selected.Count > 0;
    }

    private void white_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.White);
    }

    void SetResult(GameResult result)
    {
      var lvi = lvGames.SelectedItems[0];
      var game = (Game)lvi.Tag;
      game.Result = result;
      lvi.SubItems[2].Text = Result(result);
    }

    private void black_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.Black);
    }

    private void draw_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.Draw);
    }
  }
}