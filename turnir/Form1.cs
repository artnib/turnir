using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AppSettings;
using System.Text;

namespace turnir
{
  public partial class Form1 : Form
  {
    string turPath;

    Player PlayerFromItem(ListViewItem lvi)
    {
      if (lvi.Tag == null)
      {
        return new Player
        {
          Number = Byte.Parse(lvi.SubItems[0].Text),
          Name = lvi.SubItems[1].Text,
          Location = lvi.SubItems[2].Text
        };
      }
      else
        return (Player)lvi.Tag;
    }
    
    #region Таблица участников

    List<Player> PlayersFromListView()
    {
      var players = new List<Player>(lvPlayers.Items.Count);
      Player player;
      foreach (ListViewItem lvi in lvPlayers.Items)
      {
        player = new Player
        {
          Number = Byte.Parse(lvi.SubItems[0].Text),
          Name = lvi.SubItems[1].Text,
          Location = lvi.SubItems[2].Text
        };
        players.Add(player);
      }
      return players;
    }

    void PlayersToListView(List<Player> players)
    {
      lvPlayers.Items.Clear();
      RemoveTempColumns();
      lvPlayers.BeginUpdate();
      foreach (Player player in players)
      {
        AddPlayer(player);
      }
      ColumnHeader header;
      for (int i = 1; i <= players.Count; i++)
      {
        lvPlayers.Columns.Insert(lvPlayers.Columns.Count - 1, i.ToString(), 30);
      }
      SetColumnWidth();
      lvPlayers.EndUpdate();
    }

    #endregion

    void SaveTurnir(string turPath)
    {
      if (String.IsNullOrEmpty(turPath))
        if (saveDlg.ShowDialog(this) == DialogResult.OK)
          turPath = saveDlg.FileName;
      var fs = new FileStream(turPath, FileMode.Create);
      var bf = new BinaryFormatter();
      CurTurnir.Date = dtDate.Value;
      CurTurnir.Name = tbTurnir.Text;
      CurTurnir.Referee = tbReferee.Text;
      bf.Serialize(fs, CurTurnir);
      fs.Close();
      curFile = turPath;
    }

    void RestoreTurnir(string turPath)
    {
      if (File.Exists(turPath))
      {
        var bf = new BinaryFormatter();
        var fs = File.OpenRead(turPath);
        if (fs.Length > 0)
        {
          CurTurnir = (Turnir)bf.Deserialize(fs);
          dtDate.Value = CurTurnir.Date;
          tbTurnir.Text = CurTurnir.Name;
          tbReferee.Text = CurTurnir.Referee;
          PlayersToListView(CurTurnir.Players);
        }
        fs.Close();
        curFile = turPath;
        UpdateCaption();
      }
      if (CurTurnir == null) CurTurnir = new Turnir();
    }

    /// <summary>
    /// Подготовка к работе с новым турниром
    /// </summary>
    void NewTurnir()
    {
      SaveTurnir(curFile); //сохраняем текущий турнир
      curFile = String.Empty;
      CurTurnir = new Turnir();
      tbTurnir.Text = "Турнир";
      dtDate.Value = DateTime.Now;
      lvPlayers.Items.Clear();
      RemoveTempColumns();    
    }

    /// <summary>
    /// Удаляет все столбцы между столбцами "Разряд" и "Очки"
    /// </summary>
    private void RemoveTempColumns()
    {
      while (lvPlayers.Columns.Count > 5)
      {
        lvPlayers.Columns.RemoveAt(4);
      }
    }

    string AppDir;
    Turnir CurTurnir;
    string FileName;
    string LastPath;

    PlayerForm playerForm;

    public Form1()
    {
      InitializeComponent();
    }

    #region События формы

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveTurnir(curFile);
      SaveSettings();
    }

    private void Form1_Load(object sender, System.EventArgs e)
    {
      bf = new BinaryFormatter();
      AppDir = Path.GetDirectoryName(Application.ExecutablePath);
      
      xs = new XmlSettings(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
      xs.LoadSettings(Path.Combine(AppDir, "settings.xml"));
      SetPosAndSize();
      turPath = xs.ReadSetting(Setting.LastFile, String.Empty);
      RestoreTurnir(turPath);
    }

    #endregion

    private void dgvPlayers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      NewTurnir();
    }
 
    private void tbTurnir_TextChanged(object sender, System.EventArgs e)
    {
      if (!String.IsNullOrEmpty(tbTurnir.Text))
      {
        tbTurnir.Text = "У турнира должно быть название";
        tbTurnir.ForeColor = Color.Red;
      }
      else
      {
        tbTurnir.ForeColor = Color.Black;
        Text = tbTurnir.Text;
      }
    }

    private void dgvPlayers_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
    {
      
    }

    private void dgvPlayers_UserAddedRow(object sender, DataGridViewRowEventArgs e)
    {
      e.Row.Cells[2].Value = String.Empty;
    }

    private void mnuAddPlaeyr_Click(object sender, EventArgs e)
    {
      if (playerForm == null)
        playerForm = new PlayerForm();
      else
        playerForm.Player = null;
      playerForm.ShowDialog(this);
      var player = playerForm.Player;
      if (player == null) return;
      var lvItems = lvPlayers.Items;
      player.Number = (Byte)(lvItems.Count + 1);
      CurTurnir.Players.Add(player);
      AddPlayer(player);
    }

