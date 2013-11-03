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
      var games = rr.PendingGames((Player)cbPlayer.Items[index]);
      ShowGames(games);
    }

    void ShowGames(List<Game> games)
    {
      lvGames.Items.Clear();      
      foreach (Game game in games)
      {
        lvGames.Items.Add(new ListViewItem(
          new string[] {
            players.Find(p => p.Number == game.White).ToString(),
            players.Find(p => p.Number == game.Black).ToString()
          }
        ));
      }
    }

    RoundRobin rr;
    List<Player> players;

    private void label2_Click(object sender, EventArgs e)
    {

    }
  }
}