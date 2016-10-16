
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
    
    #region Турнирная таблица

    void FillTableCombo()
    {
      cbTable.Items.Clear();
      if (CurTurnir.IsTeam())
      {
        cbTable.Items.Add("Команды");
        for (int i = 1; i <= CurTurnir.BoardNumber; i++)
          cbTable.Items.Add(String.Format("{0} доска", i));
      }
      else
        cbTable.Items.Add("Все участники");
      cbTable.SelectedIndex = 0;
    }

    #endregion

    void SaveTurnir(string turPath)
    {
      if (String.IsNullOrEmpty(turPath)) //несохранённый турнир
      {
        saveDlg.Filter = "Турниры|*.tur";
        saveDlg.DefaultExt = "tur";
        if (saveDlg.ShowDialog(this) == DialogResult.OK)
        {
          turPath = saveDlg.FileName;
        }
        else //отмена сохранения
          return;
      }
      var fs = new FileStream(turPath, FileMode.Create);
      var bf = new BinaryFormatter();
      CurTurnir.Date = dtDate.Value;
      CurTurnir.Name = tbTurnir.Text;
      CurTurnir.Referee = tbReferee.Text;
      CurTurnir.Secretary = tbSecretary.Text;
      CurTurnir.Place = tbPlace.Text;
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
          tbPlace.Text = CurTurnir.Place;
          CheckTurnirType(CurTurnir);
          TurChangesEnabled();
          FillTableCombo();
          RestoreCompetitorList();
        }
        fs.Close();
        curFile = turPath;
        UpdateCaption();
      }
      if (CurTurnir == null) InitNewTurnir();
      UpdateGameForm();
    }

    private void UpdateGameForm()
    {
      if (gamesForm != null)
        gamesForm.SetTurnir(CurTurnir);
    }

    void CheckTurnirType(Turnir tur)
    {
      bool teamTur = tur.IsTeam();
      noChange = true;
      if (teamTur)
      {
        rbTeam.Checked = true;
        numBoard.Value = CurTurnir.BoardNumber;
      }
      else
      {
        rbPersonal.Checked = true;
      }
      noChange = false;
    }

    bool noChange;
    const string defaultCaption = "Турнир";

    void SaveAndInit()
    {
      if (!CurTurnir.IsEmpty())
        SaveTurnir();
      InitNewTurnir();
    }

    /// <summary>
    /// Подготовка к работе с новым турниром
    /// </summary>
    void InitNewTurnir()
    {
      curFile = String.Empty;
      CurTurnir = new Turnir();
      Text = defaultCaption;
      dtDate.Value = DateTime.Now;
      CheckTurnirType(CurTurnir);
      FillTableCombo();
      dgvTable.Rows.Clear();
      lvCompetitors.Items.Clear();
      UpdateGameForm();
    }

    /// <summary>
    /// Начальная ширина столбца с очками за партию
    /// </summary>
    const int columnWidth = 30;

    const int baseTeamCols = 5;
    const int tempTeamStart = 3;
    const int basePlayerCols = 7;
    const int tempPlayerStart = 4;
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
      tabControl1.SelectedIndex = xs.ReadSetting(Setting.LastTab, 0);
      cbTable.SelectedIndex = xs.ReadSetting(Setting.TableIndex, 0);
    }

    #endregion

    private void SaveTurnir()
    {
      SaveTurnir(curFile);
    }
 
    #region Меню "Участники"

    private void mnuAddPlaeyr_Click(object sender, EventArgs e)
    {
      ListViewItem competitor;
      if (CurTurnir.IsPersonal())
      {
        var player = GetNewPlayer();
        if (player == null) return;
        var lvItems = lvCompetitors.Items;
        player.Number = (Byte)(lvItems.Count + 1);
        competitor = GetItem(player);
        CurTurnir.AddPlayer(player);
      }
      else
      {
        var team = GetNewTeam();
        if (team == null) return;
        competitor = GetItem(team);
        CurTurnir.Teams.Add(team);
      }
      lvCompetitors.Items.Add(competitor);
    }

    private static ListViewItem GetItem(Team team)
    {
      ListViewItem lvi;
      lvi = new ListViewItem(new string[] { team.Number.ToString(), team.Name });
      lvi.Tag = team;
      return lvi;
    }

    private static ListViewItem GetItem(Player player)
    {
      var lvi = new ListViewItem(new string[] { player.Number.ToString(),
        player.Name, player.Location, player.Title });
      lvi.Tag = player;
      return lvi;
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
      {
        teamForm = new TeamForm(CurTurnir);
        SetTeamBounds();
      }
      teamForm.Team = new Team {
        Number = (Byte)(lvCompetitors.Items.Count + 1) };
      if (teamForm.ShowDialog(this) == DialogResult.OK)
        return teamForm.Team;
      else
        return null;
    }

    private void mnuDel_Click(object sender, EventArgs e)
    {
      var competitor = lvCompetitors.SelectedItems[0].Tag;
      if (competitor is Team)
        CurTurnir.RemoveTeam((Team)competitor);
      else
        CurTurnir.RemovePlayer((Player)competitor);
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
        {
          teamForm = new TeamForm(team, CurTurnir);
          SetTeamBounds();
        }
        teamForm.ShowDialog(this);
        lvi.SubItems[1].Text = team.Name;
      }
    }

    private void MoveDown_Click(object sender, EventArgs e)
    {
      MoveCompetitor(false);
    }

    void MoveCompetitor(bool up)
    {
      var competitor = lvCompetitors.SelectedItems[0].Tag;
      var index2 = up ? lvCompetitors.SelectedIndices[0] - 1
        : lvCompetitors.SelectedIndices[0] + 1;
      if (competitor is Team)
      {
        var team1 = (Team)competitor;
        var team2 = (Team)lvCompetitors.Items[index2].Tag;
        CurTurnir.ChangeTeams(team1, team2);
        CurTurnir.Teams.Sort(Turnir.CompareByNumber);
      }
      else
      {
        var player1 = (Player)competitor;
        var player2 = (Player)lvCompetitors.Items[index2].Tag;
        CurTurnir.ChangePlayers(player1, player2);
        CurTurnir.Players.Sort(Turnir.CompareByNumber);
      }
      RestoreCompetitorList();
      lvCompetitors.Items[index2].Selected = true;
    }

    private void MoveUp_Click(object sender, EventArgs e)
    {
      MoveCompetitor(true);
    }

    #endregion

    void FillTableRow(Team team)
    {
      var row = dgvTable.Rows[team.Number - 1];
      row.Cells[0].Value = team.Number;
      row.Cells[1].Value = team.Name;
      row.Cells[2].Value = null;
      var teamCount = CurTurnir.Teams.Count;
      Double score;
      Double totalScore = 0.0;
      int j = 0;
      for (int i = 1; i <= teamCount; i++)
      {
        j = 2 + i;
        if (i == team.Number) //свой столбец
          row.Cells[j].Value = "X";
        else
        {
          score = CurTurnir.TeamScore(team,
            CurTurnir.Teams.Find(t => t.Number == i));
          if (Double.IsNaN(score))
            row.Cells[j].Value = null;
          else
          {
            row.Cells[j].Value = score;
            totalScore += score;
          }
        }
      }
      if (j > 0)
        row.Cells[j + 1].Value = totalScore;
      byte? place = team.Place;
      row.Cells[j + 2].Value = place > 0 ? place : null;
    }

    void FillTableRow(Player player)
    {
      var row = dgvTable.Rows[player.Number - 1];
      row.Tag = player;
      row.Cells[0].Value = player.Number;
      row.Cells[1].Value = player.Name;
      row.Cells[2].Value = player.Location;
      row.Cells[3].Value = player.Grade == null ? player.Title : player.Grade.ShortName;
      var board = player.Board;
      var playerCount = CurTurnir.Players.FindAll(p => p.Board == board).Count;
      int j = 0;
      double score;
      double totalScore = 0.0;
      for (byte i = 1; i <= playerCount; i++)
      {
        j = 3 + i;
        if (i == player.Number)
          row.Cells[j].Value = "X";
        else
        {
          score = CurTurnir.GameScore(player, i);
          if (Double.IsNaN(score))
            row.Cells[j].Value = null;
          else
          {
            row.Cells[j].Value = score;
            totalScore += score;
          }
        }
      }
      if (j > 0)
        row.Cells[j + 1].Value = totalScore;
      byte? place = player.Place;
      row.DefaultCellStyle.BackColor =RowColor(player.Place);
      row.Cells[j + 2].Value = place > 0 ? place : null;
      double? shmulyan = player.Shmulyan;
      row.Cells[j + 3].Value = shmulyan != 0.0 ? shmulyan : null;
      //if (player.NewGrade != null)
        row.Cells[j + 4].Value = player.NewGrade;
    }

    static Color RowColor(byte place)
    {
      switch (place)
      {
        case 1:
          return Color.Gold;
        case 2:
          return Color.Silver;
        case 3:
          return Color.RosyBrown;
        default:
          return SystemColors.Window;
      }
    }

    /// <summary>
    /// Возвращает строку турнирной таблицы для участника
    /// </summary>
    /// <param name="player">Участник</param>
    /// <returns></returns>
    ListViewItem PlayerToItem(Player player)
    {
      var lvi = new ListViewItem(new string[]{
          player.Number.ToString(), player.Name, player.Location, player.Title});
      double totalScore = 0.0;
      double score = 0.0;
      var board = player.Board;
      var playerCount = CurTurnir.Players.FindAll(p => p.Board == board).Count;
      for (int i = 1; i <= playerCount; i++)
      {
        if (i == player.Number)
          lvi.SubItems.Add("X");
        else
        {
          var game = CurTurnir.Games.Find(g => g.Board == board &&
            ((g.Black == player.Number && g.White == i)
            || (g.Black==i && g.White==player.Number)));
          if (game != null) //партия найдена
          {
            if (game.Result == GameResult.None)
              lvi.SubItems.Add(String.Empty);
            else //партия сыграна
            {
              score = GameScore(game, player);
              lvi.SubItems.Add(score.ToString());
              totalScore += score;
            }
          }
          else //партия не найдена
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

    private void EditPlayer()
    {
      var selectedItems = lvCompetitors.SelectedItems;
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

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      mnuPlayers.Enabled = tabControl1.SelectedTab == tabReg;
      switch (tabControl1.SelectedIndex)
      {
        case 0:
          UpdatePlayerTable();
          break;
        case 1:
          TurChangesEnabled();
          break;
      }
    }

    private void TurChangesEnabled()
    {
      bool changesEnabled = CurTurnir.Started();
      rbPersonal.Enabled = changesEnabled;
      rbTeam.Enabled = changesEnabled;
      numBoard.Enabled = changesEnabled;
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
      var selected = dgvTable.SelectedRows;
      if (selected.Count > 0)
        gamesForm.Filter = dgvTable.SelectedRows[0].Tag;
      gamesForm.ShowDialog(this);
      UpdatePlayerTable();
    }

    #region Работа с файлами турниров

    BinaryFormatter bf;
    FileStream fs;
    string curFile;
    
    private void mnuSave_Click(object sender, EventArgs e)
    {
      SaveTurnir();
    }

    private void mnuSaveAs_Click(object sender, EventArgs e)
    {
      saveDlg.Filter = "Турниры|*.tur";
      saveDlg.DefaultExt = "tur";
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
      if (!CurTurnir.IsEmpty())
        SaveTurnir();
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

    internal void UpdatePlayerTable()
    {
      UpdatePlayerTable(CurTurnir.IsTeam() ? cbTable.SelectedIndex : 1);
    }

    /// <summary>
    /// Обновляет турнирную таблицу
    /// </summary>
    /// <param name="board">Номер доски</param>
    void UpdatePlayerTable(Int32 board)
    {
      if (Double.IsNaN(CurTurnir.Coefficient(board)))
        lbCoeff.Visible = false;
      else
      {
        lbCoeff.Text = String.Format("Коэффициент турнира: {0:F2}. Можно выполнить: {1}",
          CurTurnir.Coefficient(board),CurTurnir.HighestTitle(board).ShortName);
        lbCoeff.Visible = true;
      }
      UpdateGrid(board);
      if (board > 0 || CurTurnir.IsPersonal()) //личная таблица или таблица по доскам
      {
        var players = CurTurnir.Players.FindAll(p => p.Board == board);
        players.Sort(Turnir.CompareByNumber);
        foreach (Player player in players)
        {
          FillTableRow(player);
        }
      }
      else //командная таблица
      {
        foreach (Team team in CurTurnir.Teams)
        {
          FillTableRow(team);
        }
      }
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
      //SaveColumnWidth();
      SaveGameBounds();
      SaveTeamBounds();
      xs.WriteSetting(Setting.LastFile, curFile);
      xs.WriteSetting(Setting.LastTab, tabControl1.SelectedIndex);
      xs.WriteSetting(Setting.TableIndex, cbTable.SelectedIndex);
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

    #region Настройки формы партий

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
    
    #endregion

    #region Настройки формы команды

    internal void SaveTeamBounds()
    {
      if (teamForm == null) return;
      xs.WriteSetting(Setting.TeamLeft, teamForm.Left);
      xs.WriteSetting(Setting.TeamTop, teamForm.Top);
      xs.WriteSetting(Setting.TeamWidth, teamForm.Width);
      xs.WriteSetting(Setting.TeamHeight, teamForm.Height);
    }

    void SetTeamBounds()
    {
      teamForm.Left = xs.ReadSetting(Setting.TeamLeft, teamForm.Left);
      teamForm.Top = xs.ReadSetting(Setting.TeamTop, teamForm.Top);
      teamForm.Width = xs.ReadSetting(Setting.TeamWidth, teamForm.Width);
      teamForm.Height = xs.ReadSetting(Setting.TeamHeight, teamForm.Height);
    }

    #endregion

    #region Ширина столбцов таблицы

    const char delim = ';';
    
    #endregion
    
    #endregion

    private void mnuNewTurnir_Click(object sender, EventArgs e)
    {
      SaveAndInit();
    }

    void SetResults()
    {
      CurTurnir.UpdateResults();
      var board = CurTurnir.IsTeam() ? cbTable.SelectedIndex : 1;
        UpdatePlayerTable((Byte)board);
    }

    private void mnuResults_Click(object sender, EventArgs e)
    {
      SetResults();
    }

    #region Сведения о турнире

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
      SetBoardNumber();
    }

    private void SetBoardNumber()
    {
      CurTurnir.BoardNumber = (Byte)(rbPersonal.Checked ? 1 : numBoard.Value);
    }

    void TurTypeChanged()
    {
      numBoard.Enabled = rbTeam.Checked;
      colCompetitor.Text = rbTeam.Checked ? "Команда" : "Фамилия, имя";
      SetBoardNumber();
    }

    private void rbPersonal_CheckedChanged(object sender, EventArgs e)
    {
      //numBoard.Enabled = !rbPersonal.Checked;
      //if (rbPersonal.Checked) colCompetitor.Text = "Фамилия, имя";
      if (noChange) return;
      TurTypeChanged();
    }

    private void rbTeam_CheckedChanged(object sender, EventArgs e)
    {
      if (noChange) return;
      TurTypeChanged();
      //if (rbTeam.Checked) colCompetitor.Text = "Команда";
    }

    #endregion

    /// <summary>
    /// Восстанавливает список участников/команд на вкладке "Участники"
    /// </summary>
    void RestoreCompetitorList()
    {
      lvCompetitors.BeginUpdate();
      lvCompetitors.Items.Clear();
      if (CurTurnir.IsPersonal()) //личный турнир
        foreach (Player player in CurTurnir.Players)
        {
          lvCompetitors.Items.Add(GetItem(player));
        }
      else //командный турнир
        foreach (Team team in CurTurnir.Teams)
        {
          lvCompetitors.Items.Add(GetItem(team));
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
      mnuDel.Enabled = selected && CurTurnir.PlayedGames == 0;
      mnuEditPlayer.Enabled = selected;
      MoveDown.Enabled = selected &&
        lvCompetitors.SelectedIndices[0] < (lvCompetitors.Items.Count - 1);
      MoveUp.Enabled = selected &&
        lvCompetitors.SelectedIndices[0] > 0;
    }

    private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
    {
      var board = CurTurnir.IsTeam() ? cbTable.SelectedIndex : 1;
      UpdateGrid(board);
      UpdatePlayerTable((Byte)board);
    }

    const string teamHeaders = "№;Команда;Откуда;Очки;Место";
    const string playerHeaders = "№;Участник;Откуда;Разряд;Очки;Место;Шмульян;Квал.";
    const string boardHeaders = "№;Участник;Команда;Разряд;Очки;Место;Шмульян";

    void UpdateGrid(int board)
    {
      string[] headers;
      var delims = new char[]{delim};
      string hdrs;
      int tempStart = tempPlayerStart;
      if (CurTurnir.IsPersonal())
        hdrs = playerHeaders;
      else
        if (board > 0)
          hdrs = boardHeaders;
        else
        {
          hdrs = teamHeaders;
          tempStart = tempTeamStart;
        }
      headers = hdrs.Split(delims);
      var rowCount = CurTurnir.IsTeam() ? CurTurnir.Teams.Count :
        CurTurnir.Players.Count;
      if (rowCount == 0)
        dgvTable.Rows.Clear();
      else
        dgvTable.RowCount = rowCount;
      dgvTable.ColumnCount = headers.Length + rowCount;
      for (int i = 0; i < tempStart; i++)
        dgvTable.Columns[i].HeaderText = headers[i];
      for (int i = 0; i < rowCount; i++)
        dgvTable.Columns[tempStart + i].HeaderText = (i + 1).ToString();
      for (int i = tempStart + rowCount; i < dgvTable.ColumnCount; i++)
        dgvTable.Columns[i].HeaderText = headers[i - rowCount];
    }

    #region Перетаскивание файла

    private void TurnirForm_DragDrop(object sender, DragEventArgs e)
    {
      if (!String.IsNullOrEmpty(dropFile))
      {
        if (!CurTurnir.IsEmpty())
          SaveTurnir();
        RestoreTurnir(dropFile);
      }
    }

    const string extension = ".tur";
    string dropFile;

    private void TurnirForm_DragEnter(object sender, DragEventArgs e)
    {
      dropFile = null;
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        if (Path.GetExtension(files[0]).Equals(extension,
          StringComparison.InvariantCultureIgnoreCase))
        {
          e.Effect = DragDropEffects.All;
          dropFile = files[0];
        }
      }
    }

    #endregion

    private void mnuHtml_Click(object sender, EventArgs e)
    {
      saveDlg.Filter = "Веб-страница|*.htm";
      saveDlg.DefaultExt = "htm";
      saveDlg.FileName = Path.GetFileNameWithoutExtension(curFile);
      if (saveDlg.ShowDialog() == DialogResult.OK)
      {
        var hw = new HtmlWriter(CurTurnir);
        var htmlFile = saveDlg.FileName;
        hw.SaveTable(htmlFile, dgvTable, cbTable.SelectedIndex);
        System.Diagnostics.Process.Start(htmlFile);
      }
    }

    private void tbTurnir_TextChanged_1(object sender, EventArgs e)
    {
      CurTurnir.Name = tbTurnir.Text;
    }

    private void tbPlace_TextChanged(object sender, EventArgs e)
    {
      CurTurnir.Place = tbPlace.Text;
    }
  }
}