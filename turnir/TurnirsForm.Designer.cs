namespace turnir
{
  partial class TurnirsForm
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
      this.lvTurnirs = new System.Windows.Forms.ListView();
      this.SuspendLayout();
      // 
      // lvTurnirs
      // 
      this.lvTurnirs.Location = new System.Drawing.Point(72, 53);
      this.lvTurnirs.Name = "lvTurnirs";
      this.lvTurnirs.Size = new System.Drawing.Size(121, 97);
      this.lvTurnirs.TabIndex = 0;
      this.lvTurnirs.UseCompatibleStateImageBehavior = false;
      // 
      // TurnirsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(537, 303);
      this.Controls.Add(this.lvTurnirs);
      this.Name = "TurnirsForm";
      this.Text = "TurnirsForm";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lvTurnirs;
  }
}