    ListViewItem PlayerToItem(Player player)
    {
      var lvi = new ListViewItem(new string[]{
          player.Number.ToString(), player.Name, player.Location, player.Title});
      double totalScore = 0.0;
      double score = 0.0;
      for (int i = 1; i <= CurTurnir.Players.Count; i++)
      {
        if (i == player.Number)
          lvi.SubItems.Add("X");
        else
        {
          var game = CurTurnir.Games.Find(g => (g.Black == player.Number && g.White == i)
            || (g.Black==i && g.White==player.Number));
          if (game != null)
          {
            if (game.Result == GameResult.None)
              lvi.SubItems.Add(String.Empty);
            else
            {
              score = GameScore(game, player);
              lvi.SubItems.Add(score.ToString());
              totalScore += score;
            }
          }
          else
            lvi.SubItems.Add(String.Empty);
        }
      }
      lvi.SubItems.Add(totalScore.ToString());
      lvi.Tag = player;
      return lvi;
    }

    double GameScore(Game game, Player player)
    {
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
      }
      return score;
    }

    void AddPlayer(Player player)
    {
      var lvi = lvPlayers.Items.Add(PlayerToItem(player));
      lvi.Selected = true;
    }

    private void mnuPlayers_Click(object sender, EventArgs e)
    {
      tabControl1.SelectedTab = tabPlayers;
    }

    void UpdateItem(ListViewItem item, Player player)
    {
      item.SubItems[1].Text = player.Name;
      item.SubItems[2].Text = player.Location;
      item.Tag = player;
    }

    private void lvPlayers_DoubleClick(object sender, EventArgs e)
    {
      var selectedItems = lvPlayers.SelectedItems;
      if (selectedItems.Count > 0)
      {
        var item = selectedItems[0];
        var player = PlayerFromItem(item);
        if (playerForm == null)
          playerForm = new PlayerForm(player);
        else
          playerForm.Player = player;
        playerForm.ShowDialog(this);
        if(playerForm.Player != null)
          UpdateItem(item, playerForm.Player);
      }
    }

    private void lvPlayers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      mnuPlayers.Enabled = tabControl1.SelectedTab == tabPlayers;
    }

    private void tbReferee_TextChanged(object sender, EventArgs e)
    {

    }

    GamesForm gamesForm;

    private void mnuGames_Click(object sender, EventArgs e)
    {
      if (gamesForm == null)
        gamesForm = new GamesForm(CurTurnir);
      var selected = lvPlayers.SelectedIndices;
      if (selected.Count > 0)
        gamesForm.SetPlayer(selected[0]);
      gamesForm.ShowDialog(this);
      UpateTable();
    }

    private void mnuDel_Click(object sender, EventArgs e)
    {

    }

    #region Работа с файлами турниров

    BinaryFormatter bf;
    FileStream fs;
    string curFile;

    private void mnuSave_Click(object sender, EventArgs e)
    {
      if (saveDlg.ShowDialog() == DialogResult.OK)
      {
        fs = new FileStream(saveDlg.FileName, FileMode.Create);
        CurTurnir.Date = dtDate.Value;
        CurTurnir.Name = tbTurnir.Text;
        CurTurnir.Referee = tbReferee.Text;
        bf.Serialize(fs, CurTurnir);
        fs.Close();
        curFile = saveDlg.FileName;
        UpdateCaption();
      }
    }

    private void mnuOpen_Click(object sender, EventArgs e)
    {
      if (openDlg.ShowDialog() == DialogResult.OK)
      {
        curFile = openDlg.FileName;
        RestoreTurnir(curFile);
      }
    }

    void UpdateCaption()
    {
      Text = Path.GetFileNameWithoutExtension(curFile);
    }

    #endregion

    void UpateTable()
    {
      lvPlayers.BeginUpdate();
      lvPlayers.Items.Clear();
      foreach (Player player in CurTurnir.Players)
      {
        AddPlayer(player);
      }
      lvPlayers.EndUpdate();
    }

    #region Настройки формы

    XmlSettings xs;

    private void SetPosAndSize()
    {
      if (xs.ReadSetting(Setting.Maximized, false))
        WindowState = FormWindowState.Maximized;
      else
      {
        Visible = false;
        WindowState = FormWindowState.Normal;
        Left = xs.ReadSetting(Setting.Left, 0);
        Top = xs.ReadSetting(Setting.Top, 0);
        Width = xs.ReadSetting(Setting.Width, Width);
        Height = xs.ReadSetting(Setting.Height, Height);
        Visible = true;
      }
    }

    private void SaveSettings()
    {
      SavePosAndSize();
      SaveColumnWidth();
      xs.WriteSetting(Setting.LastFile, curFile);
      xs.Save();
    }

    private void SavePosAndSize()
    {
      bool maximized = WindowState == FormWindowState.Maximized;
      xs.WriteSetting(Setting.Maximized, maximized);
      if (!maximized)
      {
        xs.WriteSetting(Setting.Left, Left);
        xs.WriteSetting(Setting.Top, Top);
        xs.WriteSetting(Setting.Width, Width);
        xs.WriteSetting(Setting.Height, Height);
      }
    }

    #region Ширина столбцов таблицы

    const char delim = ';';
    
    void SetColumnWidth()
    {
      var widths = xs.ReadSetting(Setting.Columns, String.Empty).Split(new char[] { delim });
      int width;

      for (int i = 0; i < widths.Length; i++)
      {
        if (Int32.TryParse(widths[i], out width))
          if (i < lvPlayers.Columns.Count)
            lvPlayers.Columns[i].Width = width;
      }
    }
    void SaveColumnWidth()
    {
       var sb = new StringBuilder();
      for (int i = 0; i < lvPlayers.Columns.Count; i++)
      {
        if (i > 0) sb.Append(delim);
        sb.Append(lvPlayers.Columns[i].Width);
      }
      xs.WriteSetting(Setting.Columns, sb.ToString());
    }

    #endregion

    private void mnuNewTurnir_Click(object sender, EventArgs e)
    {
      NewTurnir();
    }

    #endregion
  }
}