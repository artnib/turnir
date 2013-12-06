namespace turnir
{
  partial class PlayerForm
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
      this.name = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.location = new System.Windows.Forms.TextBox();
      this.cbTitle = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.save = new System.Windows.Forms.Button();
      this.cancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // name
      // 
      this.name.Location = new System.Drawing.Point(12, 25);
      this.name.Name = "name";
      this.name.Size = new System.Drawing.Size(200, 20);
      this.name.TabIndex = 0;
      this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(82, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Фамилия, имя";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 59);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(74, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Организация";
      // 
      // location
      // 
      this.location.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.location.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.location.Location = new System.Drawing.Point(12, 75);
      this.location.Name = "location";
      this.location.Size = new System.Drawing.Size(200, 20);
      this.location.TabIndex = 3;
      // 
      // cbTitle
      // 
      this.cbTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitle.FormattingEnabled = true;
      this.cbTitle.Items.AddRange(new object[] {
            "-",
            "3 ю.",
            "2 ю.",
            "1 ю.",
            "4",
            "3",
            "2",
            "1",
            "кмс",
            "мс"});
      this.cbTitle.Location = new System.Drawing.Point(12, 129);
      this.cbTitle.Name = "cbTitle";
      this.cbTitle.Size = new System.Drawing.Size(200, 21);
      this.cbTitle.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 113);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(44, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Разряд";
      // 
      // save
      // 
      this.save.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.save.Enabled = false;
      this.save.Location = new System.Drawing.Point(11, 169);
      this.save.Name = "save";
      this.save.Size = new System.Drawing.Size(75, 23);
      this.save.TabIndex = 6;
      this.save.Text = "Сохранить";
      this.save.UseVisualStyleBackColor = true;
      this.save.Click += new System.EventHandler(this.save_Click);
      // 
      // cancel
      // 
      this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancel.Location = new System.Drawing.Point(104, 169);
      this.cancel.Name = "cancel";
      this.cancel.Size = new System.Drawing.Size(75, 23);
      this.cancel.TabIndex = 7;
      this.cancel.Text = "Отмена";
      this.cancel.UseVisualStyleBackColor = true;
      this.cancel.Click += new System.EventHandler(this.cancel_Click);
      // 
      // PlayerForm
      // 
      this.AcceptButton = this.save;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancel;
      this.ClientSize = new System.Drawing.Size(226, 202);
      this.Controls.Add(this.cancel);
      this.Controls.Add(this.save);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cbTitle);
      this.Controls.Add(this.location);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.name);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "PlayerForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Участник";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerForm_FormClosing);
      this.Load += new System.EventHandler(this.PlayerForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox name;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox location;
    private System.Windows.Forms.ComboBox cbTitle;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button save;
    private System.Windows.Forms.Button cancel;
  }
}