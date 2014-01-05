namespace turnir
{
  partial class TurnirForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lbTurnir = new System.Windows.Forms.Label();
      this.tbTurnir = new System.Windows.Forms.TextBox();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPlayers = new System.Windows.Forms.TabPage();
      this.lvPlayers = new System.Windows.Forms.ListView();
      this.colNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabStart = new System.Windows.Forms.TabPage();
      this.tbReferee = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lbDate = new System.Windows.Forms.Label();
      this.dtDate = new System.Windows.Forms.DateTimePicker();
      this.lnkTurnirs = new System.Windows.Forms.LinkLabel();
      this.lnkNew = new System.Windows.Forms.LinkLabel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.турнирToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuNewTurnir = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPlayers = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAddPlaeyr = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuDel = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuEditPlayer = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuGames = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuResults = new System.Windows.Forms.ToolStripMenuItem();
      this.saveDlg = new System.Windows.Forms.SaveFileDialog();
      this.openDlg = new System.Windows.Forms.OpenFileDialog();
      this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabControl1.SuspendLayout();
      this.tabPlayers.SuspendLayout();
      this.tabStart.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbTurnir
      // 
      this.lbTurnir.AutoSize = true;
      this.lbTurnir.Location = new System.Drawing.Point(3, 10);
      this.lbTurnir.Name = "lbTurnir";
      this.lbTurnir.Size = new System.Drawing.Size(100, 13);
      this.lbTurnir.TabIndex = 0;
      this.lbTurnir.Text = "Название турнира";
      // 
      // tbTurnir
      // 
      this.tbTurnir.Location = new System.Drawing.Point(6, 26);
      this.tbTurnir.Multiline = true;
      this.tbTurnir.Name = "tbTurnir";
      this.tbTurnir.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tbTurnir.Size = new System.Drawing.Size(500, 39);
      this.tbTurnir.TabIndex = 1;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPlayers);
      this.tabControl1.Controls.Add(this.tabStart);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 24);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(734, 297);
      this.tabControl1.TabIndex = 4;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      // 
      // tabPlayers
      // 
      this.tabPlayers.BackColor = System.Drawing.SystemColors.Control;
      this.tabPlayers.Controls.Add(this.lvPlayers);
      this.tabPlayers.Location = new System.Drawing.Point(4, 22);
      this.tabPlayers.Name = "tabPlayers";
      this.tabPlayers.Padding = new System.Windows.Forms.Padding(3);
      this.tabPlayers.Size = new System.Drawing.Size(726, 271);
      this.tabPlayers.TabIndex = 1;
      this.tabPlayers.Text = "Участники";
      // 
      // lvPlayers
      // 
      this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNumber,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
      this.lvPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvPlayers.FullRowSelect = true;
      this.lvPlayers.GridLines = true;
      this.lvPlayers.HideSelection = false;
      this.lvPlayers.Location = new System.Drawing.Point(3, 3);
      this.lvPlayers.MultiSelect = false;
      this.lvPlayers.Name = "lvPlayers";
      this.lvPlayers.Size = new System.Drawing.Size(720, 265);
      this.lvPlayers.TabIndex = 5;
      this.lvPlayers.UseCompatibleStateImageBehavior = false;
      this.lvPlayers.View = System.Windows.Forms.View.Details;
      this.lvPlayers.SelectedIndexChanged += new System.EventHandler(this.lvPlayers_SelectedIndexChanged);
      this.lvPlayers.DoubleClick += new System.EventHandler(this.lvPlayers_DoubleClick);
      // 
      // colNumber
      // 
      this.colNumber.Text = "№";
      this.colNumber.Width = 30;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Участник";
      this.columnHeader1.Width = 200;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Откуда";
      this.columnHeader2.Width = 150;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Разряд";
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Очки";
      // 
      // tabStart
      // 
      this.tabStart.BackColor = System.Drawing.SystemColors.Control;
      this.tabStart.Controls.Add(this.tbReferee);
      this.tabStart.Controls.Add(this.label1);
      this.tabStart.Controls.Add(this.lbDate);
      this.tabStart.Controls.Add(this.dtDate);
      this.tabStart.Controls.Add(this.lnkTurnirs);
      this.tabStart.Controls.Add(this.lnkNew);
      this.tabStart.Controls.Add(this.tbTurnir);
      this.tabStart.Controls.Add(this.lbTurnir);
      this.tabStart.Location = new System.Drawing.Point(4, 22);
      this.tabStart.Name = "tabStart";
      this.tabStart.Padding = new System.Windows.Forms.Padding(3);
      this.tabStart.Size = new System.Drawing.Size(726, 271);
      this.tabStart.TabIndex = 0;
      this.tabStart.Text = "Турнир";
      // 
      // tbReferee
      // 
      this.tbReferee.Location = new System.Drawing.Point(166, 102);
      this.tbReferee.Name = "tbReferee";
      this.tbReferee.Size = new System.Drawing.Size(100, 20);
      this.tbReferee.TabIndex = 9;
      this.tbReferee.TextChanged += new System.EventHandler(this.tbReferee_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(166, 86);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 13);
      this.label1.TabIndex = 8;
      this.label1.Text = "Главный судья";
      // 
      // lbDate
      // 
      this.lbDate.AutoSize = true;
      this.lbDate.Location = new System.Drawing.Point(8, 86);
      this.lbDate.Name = "lbDate";
      this.lbDate.Size = new System.Drawing.Size(96, 13);
      this.lbDate.TabIndex = 7;
      this.lbDate.Text = "Дата проведения";
      // 
      // dtDate
      // 
      this.dtDate.Location = new System.Drawing.Point(6, 102);
      this.dtDate.Name = "dtDate";
      this.dtDate.Size = new System.Drawing.Size(140, 20);
      this.dtDate.TabIndex = 6;
      // 
      // lnkTurnirs
      // 
      this.lnkTurnirs.AutoSize = true;
      this.lnkTurnirs.Location = new System.Drawing.Point(521, 52);
      this.lnkTurnirs.Name = "lnkTurnirs";
      this.lnkTurnirs.Size = new System.Drawing.Size(53, 13);
      this.lnkTurnirs.TabIndex = 5;
      this.lnkTurnirs.TabStop = true;
      this.lnkTurnirs.Text = "Другой...";
      // 
      // lnkNew
      // 
      this.lnkNew.AutoSize = true;
      this.lnkNew.Location = new System.Drawing.Point(521, 26);
      this.lnkNew.Name = "lnkNew";
      this.lnkNew.Size = new System.Drawing.Size(41, 13);
      this.lnkNew.TabIndex = 4;
      this.lnkNew.TabStop = true;
      this.lnkNew.Text = "Новый";
      this.lnkNew.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNew_LinkClicked);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.турнирToolStripMenuItem,
            this.mnuPlayers,
            this.mnuGames,
            this.mnuResults});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(734, 24);
      this.menuStrip1.TabIndex = 6;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // турнирToolStripMenuItem
      // 
      this.турнирToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuOpen,
            this.mnuNewTurnir});
      this.турнирToolStripMenuItem.Name = "турнирToolStripMenuItem";
      this.турнирToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
      this.турнирToolStripMenuItem.Text = "Турнир";
      // 
      // mnuSave
      // 
      this.mnuSave.Name = "mnuSave";
      this.mnuSave.Size = new System.Drawing.Size(141, 22);
      this.mnuSave.Text = "Сохранить...";
      this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
      // 
      // mnuOpen
      // 
      this.mnuOpen.Name = "mnuOpen";
      this.mnuOpen.Size = new System.Drawing.Size(141, 22);
      this.mnuOpen.Text = "Открыть...";
      this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
      // 
      // mnuNewTurnir
      // 
      this.mnuNewTurnir.Name = "mnuNewTurnir";
      this.mnuNewTurnir.Size = new System.Drawing.Size(141, 22);
      this.mnuNewTurnir.Text = "Создать...";
      this.mnuNewTurnir.Click += new System.EventHandler(this.mnuNewTurnir_Click);
      // 
      // mnuPlayers
      // 
      this.mnuPlayers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddPlaeyr,
            this.mnuDel,
            this.mnuEditPlayer});
      this.mnuPlayers.Name = "mnuPlayers";
      this.mnuPlayers.Size = new System.Drawing.Size(77, 20);
      this.mnuPlayers.Text = "Участники";
      // 
      // mnuAddPlaeyr
      // 
      this.mnuAddPlaeyr.Name = "mnuAddPlaeyr";
      this.mnuAddPlaeyr.ShortcutKeys = System.Windows.Forms.Keys.Insert;
      this.mnuAddPlaeyr.Size = new System.Drawing.Size(173, 22);
      this.mnuAddPlaeyr.Text = "Добавить";
      this.mnuAddPlaeyr.Click += new System.EventHandler(this.mnuAddPlaeyr_Click);
      // 
      // mnuDel
      // 
      this.mnuDel.Name = "mnuDel";
      this.mnuDel.Size = new System.Drawing.Size(173, 22);
      this.mnuDel.Text = "Удалить";
      this.mnuDel.Click += new System.EventHandler(this.mnuDel_Click);
      // 
      // mnuEditPlayer
      // 
      this.mnuEditPlayer.Name = "mnuEditPlayer";
      this.mnuEditPlayer.ShortcutKeys = System.Windows.Forms.Keys.F4;
      this.mnuEditPlayer.Size = new System.Drawing.Size(173, 22);
      this.mnuEditPlayer.Text = "Редактировать";
      this.mnuEditPlayer.Click += new System.EventHandler(this.mnuEditPlayer_Click);
      // 
      // mnuGames
      // 
      this.mnuGames.Name = "mnuGames";
      this.mnuGames.Size = new System.Drawing.Size(60, 20);
      this.mnuGames.Text = "Партии";
      this.mnuGames.Click += new System.EventHandler(this.mnuGames_Click);
      // 
      // mnuResults
      // 
      this.mnuResults.Name = "mnuResults";
      this.mnuResults.Size = new System.Drawing.Size(52, 20);
      this.mnuResults.Text = "Итоги";
      this.mnuResults.Click += new System.EventHandler(this.mnuResults_Click);
      // 
      // saveDlg
      // 
      this.saveDlg.DefaultExt = "tur";
      this.saveDlg.Filter = "Турниры|*.tur";
      this.saveDlg.Title = "Сохранение турнира";
      // 
      // openDlg
      // 
      this.openDlg.Filter = "Турниры|*.tur";
      this.openDlg.Title = "Открытие турнира";
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Место";
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "Шмульян";
      // 
      // TurnirForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(734, 321);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "TurnirForm";
      this.Text = "Турнир";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPlayers.ResumeLayout(false);
      this.tabStart.ResumeLayout(false);
      this.tabStart.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbTurnir;
    private System.Windows.Forms.TextBox tbTurnir;
    private System.Windows.Forms.DataGridViewTextBoxColumn playerDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn playerIdDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn groupIdDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabStart;
    private System.Windows.Forms.TabPage tabPlayers;
    private System.Windows.Forms.LinkLabel lnkTurnirs;
    private System.Windows.Forms.LinkLabel lnkNew;
    private System.Windows.Forms.Label lbDate;
    private System.Windows.Forms.DateTimePicker dtDate;
    private System.Windows.Forms.TextBox tbReferee;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListView lvPlayers;
    private System.Windows.Forms.ColumnHeader colNumber;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem турнирToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuPlayers;
    private System.Windows.Forms.ToolStripMenuItem mnuAddPlaeyr;
    private System.Windows.Forms.ToolStripMenuItem mnuGames;
    private System.Windows.Forms.ToolStripMenuItem mnuDel;
    private System.Windows.Forms.ToolStripMenuItem mnuSave;
    private System.Windows.Forms.SaveFileDialog saveDlg;
    private System.Windows.Forms.ToolStripMenuItem mnuOpen;
    private System.Windows.Forms.OpenFileDialog openDlg;
    private System.Windows.Forms.ToolStripMenuItem mnuNewTurnir;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ToolStripMenuItem mnuEditPlayer;
    private System.Windows.Forms.ToolStripMenuItem mnuResults;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
  }
}

