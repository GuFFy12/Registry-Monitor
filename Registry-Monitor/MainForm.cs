using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;
using Microsoft.Win32;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    public partial class MainForm : Form
    {
        private readonly List<RegistryPath> _registryPaths = new List<RegistryPath>();
        private readonly List<WmiRegistryEventListener> _wmiRegistryEventListeners = new List<WmiRegistryEventListener>();

        public MainForm()
        {
            InitializeComponent();
        }

        /**
         * Run regedit.
         */
        private void openRegeditButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("regedit.exe");
            }
            catch (Exception exception)
            {
                Logger(exception.Message, LoggerMessageType.Error);
            }
        }

        /**
         * This start / stop tracking changes.
         */
        private void startStopTrackingButton_Click(object sender, EventArgs e)
        {
            if (_wmiRegistryEventListeners.Count == 0)
            {
                startStopTrackingButton.Text = "Stop tracking changes";
                addRegistryPathButton.Enabled = false;
                removeAllRegistryPathsButton.Enabled = false;

                foreach (var registryPath in _registryPaths)
                    try
                    {
                        _wmiRegistryEventListeners.Add(new WmiRegistryEventListener(registryPath, WmiRegistryWatcherEventArrived));
                    }
                    catch (Exception exception)
                    {
                        Logger(
                            $"[{registryPath.TrackType}] {registryPath.Hive}\\{registryPath.RootPath}{(registryPath.TrackType == WmiRegistryEventListener.TrackTypes.RegistryValueChangeEvent ? $" - {registryPath.Value}" : string.Empty)} - {exception.Message}",
                            LoggerMessageType.Error);
                    }

                Logger("Start tracking changes...");
            }
            else
            {
                startStopTrackingButton.Text = "Start tracking changes";
                addRegistryPathButton.Enabled = true;
                removeAllRegistryPathsButton.Enabled = true;

                foreach (var wmiRegistryEventListener in _wmiRegistryEventListeners) wmiRegistryEventListener.Dispose();
                _wmiRegistryEventListeners.Clear();

                Logger("Stop tracking changes");
            }
        }

        private void WmiRegistryWatcherEventArrived(object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            if (!(sender is WmiRegistryEventListener wmiRegistryEventListener)) return;

            var loggerMessage =
                $"[{wmiRegistryEventListener.RegistryPath.TrackType}] {wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}";

            foreach (var prop in eventArrivedEventArgs.NewEvent.Properties) loggerMessage += $" [{prop.Name}:{prop.Value}]";

            if (wmiRegistryEventListener.RegistryPath.TrackType != WmiRegistryEventListener.TrackTypes.RegistryTreeChangeEvent)
                loggerMessage += $@"[ValueData:{(wmiRegistryEventListener.RegistryPath.TrackType == WmiRegistryEventListener.TrackTypes.RegistryKeyChangeEvent
                    ? Registry.GetValue($"{wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}", "", "").ToString()
                    : Registry.GetValue($"{wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}", wmiRegistryEventListener.RegistryPath.Value, "").ToString())}]";

            Logger(loggerMessage);
        }

        /**
         * Call addRegistryPath form.
         */
        private void addRegistryButton_Click(object sender, EventArgs e)
        {
            var addRegistryPath = new AddRegistryPath();
            addRegistryPath.FormClosed += AddRegistryPath_FormClosed;
            addRegistryPath.ShowDialog();
        }

        /**
         * On addRegistryPath form close, add registry paths params to registryPaths.
         */
        private void AddRegistryPath_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!(sender is AddRegistryPath addRegistryPath) || addRegistryPath.RegistryPath == null) return;

            registryPathsRichTextBox.AppendText(
                $"[{addRegistryPath.RegistryPath.TrackType}] {addRegistryPath.RegistryPath.Hive}\\{addRegistryPath.RegistryPath.RootPath}{(addRegistryPath.RegistryPath.TrackType == WmiRegistryEventListener.TrackTypes.RegistryValueChangeEvent ? $" - {addRegistryPath.RegistryPath.Value}" : string.Empty)}");
            registryPathsRichTextBox.AppendText(Environment.NewLine);

            _registryPaths.Add(addRegistryPath.RegistryPath);
        }

        /**
         * Remove registry paths.
         */
        private void removeAllRegistryPathsButton_Click(object sender, EventArgs e)
        {
            registryPathsRichTextBox.Clear();
            _registryPaths.Clear();
        }

        /**
         * If we close app and deleteFilesCheckBox.Checked is true, delete all snapshot files.
         */
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (saveLogCheckBox.Checked) File.WriteAllText($"{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fffffff}.log", logRichTextBox.Text);
        }

        /**
         * Simple logger that writes it to logRichTextBox. Print time, message type.
         */

        #region Logger

        public enum LoggerMessageType
        {
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        private void Logger(string message, LoggerMessageType loggerMessageType = LoggerMessageType.Info)
        {
            // Use invoke, because we can call Logger from another classes.
            logRichTextBox.Invoke(new Action(() => logRichTextBox.AppendText($"[{loggerMessageType.ToString().ToUpper()}] {DateTime.Now:HH:mm:ss.fff} - {message}\n")));
        }

        #endregion
    }
}