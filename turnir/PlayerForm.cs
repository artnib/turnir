﻿using System;
using System.Windows.Forms;
using turnir.Properties;

namespace turnir
{
  public partial class PlayerForm : Form
  {
    public PlayerForm()
    {
      InitializeComponent();
    }

    internal PlayerForm(Player player)
      :this()
    {
      this.Player = player;
    }

    internal Player Player
    {
      get { return player; }
      set 
      {
        player = value;
        CheckTitles();
        if (value == null)
        {
          name.Text = String.Empty;
          location.Text = String.Empty;
          cbTitle.SelectedIndex = 0;
        }
        else
        {
          name.Text = value.Name;
          location.Text = value.Location;
          if (value.Grade == null)
            cbTitle.SelectedIndex = 0;
          else
            cbTitle.SelectedItem = value.Grade;
        }
      }
    }

    Player player;

    private void save_Click(object sender, EventArgs e)
    {
      var place = location.Text;
      if (player == null)
      {
        player = new Player {
          Name = name.Text, Location = place, Title=cbTitle.Text };
      }
      else
      {
        player.Name = name.Text;
        player.Location = place;
        player.Title = cbTitle.Text;
      }
      if (cbTitle.SelectedItem is Title)
        player.Grade = (Title)cbTitle.SelectedItem;
      if (place.Length > 0)
      {
        var locations = location.AutoCompleteCustomSource;
        if (!locations.Contains(location.Text))
          locations.Add(place);
      }
      DialogResult = DialogResult.OK; 
    }

    private void PlayerForm_Load(object sender, EventArgs e)
    {
      CheckTitles();
      if(player == null)
        cbTitle.SelectedIndex = 0;
    }

    private void CheckTitles()
    {
      if (cbTitle.Items.Count == 0)
      {
        cbTitle.Items.AddRange(new Titles().GetTitles().ToArray());
      }
    }

    private void name_TextChanged(object sender, EventArgs e)
    {
      save.Enabled = name.Text.Length > 0;
    }

    private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }

    private void cancel_Click(object sender, EventArgs e)
    {
      player = null;
    }
  }
}