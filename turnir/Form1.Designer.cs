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
      this.tabTable = new System.Windows.Forms.TabPage();
      this.lvTable = new System.Windows.Forms.ListView();
      this.colNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.panel1 = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.cbTable = new System.Windows.Forms.ComboBox();
      this.tabStart = new System.Windows.Forms.TabPage();
      this.tbSecretary = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label2 = new System.Windows.Forms.Label();
      this.numBoard = new System.Windows.Forms.NumericUpDown();
      this.rbTeam = new System.Windows.Forms.RadioButton();
      this.rbPersonal = new System.Windows.Forms.RadioButton();
      this.tbReferee = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lbDate = new System.Windows.Forms.Label();
      this.dtDate = new System.Windows.Forms.DateTimePicker();
      this.lnkTurnirs = new System.Windows.Forms.LinkLabel();
      this.lnkNew = new System.Windows.Forms.LinkLabel();
      this.tabReg = new System.Windows.Forms.TabPage();
      this.lvCompetitors = new System.Windows.Forms.ListView();
      this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colCompetitor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
      this.tabControl1.SuspendLayout();
      this.tabTable.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabStart.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numBoard)).BeginInit();
      this.tabReg.SuspendLayout();
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
      this.tabControl1.Controls.Add(this.tabTable);
      this.tabControl1.Controls.Add(this.tabStart);
      this.tabControl1.Controls.Add(this.tabReg);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 24);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(734, 297);
      this.tabControl1.TabIndex = 4;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      // 
      // tabTable
      // 
      this.tabTable.BackColor = System.Drawing.SystemColors.Control;
      this.tabTable.Controls.Add(this.lvTable);
      this.tabTable.Controls.Add(this.panel1);
      this.tabTable.Location = new System.Drawing.Point(4, 22);
      this.tabTable.Name = "tabTable";
      this.tabTable.Padding = new System.Windows.Forms.Padding(3);
      this.tabTable.Size = new System.Drawing.Size(726, 271);
      this.tabTable.TabIndex = 1;
      this.tabTable.Text = "Таблица";
      // 
      // lvTable
      // 
      this.lvTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNumber,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
      this.lvTable.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvTable.FullRowSelect = true;
      this.lvTable.GridLines = true;
      this.lvTable.HideSelection = false;
      this.lvTable.Location = new System.Drawing.Point(3, 30);
      this.lvTable.MultiSelect = false;
      this.lvTable.Name = "lvTable";
      this.lvTable.Size = new System.Drawing.Size(720, 238);
      this.lvTable.TabIndex = 5;
      this.lvTable.UseCompatibleStateImageBehavior = false;
      this.lvTable.View = System.Windows.Forms.View.Details;
      this.lvTable.SelectedIndexChanged += new System.EventHandler(this.lvTable_SelectedIndexChanged);
      this.lvTable.DoubleClick += new System.EventHandler(this.lvTable_DoubleClick);
      // 
      // colNumber
      // 
      this.colNumber.Text = "№";
      this.colNumber.Width = 100;
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
      // columnHeader5
      // 
      this.columnHeader5.Text = "Место";
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "Шмульян";
      // 
      // panel1
      // 
      this.panel1.AutoSize = true;
      this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.cbTable);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(720, 27);
      this.panel1.TabIndex = 8;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(5, 6);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(108, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Турнирная таблица:";
      // 
      // cbTable
      // 
      this.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTable.FormattingEnabled = true;
      this.cbTable.Location = new System.Drawing.Point(119, 3);
      this.cbTable.Name = "cbTable";
      this.cbTable.Size = new System.Drawing.Size(121, 21);
      this.cbTable.TabIndex = 6;
      this.cbTable.SelectedIndexChanged += new System.EventHandler(this.cbTable_SelectedIndexChanged);
      // 
      // tabStart
      // 
      this.tabStart.BackColor = System.Drawing.SystemColors.Control;
      this.tabStart.Controls.Add(this.tbSecretary);
      this.tabStart.Controls.Add(this.label3);
      this.tabStart.Controls.Add(this.groupBox1);
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
      // tbSecretary
      // 
      this.tbSecretary.Location = new System.Drawing.Point(288, 102);
      this.tbSecretary.Name = "tbSecretary";
      this.tbSecretary.Size = new System.Drawing.Size(107, 20);
      this.tbSecretary.TabIndex = 14;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(288, 86);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(107, 13);
      this.label3.TabIndex = 13;
      this.label3.Text = "Главный секретарь";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.numBoard);
      this.groupBox1.Controls.Add(this.rbTeam);
      this.groupBox1.Controls.Add(this.rbPersonal);
      this.groupBox1.Location = new System.Drawing.Point(6, 140);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(192, 70);
      this.groupBox1.TabIndex = 12;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Тип турнира";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(146, 43);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(42, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "досках";
      // 
      // numBoard
      // 
      this.numBoard.Location = new System.Drawing.Point(107, 39);
      this.numBoard.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
      this.numBoard.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.numBoard.Name = "numBoard";
      this.numBoard.Size = new System.Drawing.Size(33, 20);
      this.numBoard.TabIndex = 13;
      this.numBoard.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.numBoard.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
      // 
      // rbTeam
      // 
      this.rbTeam.AutoSize = true;
      this.rbTeam.Location = new System.Drawing.Point(6, 41);
      this.rbTeam.Name = "rbTeam";
      this.rbTeam.Size = new System.Drawing.Size(98, 17);
      this.rbTeam.TabIndex = 11;
      this.rbTeam.TabStop = true;
      this.rbTeam.Text = "командный на";
      this.rbTeam.UseVisualStyleBackColor = true;
      this.rbTeam.CheckedChanged += new System.EventHandler(this.rbTeam_CheckedChanged);
      // 
      // rbPersonal
      // 
      this.rbPersonal.AutoSize = true;
      this.rbPersonal.Location = new System.Drawing.Point(6, 20);
      this.rbPersonal.Name = "rbPersonal";
      this.rbPersonal.Size = new System.Drawing.Size(62, 17);
      this.rbPersonal.TabIndex = 10;
      this.rbPersonal.TabStop = true;
      this.rbPersonal.Text = "личный";
      this.rbPersonal.UseVisualStyleBackColor = true;
      this.rbPersonal.CheckedChanged += new System.EventHandler(this.rbPersonal_CheckedChanged);
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
      // tabReg
      // 
      this.tabReg.Controls.Add(this.lvCompetitors);
      this.tabReg.Location = new System.Drawing.Point(4, 22);
      this.tabReg.Name = "tabReg";
      this.tabReg.Padding = new System.Windows.Forms.Padding(3);
      this.tabReg.Size = new System.Drawing.Size(726, 271);
      this.tabReg.TabIndex = 2;
      this.tabReg.Text = "Участники";
      this.tabReg.UseVisualStyleBackColor = true;
      // 
      // lvCompetitors
      // 
      this.lvCompetitors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.colCompetitor,
            this.colLocation,
            this.colTitle});
      this.lvCompetitors.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvCompetitors.FullRowSelect = true;
      this.lvCompetitors.GridLines = true;
      this.lvCompetitors.HideSelection = false;
      this.lvCompetitors.Location = new System.Drawing.Point(3, 3);
      this.lvCompetitors.MultiSelect = false;
      this.lvCompetitors.Name = "lvCompetitors";
      this.lvCompetitors.Size = new System.Drawing.Size(720, 265);
      this.lvCompetitors.TabIndex = 0;
      this.lvCompetitors.UseCompatibleStateImageBehavior = false;
      this.lvCompetitors.View = System.Windows.Forms.View.Details;
      this.lvCompetitors.SelectedIndexChanged += new System.EventHandler(this.lvCompetitors_SelectedIndexChanged);
      // 
      // columnHeader7
      // 
      this.columnHeader7.Text = "№";
      this.columnHeader7.Width = 30;
      // 
      // colCompetitor
      // 
      this.colCompetitor.Width = 200;
      // 
      // colLocation
      // 
      this.colLocation.Text = "Откуда";
      // 
      // colTitle
      // 
      this.colTitle.Text = "Разряд";
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
      this.mnuSave.Size = new System.Drawing.Size(152, 22);
      this.mnuSave.Text = "Сохранить...";
      this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
      // 
      // mnuOpen
      // 
      this.mnuOpen.Name = "mnuOpen";
      this.mnuOpen.Size = new System.Drawing.Size(152, 22);
      this.mnuOpen.Text = "Открыть...";
      this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
      // 
      // mnuNewTurnir
      // 
      this.mnuNewTurnir.Name = "mnuNewTurnir";
      this.mnuNewTurnir.Size = new System.Drawing.Size(152, 22);
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
      this.mnuDel.Enabled = false;
      this.mnuDel.Name = "mnuDel";
      this.mnuDel.Size = new System.Drawing.Size(173, 22);
      this.mnuDel.Text = "Удалить";
      this.mnuDel.Click += new System.EventHandler(this.mnuDel_Click);
      // 
      // mnuEditPlayer
      // 
      this.mnuEditPlayer.Enabled = false;
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
      this.mnuResults.Enabled = false;
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
      this.tabTable.ResumeLayout(false);
      this.tabTable.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.tabStart.ResumeLayout(false);
      this.tabStart.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numBoard)).EndInit();
      this.tabReg.ResumeLayout(false);
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
    private System.Windows.Forms.TabPage tabTable;
    private System.Windows.Forms.LinkLabel lnkTurnirs;
    private System.Windows.Forms.LinkLabel lnkNew;
    private System.Windows.Forms.Label lbDate;
    private System.Windows.Forms.DateTimePicker dtDate;
    private System.Windows.Forms.TextBox tbReferee;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListView lvTable;
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
    private System.Windows.Forms.TextBox tbSecretary;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown numBoard;
    private System.Windows.Forms.RadioButton rbTeam;
    private System.Windows.Forms.RadioButton rbPersonal;
    private System.Windows.Forms.TabPage tabReg;
    private System.Windows.Forms.ListView lvCompetitors;
    private System.Windows.Forms.ColumnHeader columnHeader7;
    private System.Windows.Forms.ColumnHeader colCompetitor;
    private System.Windows.Forms.ColumnHeader colLocation;
    private System.Windows.Forms.ColumnHeader colTitle;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cbTable;
  }
}

