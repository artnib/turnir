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
  public partial class TurnirForm : Form
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

    void PlayersToListView(List<Player> players)
    {
      lvTable.Items.Clear();
      RemoveTempColumns();
      lvTable.BeginUpdate();
      foreach (Player player in players)
      {
        AddPlayer(player);
      }
      AddPlayerColumns();
      SetColumnWidth();
      lvTable.EndUpdate();
    }

    #endregion

    void SaveTurnir(string turPath)
    {
      if (String.IsNullOrEmpty(turPath)) //несохранённый турнир
        if (saveDlg.ShowDialog(this) == DialogResult.OK)
        {
          turPath = saveDlg.FileName;
        }
        else //отмена сохранения
          return;
      var fs = new FileStream(turPath, FileMode.Create);
      var bf = new BinaryFormatter();
      CurTurnir.Date = dtDate.Value;
      CurTurnir.Name = tbTurnir.Text;
      CurTurnir.Referee = tbReferee.Text;
      CurTurnir.Secretary = tbSecretary.Text;
      CurTurnir.Players.Sort(Turnir.CompareByNumber);
      bf.Serialize(fs, CurTurnir);
      fs.Close();
      curFile = turPath;
    }

    void RestoreTurnir(string turPath)
    {
      curFile = String.Empty;
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
          tbSecretary.Text = CurTurnir.Secretary;
          CheckTurnirType(CurTurnir);
          PlayersToListView(CurTurnir.Players);
          RestoreCompetitorList();
        }
        fs.Close();
        curFile = turPath;
        UpdateCaption();
      }
      if (CurTurnir == null) NewTurnir();
    }

    void CheckTurnirType(Turnir tur)
    {
      bool teamTur = tur.IsTeam();
      if (teamTur)
      {
        rbTeam.Checked = true;
        numBoard.Value = CurTurnir.BoardNumber;
      }
      else
      {
        rbPersonal.Checked = true;
      }
    }

    const string defaultCaption = "Турнир";

    /// <summary>
    /// Подготовка к работе с новым турниром
    /// </summary>
    void NewTurnir()
    {
      curFile = String.Empty;
      CurTurnir = new Turnir();
      tbTurnir.Text = "Турнир";
      Text = defaultCaption;
      dtDate.Value = DateTime.Now;
      CheckTurnirType(CurTurnir);
      lvTable.Items.Clear();
      lvCompetitors.Items.Clear();
      RemoveTempColumns();    
    }

    /// <summary>
    /// Удаляет все столбцы между столбцами "Разряд" и "Очки"
    /// </summary>
    private void RemoveTempColumns()
    {
      while (lvTable.Columns.Count > 7)
      {
        lvTable.Columns.RemoveAt(4);
      }
    }

    /// <summary>
    /// Начальная ширина столбца с очками за партию
    /// </summary>
    const int columnWidth = 30;

    void AddPlayerColumns()
    {
      while ((lvTable.Columns.Count - 7) < CurTurnir.Players.Count)
      {
        lvTable.Columns.Insert(lvTable.Columns.Count - 3,
          (lvTable.Columns.Count - 6).ToString(), columnWidth);
      }
    }

    string  AppDir;
    Turnir CurTurnir;

    PlayerForm playerForm;

    public TurnirForm()
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
      SaveTurnir(curFile);
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

    #region Меню "Участники"

    private void mnuAddPlaeyr_Click(object sender, EventArgs e)
    {
      ListViewItem competitor;
      if (CurTurnir.IsPersonal())
      {
        var player = GetNewPlayer();
        if (player == null) return;
        var lvItems = lvTable.Items;
        player.Number = (Byte)(lvItems.Count + 1);
        CurTurnir.Players.Add(player);
      }
      else
      {
        var team = GetNewTeam();
        if (team == null) return;
        competitor = new ListViewItem(new string[] {
          team.Number.ToString(), team.Name });
        competitor.Tag = team;
        lvCompetitors.Items.Add(competitor);
        CurTurnir.Teams.Add(team);
      }
      //UpdatePlayerTable();
    }

    /// <summary>
    /// Показывает форму участника и возвращает сведения об участнике
    /// </summary>
    /// <returns>Сведения об участнике или null</returns>
    private Player GetNewPlayer()
    {
      if (playerForm == null)
        playerForm = new PlayerForm();
      else
        playerForm.Player = null;
      playerForm.ShowDialog(this);
      var player = playerForm.Player;
      return player;
    }

    /// <summary>
    /// Показывает форму команды и возвращает сведения о команде
    /// </summary>
    /// <returns>Сведения о команде или null</returns>
    Team GetNewTeam()
    {
      if (teamForm == null)
        teamForm = new TeamForm(CurTurnir);
      teamForm.Team = new Team {
        Number = (Byte)(lvCompetitors.Items.Count + 1) };
      teamForm.ShowDialog(this);
      return teamForm.Team;
    }

    private void mnuDel_Click(object sender, EventArgs e)
    {
      var competitor = lvCompetitors.SelectedItems[0].Tag;
      if(competitor is Team)
        CurTurnir.RemoveTeam((Team)competitor);
      RestoreCompetitorList();
    }

    private void mnuEditPlayer_Click(object sender, EventArgs e)
    {
      if (CurTurnir.IsPersonal())
        EditPlayer();
      else
      {
        ListViewItem lvi;
        lvi = lvCompetitors.SelectedItems[0];
        Team team = (Team)lvi.Tag;
        if (teamForm != null)
          teamForm.Team = team;
        else
          teamForm = new TeamForm(team, CurTurnir);
        teamForm.ShowDialog(this);
        lvi.SubItems[1].Text = team.Name;
      }
    }

    #endregion

    ListViewItem PlayerToItem(Player player)
    {
      var lvi = new ListViewItem(new string[]{
          player.Number.ToString(), player.Name, player.Location, player.Title});
      double totalScore = 0.0;
      double score = 0.0;
      var playerCount = CurTurnir.Players.Count;
      for (int i = 1; i <= playerCount; i++)
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
      
      lvi.Tag = player;
      lvi.SubItems.Add(totalScore.ToString());
      var place = player.Place;
      lvi.SubItems.Add(place > 0 ? player.Place.ToString() : String.Empty);
      var shmulyan = player.Shmulyan;
      lvi.SubItems.Add(shmulyan != 0.0 ? shmulyan.ToString() : String.Empty);
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
      var lvi = lvTable.Items.Add(PlayerToItem(player));
      lvi.Selected = true;
    }

    private void mnuPlayers_Click(object sender, EventArgs e)
    {
      tabControl1.SelectedTab = tabTable;
    }

    void UpdateItem(ListViewItem item, Player player)
    {
      item.SubItems[1].Text = player.Name;
      item.SubItems[2].Text = player.Location;
      item.SubItems[3].Text = player.Title;
      item.Tag = player;
    }

    private void lvTable_DoubleClick(object sender, EventArgs e)
    {
      EditPlayer();
    }

    private void EditPlayer()
    {
      var selectedItems = lvTable.SelectedItems;
      if (selectedItems.Count > 0)
      {
        var item = selectedItems[0];
        var player = PlayerFromItem(item);
        if (playerForm == null)
          playerForm = new PlayerForm(player);
        else
          playerForm.Player = player;
        playerForm.ShowDialog(this);
        if (playerForm.Player != null)
          UpdateItem(item, playerForm.Player);
      }
    }

    private void lvTable_SelectedIndexChanged(object sender, EventArgs e)
    {
      mnuEditPlayer.Enabled = lvTable.SelectedItems.Count > 0;
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      mnuPlayers.Enabled = tabControl1.SelectedTab == tabReg;
      
    }

    private void tbReferee_TextChanged(object sender, EventArgs e)
    {

    }

    GamesForm gamesForm;
    TeamForm teamForm;

    private void mnuGames_Click(object sender, EventArgs e)
    {
      if (gamesForm == null)
      {
        gamesForm = new GamesForm(CurTurnir, this);
        SetGameBounds();
      }
      var selected = lvTable.SelectedIndices;
      if (selected.Count > 0)
        gamesForm.SetPlayer(selected[0]);
      gamesForm.ShowDialog(this);
      UpdatePlayerTable();
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

    /// <summary>
    /// Обновляет турнирную таблицу
    /// </summary>
    internal void UpdatePlayerTable()
    {
      lvTable.BeginUpdate();
      lvTable.Items.Clear();
      CurTurnir.Players.Sort(Turnir.CompareByNumber);
      foreach (Player player in CurTurnir.Players)
      {
        AddPlayer(player);
      }
      AddPlayerColumns();
      lvTable.EndUpdate();
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
      SaveGameBounds();
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

    /// <summary>
    /// Сохраняет размер и положение формы партий
    /// </summary>
    internal void SaveGameBounds()
    {
      if (gamesForm != null)
      {
        xs.WriteSetting(Setting.GameLeft, gamesForm.Left);
        xs.WriteSetting(Setting.GameTop, gamesForm.Top);
        xs.WriteSetting(Setting.GameWidth, gamesForm.Width);
        xs.WriteSetting(Setting.GameHeight, gamesForm.Height);
      }
    }

    /// <summary>
    /// Задает размер и положение формы партий
    /// </summary>
    void SetGameBounds()
    {
      gamesForm.Left = xs.ReadSetting(Setting.GameLeft, gamesForm.Left);
      gamesForm.Top = xs.ReadSetting(Setting.GameTop, gamesForm.Top);
      gamesForm.Width = xs.ReadSetting(Setting.GameWidth, gamesForm.Width);
      gamesForm.Height = xs.ReadSetting(Setting.GameHeight, gamesForm.Height);
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
          if (i < lvTable.Columns.Count)
            lvTable.Columns[i].Width = width;
      }
    }
    void SaveColumnWidth()
    {
       var sb = new StringBuilder();
      for (int i = 0; i < lvTable.Columns.Count; i++)
      {
        if (i > 0) sb.Append(delim);
        sb.Append(lvTable.Columns[i].Width);
      }
      xs.WriteSetting(Setting.Columns, sb.ToString());
    }

    #endregion

    private void mnuNewTurnir_Click(object sender, EventArgs e)
    {
      NewTurnir();
    }

    #endregion

    void SetResults()
    {
      CurTurnir.Players.Sort(CurTurnir.CompareByScore);
      var pcount = CurTurnir.Players.Count;
      ListViewItem lvi;
      Player player;
      Byte place;
      double shmulyan;
      lvTable.BeginUpdate();
      for (int i = 0; i < pcount; i++)
      { 
        lvi = lvTable.Items[i];
        player = (Player)lvi.Tag;
        place = (Byte)(CurTurnir.Players.IndexOf(player) + 1);
        player.Place = place;
        if (lvi.SubItems.Count < pcount + 6)
          lvi.SubItems.Add(place.ToString());
        else
          lvi.SubItems[pcount + 5].Text = place.ToString();
        shmulyan = CurTurnir.Shmulyan(player);
        player.Shmulyan = shmulyan;
        if (lvi.SubItems.Count < pcount + 7)
          lvi.SubItems.Add(shmulyan.ToString());
        else
          lvi.SubItems[pcount + 6].Text = shmulyan.ToString();
      }
      lvTable.EndUpdate();
    }

    private void mnuResults_Click(object sender, EventArgs e)
    {
      SetResults();
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
      CurTurnir.BoardNumber = (Byte)numBoard.Value;
    }

    private void rbPersonal_CheckedChanged(object sender, EventArgs e)
    {
      numBoard.Enabled = !rbPersonal.Checked;
      if (rbPersonal.Checked) colCompetitor.Text = "Фамилия, имя";
    }

    private void rbTeam_CheckedChanged(object sender, EventArgs e)
    {
      numBoard.Enabled = rbTeam.Checked;
      if (rbTeam.Checked) colCompetitor.Text = "Команда";
    }

    /// <summary>
    /// Восстанавливает список участников/команд на вкладке "Участники"
    /// </summary>
    void RestoreCompetitorList()
    {
      lvCompetitors.BeginUpdate();
      lvCompetitors.Items.Clear();
      ListViewItem lvi;
      if (CurTurnir.IsPersonal()) //личный турнир
        foreach (Player player in CurTurnir.Players)
        {
          lvi = new ListViewItem(new string[] {
            player.Number.ToString(), player.Name });
          lvi.Tag = player;
          lvCompetitors.Items.Add(lvi);
        }
      else //командный турнир
        foreach (Team team in CurTurnir.Teams)
        {
          lvi = new ListViewItem(new string[]{
            team.Number.ToString(),team.Name});
          lvi.Tag = team;
          lvCompetitors.Items.Add(lvi);
        }
      lvCompetitors.EndUpdate();
    }

    void AddCompetitor()
    {
      if (CurTurnir.IsPersonal())
        playerForm.ShowDialog();
      else
        teamForm.ShowDialog();
    }

    private void lvCompetitors_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool selected = lvCompetitors.SelectedItems.Count > 0;
      mnuDel.Enabled = selected;
      mnuEditPlayer.Enabled = selected;
    }

  }
}