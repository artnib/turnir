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
          row.Cells[2].Value = player.Title;
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
      if (ColTitle.Items.Count == 0)
        ColTitle.Items.AddRange(Resources.Titles.Split(new char[] { ',' }));
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
      Player oldPlayer;
      string pName;
      string title;
      object value;
      for (int i = 0; i < gridPlayers.RowCount; i++)
      {
        row = gridPlayers.Rows[i];
        board = (Byte)(i + 1);
        pName = row.Cells[1].Value.ToString();
        value = row.Cells[2].Value;
        title = value ==  null ? String.Empty : value.ToString();
        oldPlayer = tur.Players.Find(p =>
          p.Number == team.Number && p.Board == board);
        if (oldPlayer == null)
          tur.Players.Add(new Player
          {
            Number = team.Number,
            Name = pName,
            Board = board,
            Location = team.Name,
            Title = title
          });
        else
        {
          oldPlayer.Name = pName;
          oldPlayer.Location = team.Name;
          oldPlayer.Title = title;
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

    private void gridPlayers_RowValidated(object sender, DataGridViewCellEventArgs e)
    {
      CheckSaveButton();
    }

    private void name_TextChanged(object sender, EventArgs e)
    {
      CheckSaveButton();
    }

    private void cancel_Click(object sender, EventArgs e)
    {
      team = null;
    }

    private void gridPlayers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
      if(e.ColumnIndex != 1) return;
      var cell = gridPlayers.Rows[e.RowIndex].Cells[1];
      object value = e.FormattedValue;
      e.Cancel = false;
      cell.ErrorText = String.Empty;
      if (value == null || value.ToString().Length == 0)
      {
        e.Cancel = true;
        cell.ErrorText = "Укажите фамилию, имя";
      }
    }

    private void TeamForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      //e.Cancel = true;
      //Hide();
    }
  }
}