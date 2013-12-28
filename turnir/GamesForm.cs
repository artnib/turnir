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

    internal GamesForm(Turnir tur, TurnirForm mainForm)
      : this(tur)
    {
      this.mainForm = mainForm;
    }

    /// <summary>
    /// Выбирает заданного участника
    /// </summary>
    /// <param name="index">Индекс участника</param>
    internal void SetPlayer(int index)
    {
      playerIndex = index;
    }

    int playerIndex = -1;
    Turnir tur;
    TurnirForm mainForm;

    private void GamesForm_Load(object sender, EventArgs e)
    {
      if(cbPlayer.Items.Count == 0)
        cbPlayer.Items.AddRange(players.ToArray());
      if (playerIndex >= 0)
        cbPlayer.SelectedIndex = playerIndex;
    }

    private void cbPlayer_SelectedIndexChanged(object sender, EventArgs e)
    {
      var index = cbPlayer.SelectedIndex;
      if(index == -1) return;
      if (rr == null)
        rr = new RoundRobin(tur);
      var player = (Player)cbPlayer.Items[index];
      var games = rr.PlayerGames(player);
      ShowGames(games);
      //lbScore.Text = String.Format("Очки: {0}", PlayerScore(player, games));
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

    #region Результаты партий

    private void white_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.White);
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

    void SetResult(GameResult result)
    {
      var lvi = lvGames.SelectedItems[0];
      var game = (Game)lvi.Tag;
      game.Result = result;
      lvi.SubItems[2].Text = Result(result);
      if (mainForm != null)
        mainForm.UpdatePlayerTable();
    }

    private void black_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.Black);
    }

    private void draw_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.Draw);
    }

    #endregion

  }
}