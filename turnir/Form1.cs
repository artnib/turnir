using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace turnir
{
  public partial class Form1 : Form
  {
    /// <summary>
    /// Сохраняет данные текущего турнира
    /// </summary>
    void SaveTurnir()
    {
      if (CurTurnir == null)
      {
        CurTurnir = new Turnir();
      }
      if(String.IsNullOrEmpty(CurTurnir.FileName))
      {
        FileName = Guid.NewGuid().ToString("N") + ".tur";
      }
      var TurnirPath = Path.Combine(AppDir, FileName);
      var bw = new BinaryWriter(File.Create(TurnirPath));
      bw.Write(dtDate.Value.ToBinary());
      bw.Write(tbTurnir.Text);
      bw.Close();
      WriteLastTurnir();
    }

    string turPath;

    Player PlayerFromItem(ListViewItem lvi)
    { 
      return new Player
      {
        Number = Byte.Parse(lvi.SubItems[0].Text),
        Name = lvi.SubItems[1].Text,
        Location = lvi.SubItems[2].Text
      };
    }

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
      foreach (Player player in players)
      {
        AddPlayer(player);
      }
    }

    void SaveTurnir2()
    {
      var fs = new FileStream(curFile, FileMode.Create);
      var bf = new BinaryFormatter();
      CurTurnir.Date = dtDate.Value;
      CurTurnir.Name = tbTurnir.Text;
      CurTurnir.Referee = tbReferee.Text;
      //CurTurnir.Players = PlayersFromListView();
      bf.Serialize(fs, CurTurnir);
      fs.Close();
    }

    void WriteLastTurnir()
    {
      var sw = new StreamWriter(LastPath);
      sw.Write(curFile);
      sw.Close();
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
        else
          CurTurnir = new Turnir();
        fs.Close();
        curFile = turPath;
      }
    }

    /// <summary>
    /// Подготовка к работе с новым турниром
    /// </summary>
    void NewTurnir()
    {
      tbTurnir.Text = "Турнир";
      dtDate.Value = DateTime.Now;
      AllPlayers = new List<Player>();
    }

    Turnir TurnirInfo(string FilePath)
    {
      var t = new Turnir();
      t.FileName = Path.GetFileName(FilePath);
      var br = new BinaryReader(File.Open(FilePath,FileMode.Open));
      t.Date = DateTime.FromBinary(br.ReadInt64());
      t.Name = br.ReadString();
      br.Close();
      return t;
    }

    /// <summary>
    /// Список участников, отображаемый в текущий момент
    /// </summary>
    List<Player> AllPlayers;
    List<Turnir> AllTurnirs = new List<Turnir>();
    object LastTurnir;
    string AppDir;
    Turnir CurTurnir;
    string FileName;
    string FilePath;
    string LastPath;

    PlayerForm playerForm;

    public Form1()
    {
      InitializeComponent();
    }

    #region События формы

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      //SaveTurnir();
      SaveTurnir2();
      WriteLastTurnir();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {

    }

    private void Form1_Load(object sender, System.EventArgs e)
    {
      bf = new BinaryFormatter();
      AppDir = Path.GetDirectoryName(Application.ExecutablePath);
      LastPath = Path.Combine(AppDir, "last.txt");
      var files = Directory.GetFiles(AppDir, "*.tur");
      //foreach (string file in files)
      //{
      //  AllTurnirs.Add(TurnirInfo(file));
      //}
      //turPath = Path.Combine(AppDir, "turnir.bin");
      turPath = File.ReadAllText(LastPath);
      //RestoreTurnir();
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
      if (String.IsNullOrEmpty(tbTurnir.Text))
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
      AddPlayer(player);
    }

    ListViewItem PlayerToItem(Player player)
    {
      return new ListViewItem(new string[]{
          player.Number.ToString(), player.Name, player.Location});
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
    }

    private void mnuDel_Click(object sender, EventArgs e)
    {

    }
    
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
      }
    }

    private void mnuOpen_Click(object sender, EventArgs e)
    {
      if (openDlg.ShowDialog() == DialogResult.OK)
      {
        curFile = openDlg.FileName;
        fs = File.OpenRead(curFile);
        CurTurnir = (Turnir)bf.Deserialize(fs);
        fs.Close();
      }
    }
  }
}