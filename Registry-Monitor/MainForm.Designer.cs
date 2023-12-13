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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            resources.ApplyResources(this.openRegeditButton, "openRegeditButton");
            this.openRegeditButton.Name = "openRegeditButton";
            this.openRegeditButton.UseVisualStyleBackColor = true;
            this.openRegeditButton.Click += new System.EventHandler(this.openRegeditButton_Click);
            //
            // startStopWmiRegistryEventListenersButton
            //
            resources.ApplyResources(this.startStopWmiRegistryEventListenersButton, "startStopWmiRegistryEventListenersButton");
            this.startStopWmiRegistryEventListenersButton.Name = "startStopWmiRegistryEventListenersButton";
            this.startStopWmiRegistryEventListenersButton.UseVisualStyleBackColor = true;
            this.startStopWmiRegistryEventListenersButton.Click += new System.EventHandler(this.startStopWmiRegistryEventListenersButton_Click);
            //
            // registryWmiEventListenersLabel
            //
            resources.ApplyResources(this.registryWmiEventListenersLabel, "registryWmiEventListenersLabel");
            this.registryWmiEventListenersLabel.Name = "registryWmiEventListenersLabel";
            //
            // logRichTextBox
            //
            resources.ApplyResources(this.logRichTextBox, "logRichTextBox");
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            //
            // saveLogCheckBox
            //
            this.saveLogCheckBox.Checked = true;
            this.saveLogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.saveLogCheckBox, "saveLogCheckBox");
            this.saveLogCheckBox.Name = "saveLogCheckBox";
            this.saveLogCheckBox.UseVisualStyleBackColor = true;
            //
            // addWmiRegistryEventListenerButton
            //
            resources.ApplyResources(this.addWmiRegistryEventListenerButton, "addWmiRegistryEventListenerButton");
            this.addWmiRegistryEventListenerButton.Name = "addWmiRegistryEventListenerButton";
            this.addWmiRegistryEventListenerButton.UseVisualStyleBackColor = true;
            this.addWmiRegistryEventListenerButton.Click += new System.EventHandler(this.addWmiRegistryEventListenerButton_Click);
            //
            // removeAllWmiRegistryEventListenersButton
            //
            resources.ApplyResources(this.removeAllWmiRegistryEventListenersButton, "removeAllWmiRegistryEventListenersButton");
            this.removeAllWmiRegistryEventListenersButton.Name = "removeAllWmiRegistryEventListenersButton";
            this.removeAllWmiRegistryEventListenersButton.UseVisualStyleBackColor = true;
            this.removeAllWmiRegistryEventListenersButton.Click += new System.EventHandler(this.removeAllWmiRegistryEventListenersButton_Click);
            //
            // registryWmiEventListenersRichTextBox
            //
            resources.ApplyResources(this.registryWmiEventListenersRichTextBox, "registryWmiEventListenersRichTextBox");
            this.registryWmiEventListenersRichTextBox.Name = "registryWmiEventListenersRichTextBox";
            this.registryWmiEventListenersRichTextBox.ReadOnly = true;
            //
            // MainForm
            //
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.registryWmiEventListenersRichTextBox);
            this.Controls.Add(this.removeAllWmiRegistryEventListenersButton);
            this.Controls.Add(this.addWmiRegistryEventListenerButton);
            this.Controls.Add(this.saveLogCheckBox);
            this.Controls.Add(this.logRichTextBox);
            this.Controls.Add(this.registryWmiEventListenersLabel);
            this.Controls.Add(this.startStopWmiRegistryEventListenersButton);
            this.Controls.Add(this.openRegeditButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
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