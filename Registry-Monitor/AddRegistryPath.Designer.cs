using System;
using System.Linq;
using Microsoft.Win32;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    partial class AddRegistryPath
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
            this.registryEventComboBox = new System.Windows.Forms.ComboBox();
            this.registryEventLabel = new System.Windows.Forms.Label();
            this.registryPathTextBox = new System.Windows.Forms.TextBox();
            this.registryValueTextBox = new System.Windows.Forms.TextBox();
            this.registryPathLabel = new System.Windows.Forms.Label();
            this.registryValueLabel = new System.Windows.Forms.Label();
            this.addRegistryPathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // registryEventComboBox
            //
            this.registryEventComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.registryEventComboBox.FormattingEnabled = true;
            this.registryEventComboBox.Location = new System.Drawing.Point(12, 32);
            this.registryEventComboBox.Name = "registryEventComboBox";
            this.registryEventComboBox.Size = new System.Drawing.Size(370, 21);
            this.registryEventComboBox.TabIndex = 0;
            this.registryEventComboBox.SelectedIndexChanged += new System.EventHandler(this.trackTypeComboBox_SelectedIndexChanged);
            //
            // registryEventLabel
            //
            this.registryEventLabel.Location = new System.Drawing.Point(12, 9);
            this.registryEventLabel.Name = "registryEventLabel";
            this.registryEventLabel.Size = new System.Drawing.Size(370, 20);
            this.registryEventLabel.TabIndex = 1;
            this.registryEventLabel.Text = "Registry event:";
            //
            // registryPathTextBox
            //
            this.registryPathTextBox.Location = new System.Drawing.Point(12, 79);
            this.registryPathTextBox.Name = "registryPathTextBox";
            this.registryPathTextBox.Size = new System.Drawing.Size(370, 20);
            this.registryPathTextBox.TabIndex = 2;
            //
            // registryValueTextBox
            //
            this.registryValueTextBox.Location = new System.Drawing.Point(12, 125);
            this.registryValueTextBox.Name = "registryValueTextBox";
            this.registryValueTextBox.ReadOnly = true;
            this.registryValueTextBox.Size = new System.Drawing.Size(370, 20);
            this.registryValueTextBox.TabIndex = 3;
            //
            // registryPathLabel
            //
            this.registryPathLabel.Location = new System.Drawing.Point(12, 56);
            this.registryPathLabel.Name = "registryPathLabel";
            this.registryPathLabel.Size = new System.Drawing.Size(370, 20);
            this.registryPathLabel.TabIndex = 4;
            this.registryPathLabel.Text = "Registry path:";
            //
            // registryValueLabel
            //
            this.registryValueLabel.Location = new System.Drawing.Point(12, 102);
            this.registryValueLabel.Name = "registryValueLabel";
            this.registryValueLabel.Size = new System.Drawing.Size(370, 20);
            this.registryValueLabel.TabIndex = 5;
            this.registryValueLabel.Text = "Registry value:";
            //
            // addRegistryPathButton
            //
            this.addRegistryPathButton.Location = new System.Drawing.Point(12, 159);
            this.addRegistryPathButton.Name = "addRegistryPathButton";
            this.addRegistryPathButton.Size = new System.Drawing.Size(370, 40);
            this.addRegistryPathButton.TabIndex = 6;
            this.addRegistryPathButton.Text = "Add registry path";
            this.addRegistryPathButton.UseVisualStyleBackColor = true;
            this.addRegistryPathButton.Click += new System.EventHandler(this.addRegistryPathButton_Click);
            //
            // AddRegistryPath
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(394, 211);
            this.Controls.Add(this.addRegistryPathButton);
            this.Controls.Add(this.registryValueLabel);
            this.Controls.Add(this.registryPathLabel);
            this.Controls.Add(this.registryValueTextBox);
            this.Controls.Add(this.registryPathTextBox);
            this.Controls.Add(this.registryEventLabel);
            this.Controls.Add(this.registryEventComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRegistryPath";
            this.Text = "Registry Monitror: Add registry path";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox registryEventComboBox;
        private System.Windows.Forms.Label registryEventLabel;
        private System.Windows.Forms.TextBox registryPathTextBox;
        private System.Windows.Forms.TextBox registryValueTextBox;
        private System.Windows.Forms.Label registryPathLabel;
        private System.Windows.Forms.Label registryValueLabel;
        private System.Windows.Forms.Button addRegistryPathButton;

        #endregion
    }
}