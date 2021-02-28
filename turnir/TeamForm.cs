using System;
using System.Windows.Forms;
using turnir.Properties;

namespace turnir
{
  public partial class TeamForm : Form
  {
    internal TeamForm(Turnir turnir)
    {
      InitializeComponent();
      tur = turnir;
    }

    internal TeamForm(Team team, Turnir turnir)
      : this(turnir)
    {
      this.Team = team;
    }

    /// <summary>
    /// Сведения о команде
    /// </summary>
    internal Team Team
    {
      get { return team; }
      set
      {
        team = value;
        name.Text = team.Name;
        var players = tur.Players.FindAll(p => p.Number == team.Number);
        gridPlayers.RowCount = players.Count;
        Byte board;
        int index;
        DataGridViewRow row;
        foreach (Player player in players)
        {
          board = player.Board;
          index = board - 1;
          row = gridPlayers.Rows[index];
          row.Cells[1].Value = player.Name;
          row.Cells[2].Value = player.Grade.ShortName;
          row.Tag = player;
        }
      }
    }

    Team team;
    Turnir tur;

    private void TeamForm_Load(object sender, EventArgs e)
    {
      var boardNum = tur.BoardNumber;
      gridPlayers.RowCount = boardNum;
      for (int i = 1; i <= boardNum; i++)
      {
        gridPlayers.Rows[i - 1].Cells[0].Value = i;
      }
    }

    void CheckSaveButton()
    {
      save.Enabled = (name.Text.Length > 0) && AllPlayersHaveName();
    }

    private void save_Click(object sender, EventArgs e)
    {
      team.Name = name.Text;
      Byte board;
      DataGridViewRow row;
      Player oldPlayer, player;
      for (int i = 0; i < gridPlayers.RowCount; i++)
      {
        row = gridPlayers.Rows[i];
        player = (Player)row.Tag;
        board = (Byte)(i + 1);
        oldPlayer = tur.Players.Find(p =>
          p.Number == team.Number && p.Board == board);
        if (oldPlayer == null)
          tur.AddPlayer(new Player
          {
            Number = team.Number,
            Name = player.Name,
            Board = board,
            Location = team.Name,
            Grade = player.Grade
          });
        else
        {
          oldPlayer.Name = player.Name;
          oldPlayer.Location = team.Name;
          if (oldPlayer.Grade != player.Grade)
          {
            oldPlayer.Grade = player.Grade;
            tur.UpdateCoefficient(player.Board);
          }
        }
      }
    }

    bool AllPlayersHaveName()
    {
      object value;
      foreach (DataGridViewRow row in gridPlayers.Rows)
      {
        value = row.Cells[1].Value;
        if(value == null || value.ToString().Length == 0)
          return false;
      }
      return true;
    }

    private void name_TextChanged(object sender, EventArgs e)
    {
      CheckSaveButton();
    }

    private void cancel_Click(object sender, EventArgs e)
    {
      team = null;
    }

    private void TeamForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      //e.Cancel = true;
      //Hide();
    }
    
    PlayerForm playerForm;

    private void gridPlayers_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F4)
      {
        EditRow();
      }
    }

    private void EditRow()
    {
      var row = gridPlayers.CurrentRow;
      if (row != null)
      {
        var player = (Player)row.Tag;
        if (playerForm == null)
          playerForm = new PlayerForm(player ?? new Player { Location = name.Text });
        else
          playerForm.Player = player ?? new Player { Location = name.Text };
        playerForm.ShowDialog(this);
        if (playerForm.Player != null)
        {
          row.Tag = playerForm.Player;
          row.Cells[ColName.Index].Value = playerForm.Player.Name;
          row.Cells[ColTitle.Index].Value = playerForm.Player.Grade.ShortName;
        }
        CheckSaveButton();
      }
    }
  }
}