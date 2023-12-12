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
            this.startStopWmiRegistryEventListenersButton = new System.Windows.Forms.Button();
            this.registryWmiEventListenersLabel = new System.Windows.Forms.Label();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.saveLogCheckBox = new System.Windows.Forms.CheckBox();
            this.addWmiRegistryEventListenerButton = new System.Windows.Forms.Button();
            this.removeAllWmiRegistryEventListenersButton = new System.Windows.Forms.Button();
            this.registryWmiEventListenersRichTextBox = new System.Windows.Forms.RichTextBox();
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
            // startStopWmiRegistryEventListenersButton
            //
            this.startStopWmiRegistryEventListenersButton.Enabled = false;
            this.startStopWmiRegistryEventListenersButton.Location = new System.Drawing.Point(660, 58);
            this.startStopWmiRegistryEventListenersButton.Name = "startStopWmiRegistryEventListenersButton";
            this.startStopWmiRegistryEventListenersButton.Size = new System.Drawing.Size(122, 63);
            this.startStopWmiRegistryEventListenersButton.TabIndex = 4;
            this.startStopWmiRegistryEventListenersButton.Text = "Start wmi registry event listeners";
            this.startStopWmiRegistryEventListenersButton.UseVisualStyleBackColor = true;
            this.startStopWmiRegistryEventListenersButton.Click += new System.EventHandler(this.startStopWmiRegistryEventListenersButton_Click);
            //
            // registryWmiEventListenersLabel
            //
            this.registryWmiEventListenersLabel.Location = new System.Drawing.Point(12, 9);
            this.registryWmiEventListenersLabel.Name = "registryWmiEventListenersLabel";
            this.registryWmiEventListenersLabel.Size = new System.Drawing.Size(536, 20);
            this.registryWmiEventListenersLabel.TabIndex = 12;
            this.registryWmiEventListenersLabel.Text = "Registry wmi registry event listeners:";
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
            this.saveLogCheckBox.Location = new System.Drawing.Point(660, 127);
            this.saveLogCheckBox.Name = "saveLogCheckBox";
            this.saveLogCheckBox.Size = new System.Drawing.Size(122, 40);
            this.saveLogCheckBox.TabIndex = 19;
            this.saveLogCheckBox.Text = "Save log to a file";
            this.saveLogCheckBox.UseVisualStyleBackColor = true;
            //
            // addWmiRegistryEventListenerButton
            //
            this.addWmiRegistryEventListenerButton.Location = new System.Drawing.Point(554, 12);
            this.addWmiRegistryEventListenerButton.Name = "addWmiRegistryEventListenerButton";
            this.addWmiRegistryEventListenerButton.Size = new System.Drawing.Size(100, 75);
            this.addWmiRegistryEventListenerButton.TabIndex = 20;
            this.addWmiRegistryEventListenerButton.Text = "Add wmi registry event listener";
            this.addWmiRegistryEventListenerButton.UseVisualStyleBackColor = true;
            this.addWmiRegistryEventListenerButton.Click += new System.EventHandler(this.addWmiRegistryEventListenerButton_Click);
            //
            // removeAllWmiRegistryEventListenersButton
            //
            this.removeAllWmiRegistryEventListenersButton.Enabled = false;
            this.removeAllWmiRegistryEventListenersButton.Location = new System.Drawing.Point(554, 92);
            this.removeAllWmiRegistryEventListenersButton.Name = "removeAllWmiRegistryEventListenersButton";
            this.removeAllWmiRegistryEventListenersButton.Size = new System.Drawing.Size(100, 75);
            this.removeAllWmiRegistryEventListenersButton.TabIndex = 21;
            this.removeAllWmiRegistryEventListenersButton.Text = "Remove all wmi registry event listeners";
            this.removeAllWmiRegistryEventListenersButton.UseVisualStyleBackColor = true;
            this.removeAllWmiRegistryEventListenersButton.Click += new System.EventHandler(this.removeAllWmiRegistryEventListenersButton_Click);
            //
            // registryWmiEventListenersRichTextBox
            //
            this.registryWmiEventListenersRichTextBox.Location = new System.Drawing.Point(12, 32);
            this.registryWmiEventListenersRichTextBox.Name = "registryWmiEventListenersRichTextBox";
            this.registryWmiEventListenersRichTextBox.ReadOnly = true;
            this.registryWmiEventListenersRichTextBox.Size = new System.Drawing.Size(536, 135);
            this.registryWmiEventListenersRichTextBox.TabIndex = 22;
            this.registryWmiEventListenersRichTextBox.Text = "";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(794, 571);
            this.Controls.Add(this.registryWmiEventListenersRichTextBox);
            this.Controls.Add(this.removeAllWmiRegistryEventListenersButton);
            this.Controls.Add(this.addWmiRegistryEventListenerButton);
            this.Controls.Add(this.saveLogCheckBox);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.registryWmiEventListenersLabel);
            this.Controls.Add(this.startStopWmiRegistryEventListenersButton);
            this.Controls.Add(this.openRegeditButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Registry Monitor";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RichTextBox registryWmiEventListenersRichTextBox;

        private System.Windows.Forms.Button removeAllWmiRegistryEventListenersButton;

        private System.Windows.Forms.Button addWmiRegistryEventListenerButton;

        private System.Windows.Forms.CheckBox saveLogCheckBox;
        private System.Windows.Forms.Button startStopWmiRegistryEventListenersButton;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.Label registryWmiEventListenersLabel;
        private System.Windows.Forms.Button openRegeditButton;

        #endregion
    }
}