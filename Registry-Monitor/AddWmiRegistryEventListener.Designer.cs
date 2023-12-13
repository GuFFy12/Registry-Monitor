using System;
using System.Linq;
using Microsoft.Win32;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    partial class AddWmiRegistryEventListener
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWmiRegistryEventListener));
            this.registryEventTypeComboBox = new System.Windows.Forms.ComboBox();
            this.registryEventTypeLabel = new System.Windows.Forms.Label();
            this.registryPathTextBox = new System.Windows.Forms.TextBox();
            this.registryValueTextBox = new System.Windows.Forms.TextBox();
            this.registryPathLabel = new System.Windows.Forms.Label();
            this.registryValueLabel = new System.Windows.Forms.Label();
            this.addWmiRegistryEventListenerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // registryEventTypeComboBox
            // 
            this.registryEventTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.registryEventTypeComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.registryEventTypeComboBox, "registryEventTypeComboBox");
            this.registryEventTypeComboBox.Name = "registryEventTypeComboBox";
            this.registryEventTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.registryEventTypeComboBox_SelectedIndexChanged);
            // 
            // registryEventTypeLabel
            // 
            resources.ApplyResources(this.registryEventTypeLabel, "registryEventTypeLabel");
            this.registryEventTypeLabel.Name = "registryEventTypeLabel";
            // 
            // registryPathTextBox
            // 
            resources.ApplyResources(this.registryPathTextBox, "registryPathTextBox");
            this.registryPathTextBox.Name = "registryPathTextBox";
            // 
            // registryValueTextBox
            // 
            resources.ApplyResources(this.registryValueTextBox, "registryValueTextBox");
            this.registryValueTextBox.Name = "registryValueTextBox";
            this.registryValueTextBox.ReadOnly = true;
            // 
            // registryPathLabel
            // 
            resources.ApplyResources(this.registryPathLabel, "registryPathLabel");
            this.registryPathLabel.Name = "registryPathLabel";
            // 
            // registryValueLabel
            // 
            resources.ApplyResources(this.registryValueLabel, "registryValueLabel");
            this.registryValueLabel.Name = "registryValueLabel";
            // 
            // addWmiRegistryEventListenerButton
            // 
            resources.ApplyResources(this.addWmiRegistryEventListenerButton, "addWmiRegistryEventListenerButton");
            this.addWmiRegistryEventListenerButton.Name = "addWmiRegistryEventListenerButton";
            this.addWmiRegistryEventListenerButton.UseVisualStyleBackColor = true;
            this.addWmiRegistryEventListenerButton.Click += new System.EventHandler(this.addWmiRegistryEventListenerButton_Click);
            // 
            // AddWmiRegistryEventListener
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.addWmiRegistryEventListenerButton);
            this.Controls.Add(this.registryValueLabel);
            this.Controls.Add(this.registryPathLabel);
            this.Controls.Add(this.registryValueTextBox);
            this.Controls.Add(this.registryPathTextBox);
            this.Controls.Add(this.registryEventTypeLabel);
            this.Controls.Add(this.registryEventTypeComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWmiRegistryEventListener";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox registryEventTypeComboBox;
        private System.Windows.Forms.Label registryEventTypeLabel;
        private System.Windows.Forms.TextBox registryPathTextBox;
        private System.Windows.Forms.TextBox registryValueTextBox;
        private System.Windows.Forms.Label registryPathLabel;
        private System.Windows.Forms.Label registryValueLabel;
        private System.Windows.Forms.Button addWmiRegistryEventListenerButton;

        #endregion
    }
}