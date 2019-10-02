﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperGrate.Controls
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.supergrate;
            RefreshSettings();
        }
        private void RefreshSettings()
        {
            settingsList.Items.Clear();
            settingsList.Groups.Clear();
            ListViewGroup lastGroup = null;
            foreach (KeyValuePair<string, string> setting in Config.Settings)
            {
                if (setting.Key.Contains("XComment"))
                {
                    lastGroup = new ListViewGroup(setting.Value);
                    settingsList.Groups.Add(lastGroup);
                }
                else
                {
                    ListViewItem item = settingsList.Items.Add(setting.Key);
                    item.SubItems.Add(setting.Value);
                    if (lastGroup != null) item.Group = lastGroup;
                }
            }
            settingsList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        private async void BtnCancel_Click(object sender, EventArgs e)
        {
            Config.LoadConfig();
            RefreshSettings();
            string text = btnRevert.Text;
            btnRevert.Text = "Loaded!";
            await Task.Delay(1000);
            btnRevert.Text = text;
        }
        private void SettingsList_DoubleClick(object sender, EventArgs e)
        {
            if (settingsList.SelectedItems.Count != 0)
            {
                new ChangeSetting(settingsList.SelectedItems[0].Text).ShowDialog();
                RefreshSettings();
            }
        }
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            Config.SaveConfig();
            string text = btnSave.Text;
            btnSave.Text = "Saved!";
            await Task.Delay(1000);
            btnSave.Text = text;
        }
        private void BtnApply_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}