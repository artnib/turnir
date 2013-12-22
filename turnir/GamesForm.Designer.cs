namespace turnir
{
  partial class GamesForm
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
      this.components = new System.ComponentModel.Container();
      this.cbPlayer = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lvGames = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.cmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.white = new System.Windows.Forms.ToolStripMenuItem();
      this.black = new System.Windows.Forms.ToolStripMenuItem();
      this.draw = new System.Windows.Forms.ToolStripMenuItem();
      this.pnGames = new System.Windows.Forms.Panel();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lbScore = new System.Windows.Forms.ToolStripStatusLabel();
      this.pnPlayer = new System.Windows.Forms.Panel();
      this.cmenu.SuspendLayout();
      this.pnGames.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.pnPlayer.SuspendLayout();
      this.SuspendLayout();
      // 
      // cbPlayer
      // 
      this.cbPlayer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbPlayer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cbPlayer.FormattingEnabled = true;
      this.cbPlayer.Location = new System.Drawing.Point(0, 28);
      this.cbPlayer.Name = "cbPlayer";
      this.cbPlayer.Size = new System.Drawing.Size(514, 21);
      this.cbPlayer.TabIndex = 0;
      this.cbPlayer.SelectedIndexChanged += new System.EventHandler(this.cbPlayer_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(514, 28);
      this.label1.TabIndex = 1;
      this.label1.Text = "Участник";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      this.label2.Dock = System.Windows.Forms.DockStyle.Top;
      this.label2.Location = new System.Drawing.Point(0, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(514, 23);
      this.label2.TabIndex = 2;
      this.label2.Text = "Партии участника";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lvGames
      // 
      this.lvGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.lvGames.ContextMenuStrip = this.cmenu;
      this.lvGames.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvGames.FullRowSelect = true;
      this.lvGames.Location = new System.Drawing.Point(0, 23);
      this.lvGames.MultiSelect = false;
      this.lvGames.Name = "lvGames";
      this.lvGames.Size = new System.Drawing.Size(514, 379);
      this.lvGames.TabIndex = 3;
      this.lvGames.UseCompatibleStateImageBehavior = false;
      this.lvGames.View = System.Windows.Forms.View.Details;
      this.lvGames.SelectedIndexChanged += new System.EventHandler(this.lvGames_SelectedIndexChanged);
      this.lvGames.DoubleClick += new System.EventHandler(this.lvGames_DoubleClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Белые";
      this.columnHeader1.Width = 150;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Чёрные";
      this.columnHeader2.Width = 150;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Результат";
      this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader3.Width = 70;
      // 
      // cmenu
      // 
      this.cmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.white,
            this.black,
            this.draw});
      this.cmenu.Name = "cmenu";
      this.cmenu.Size = new System.Drawing.Size(214, 70);
      // 
      // white
      // 
      this.white.Name = "white";
      this.white.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
      this.white.Size = new System.Drawing.Size(213, 22);
      this.white.Text = "Выигрыш белых";
      this.white.Click += new System.EventHandler(this.white_Click);
      // 
      // black
      // 
      this.black.Name = "black";
      this.black.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
      this.black.Size = new System.Drawing.Size(213, 22);
      this.black.Text = "Выигрыш чёрных";
      this.black.Click += new System.EventHandler(this.black_Click);
      // 
      // draw
      // 
      this.draw.Name = "draw";
      this.draw.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
      this.draw.Size = new System.Drawing.Size(213, 22);
      this.draw.Text = "Ничья";
      this.draw.Click += new System.EventHandler(this.draw_Click);
      // 
      // pnGames
      // 
      this.pnGames.AutoSize = true;
      this.pnGames.Controls.Add(this.statusStrip1);
      this.pnGames.Controls.Add(this.lvGames);
      this.pnGames.Controls.Add(this.label2);
      this.pnGames.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnGames.Location = new System.Drawing.Point(0, 61);
      this.pnGames.Name = "pnGames";
      this.pnGames.Size = new System.Drawing.Size(514, 402);
      this.pnGames.TabIndex = 4;
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbScore});
      this.statusStrip1.Location = new System.Drawing.Point(0, 380);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(514, 22);
      this.statusStrip1.TabIndex = 4;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // lbScore
      // 
      this.lbScore.Name = "lbScore";
      this.lbScore.Size = new System.Drawing.Size(39, 17);
      this.lbScore.Text = "Очки:";
      // 
      // pnPlayer
      // 
      this.pnPlayer.Controls.Add(this.cbPlayer);
      this.pnPlayer.Controls.Add(this.label1);
      this.pnPlayer.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnPlayer.Location = new System.Drawing.Point(0, 0);
      this.pnPlayer.Name = "pnPlayer";
      this.pnPlayer.Size = new System.Drawing.Size(514, 61);
      this.pnPlayer.TabIndex = 5;
      // 
      // GamesForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(514, 463);
      this.Controls.Add(this.pnGames);
      this.Controls.Add(this.pnPlayer);
      this.Name = "GamesForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Партии";
      this.Load += new System.EventHandler(this.GamesForm_Load);
      this.cmenu.ResumeLayout(false);
      this.pnGames.ResumeLayout(false);
      this.pnGames.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.pnPlayer.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cbPlayer;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListView lvGames;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.Panel pnGames;
    private System.Windows.Forms.Panel pnPlayer;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ContextMenuStrip cmenu;
    private System.Windows.Forms.ToolStripMenuItem white;
    private System.Windows.Forms.ToolStripMenuItem black;
    private System.Windows.Forms.ToolStripMenuItem draw;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lbScore;
  }
}