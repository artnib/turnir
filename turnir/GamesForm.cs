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
      SetTurnir(tur);
    }

    internal GamesForm(Turnir tur, TurnirForm mainForm)
      : this(tur)
    {
      this.mainForm = mainForm;
    }

    /// <summary>
    /// Задаёт турнир
    /// </summary>
    /// <param name="turnir">Турнир</param>
    internal void SetTurnir(Turnir turnir)
    {
      tur = turnir;
      players = tur.Players;
      cbPlayer.Items.Clear();
      cbPlayer.Text = String.Empty;
      cbPlayer.Items.AddRange(players.ToArray());
      rr = null;
      lvGames.Items.Clear();
    }

    /// <summary>
    /// Выбирает заданного участника
    /// </summary>
    /// <param name="index">Индекс участника</param>
    internal void SetPlayer(int index)
    {
      playerIndex = index;
    }

    /// <summary>
    /// Фильтр для отображения партий
    /// </summary>
    internal object Filter
    {
      set { filter = value; }
    }

    object filter;

    void SetFilter(object filter)
    {
      if (filter == null) return;
      if (filter is Player)
        cbPlayer.SelectedItem = filter;
      if (filter is Team)
        cbTeam.SelectedItem = filter;
    }

    int playerIndex = -1;
    Turnir tur;
    TurnirForm mainForm;

    private void GamesForm_Load(object sender, EventArgs e)
    {
      
      if (cbTeam.Items.Count == 0 && tur.IsTeam())
        cbTeam.Items.AddRange(tur.Teams.ToArray());
      SetFilter(filter);
    }

    private void cbPlayer_SelectedIndexChanged(object sender, EventArgs e)
    {
      var index = cbPlayer.SelectedIndex;
      if(index == -1) return;
      cbTeam.SelectedIndex = -1;
      if (rr == null)
        rr = new RoundRobin(tur);
      var player = (Player)cbPlayer.Items[index];
      var games = rr.PlayerGames(player);
      ShowGames(games);
    }
    
    void ShowGames(List<Game> games)
    {
      lvGames.Items.Clear();
      ListViewItem lvi;
      foreach (Game game in games)
      {
        lvi = new ListViewItem(new string[] {
            WhiteName(game), BlackName(game), Result(game.Result),
            game.Board.ToString()
          }
        );
        lvi.Tag = game;
        lvGames.Items.Add(lvi);
      }
    }

    const string fmtName = "{1} ({0})";

    string WhiteName(Game game)
    {
      var pname = players.Find(p =>
        p.Board == game.Board && p.Number == game.White).Name;
      var tname = String.Empty;
      if (tur.IsTeam())
      {
        tname = tur.Teams.Find(t => t.Number == game.White).Name;
        return String.Format(fmtName, tname, pname);
      }
      else
        return pname;
    }

    string BlackName(Game game)
    {
      var pname = players.Find(p =>
        p.Board == game.Board && p.Number == game.Black).Name;
      var tname = String.Empty;
      if (tur.IsTeam())
      {
        tname = tur.Teams.Find(t => t.Number == game.Black).Name;
        return String.Format(fmtName, tname, pname);
      }
      else
        return pname;
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
        case GameResult.WhiteForfeit:
          return "-/+";
        case GameResult.BlackForfeit:
          return "+/-";
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
    
    private void clear_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.None);
    }
    
    private void whiteForfeit_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.WhiteForfeit);
    }

    private void blackForfeit_Click(object sender, EventArgs e)
    {
      SetResult(GameResult.BlackForfeit);
    }

    #endregion

    private void cbTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cbTeam.SelectedIndex == -1) return;
      cbPlayer.SelectedIndex = -1;
      var team = (Team)cbTeam.SelectedItem;
      var games = tur.TeamGames(team);
      ShowGames(games);
    }
    
  }
}