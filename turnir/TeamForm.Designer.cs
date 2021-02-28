namespace turnir
{
  partial class TeamForm
  {
    /// <summary>
    /// Требуется переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Обязательный метод для поддержки конструктора - не изменяйте
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.cancel = new System.Windows.Forms.Button();
      this.save = new System.Windows.Forms.Button();
      this.gridPlayers = new System.Windows.Forms.DataGridView();
      this.ColBoard = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.name = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.gridPlayers)).BeginInit();
      this.SuspendLayout();
      // 
      // cancel
      // 
      this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancel.Location = new System.Drawing.Point(105, 241);
      this.cancel.Name = "cancel";
      this.cancel.Size = new System.Drawing.Size(75, 23);
      this.cancel.TabIndex = 9;
      this.cancel.Text = "Отмена";
      this.cancel.UseVisualStyleBackColor = true;
      this.cancel.Click += new System.EventHandler(this.cancel_Click);
      // 
      // save
      // 
      this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.save.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.save.Enabled = false;
      this.save.Location = new System.Drawing.Point(12, 241);
      this.save.Name = "save";
      this.save.Size = new System.Drawing.Size(75, 23);
      this.save.TabIndex = 8;
      this.save.Text = "Сохранить";
      this.save.UseVisualStyleBackColor = true;
      this.save.Click += new System.EventHandler(this.save_Click);
      // 
      // gridPlayers
      // 
      this.gridPlayers.AllowUserToAddRows = false;
      this.gridPlayers.AllowUserToDeleteRows = false;
      this.gridPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gridPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColBoard,
            this.ColName,
            this.ColTitle});
      this.gridPlayers.Location = new System.Drawing.Point(12, 73);
      this.gridPlayers.MultiSelect = false;
      this.gridPlayers.Name = "gridPlayers";
      this.gridPlayers.ReadOnly = true;
      this.gridPlayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.gridPlayers.Size = new System.Drawing.Size(401, 150);
      this.gridPlayers.TabIndex = 1;
      this.gridPlayers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridPlayers_KeyUp);
      // 
      // ColBoard
      // 
      this.ColBoard.HeaderText = "Доска";
      this.ColBoard.Name = "ColBoard";
      this.ColBoard.ReadOnly = true;
      this.ColBoard.Width = 50;
      // 
      // ColName
      // 
      this.ColName.HeaderText = "Фамилия, имя";
      this.ColName.Name = "ColName";
      this.ColName.ReadOnly = true;
      this.ColName.Width = 200;
      // 
      // ColTitle
      // 
      this.ColTitle.HeaderText = "Разряд";
      this.ColTitle.Name = "ColTitle";
      this.ColTitle.ReadOnly = true;
      this.ColTitle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.ColTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      // 
      // name
      // 
      this.name.Location = new System.Drawing.Point(12, 25);
      this.name.Name = "name";
      this.name.Size = new System.Drawing.Size(401, 20);
      this.name.TabIndex = 0;
      this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(106, 13);
      this.label1.TabIndex = 12;
      this.label1.Text = "Название команды";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 57);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(90, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "Члены команды";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(199, 57);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(214, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "Нажмите F4 для редактирования строки";
      // 
      // TeamForm
      // 
      this.AcceptButton = this.save;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancel;
      this.ClientSize = new System.Drawing.Size(430, 276);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.name);
      this.Controls.Add(this.gridPlayers);
      this.Controls.Add(this.cancel);
      this.Controls.Add(this.save);
      this.MaximizeBox = false;
      this.Name = "TeamForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Команда";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeamForm_FormClosing);
      this.Load += new System.EventHandler(this.TeamForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.gridPlayers)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancel;
    private System.Windows.Forms.Button save;
    private System.Windows.Forms.DataGridView gridPlayers;
    private System.Windows.Forms.TextBox name;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColBoard;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColTitle;
    private System.Windows.Forms.Label label3;
  }
}