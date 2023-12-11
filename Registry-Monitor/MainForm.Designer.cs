using System;
using System.Linq;
using Microsoft.Win32;

namespace Registry_Monitor
{
    partial class MainForm
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
            this.openRegeditButton = new System.Windows.Forms.Button();
            this.startStopTrackingButton = new System.Windows.Forms.Button();
            this.registryPathsLabel = new System.Windows.Forms.Label();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.saveLogCheckBox = new System.Windows.Forms.CheckBox();
            this.addRegistryPathButton = new System.Windows.Forms.Button();
            this.removeAllRegistryPathsButton = new System.Windows.Forms.Button();
            this.registryPathsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            //
            // openRegeditButton
            //
            this.openRegeditButton.Location = new System.Drawing.Point(660, 12);
            this.openRegeditButton.Name = "openRegeditButton";
            this.openRegeditButton.Size = new System.Drawing.Size(122, 40);
            this.openRegeditButton.TabIndex = 3;
            this.openRegeditButton.Text = "Open Regedit";
            this.openRegeditButton.UseVisualStyleBackColor = true;
            this.openRegeditButton.Click += new System.EventHandler(this.openRegeditButton_Click);
            //
            // startStopTrackingButton
            //
            this.startStopTrackingButton.Location = new System.Drawing.Point(660, 58);
            this.startStopTrackingButton.Name = "startStopTrackingButton";
            this.startStopTrackingButton.Size = new System.Drawing.Size(120, 73);
            this.startStopTrackingButton.TabIndex = 4;
            this.startStopTrackingButton.Text = "Start tracking changes";
            this.startStopTrackingButton.UseVisualStyleBackColor = true;
            this.startStopTrackingButton.Click += new System.EventHandler(this.startStopTrackingButton_Click);
            //
            // registryPathsLabel
            //
            this.registryPathsLabel.Location = new System.Drawing.Point(12, 9);
            this.registryPathsLabel.Name = "registryPathsLabel";
            this.registryPathsLabel.Size = new System.Drawing.Size(536, 20);
            this.registryPathsLabel.TabIndex = 12;
            this.registryPathsLabel.Text = "Registry path(s):";
            //
            // logRichTextBox
            //
            this.logRichTextBox.Location = new System.Drawing.Point(12, 173);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(770, 386);
            this.logRichTextBox.TabIndex = 14;
            this.logRichTextBox.Text = "";
            //
            // saveLogCheckBox
            //
            this.saveLogCheckBox.Checked = true;
            this.saveLogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveLogCheckBox.Location = new System.Drawing.Point(660, 137);
            this.saveLogCheckBox.Name = "saveLogCheckBox";
            this.saveLogCheckBox.Size = new System.Drawing.Size(122, 30);
            this.saveLogCheckBox.TabIndex = 19;
            this.saveLogCheckBox.Text = "Save log to a file";
            this.saveLogCheckBox.UseVisualStyleBackColor = true;
            //
            // addRegistryPathButton
            //
            this.addRegistryPathButton.Location = new System.Drawing.Point(554, 12);
            this.addRegistryPathButton.Name = "addRegistryPathButton";
            this.addRegistryPathButton.Size = new System.Drawing.Size(100, 75);
            this.addRegistryPathButton.TabIndex = 20;
            this.addRegistryPathButton.Text = "Add registry path";
            this.addRegistryPathButton.UseVisualStyleBackColor = true;
            this.addRegistryPathButton.Click += new System.EventHandler(this.addRegistryButton_Click);
            //
            // removeAllRegistryPathsButton
            //
            this.removeAllRegistryPathsButton.Location = new System.Drawing.Point(554, 92);
            this.removeAllRegistryPathsButton.Name = "removeAllRegistryPathsButton";
            this.removeAllRegistryPathsButton.Size = new System.Drawing.Size(100, 75);
            this.removeAllRegistryPathsButton.TabIndex = 21;
            this.removeAllRegistryPathsButton.Text = "Remove all registry paths";
            this.removeAllRegistryPathsButton.UseVisualStyleBackColor = true;
            this.removeAllRegistryPathsButton.Click += new System.EventHandler(this.removeAllRegistryPathsButton_Click);
            //
            // registryPathsRichTextBox
            //
            this.registryPathsRichTextBox.Location = new System.Drawing.Point(12, 32);
            this.registryPathsRichTextBox.Name = "registryPathsRichTextBox";
            this.registryPathsRichTextBox.ReadOnly = true;
            this.registryPathsRichTextBox.Size = new System.Drawing.Size(536, 135);
            this.registryPathsRichTextBox.TabIndex = 22;
            this.registryPathsRichTextBox.Text = "";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(794, 571);
            this.Controls.Add(this.registryPathsRichTextBox);
            this.Controls.Add(this.removeAllRegistryPathsButton);
            this.Controls.Add(this.addRegistryPathButton);
            this.Controls.Add(this.saveLogCheckBox);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.registryPathsLabel);
            this.Controls.Add(this.startStopTrackingButton);
            this.Controls.Add(this.openRegeditButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Registry Monitor";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RichTextBox registryPathsRichTextBox;

        private System.Windows.Forms.Button removeAllRegistryPathsButton;

        private System.Windows.Forms.Button addRegistryPathButton;

        private System.Windows.Forms.CheckBox saveLogCheckBox;
        private System.Windows.Forms.Button startStopTrackingButton;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Label registryPathsLabel;
        private System.Windows.Forms.Button openRegeditButton;

        #endregion
    }
}