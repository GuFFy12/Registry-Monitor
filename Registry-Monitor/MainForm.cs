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
        private readonly List<WmiRegistryEventListener> _wmiRegistryEventListeners = new List<WmiRegistryEventListener>();
        private bool _wmiRegistryEventListenersStopped = true;

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
                Log($"{exception.GetType().Name}: {exception.Message}", LogLevel.Warn);
            }
        }

        /**
         * This start / stop tracking changes.
         */
        private void startStopWmiRegistryEventListenersButton_Click(object sender, EventArgs e)
        {
            if (_wmiRegistryEventListenersStopped)
            {
                startStopWmiRegistryEventListenersButton.Text = "Stop wmi registry event listeners";
                addWmiRegistryEventListenerButton.Enabled = false;
                removeAllWmiRegistryEventListenersButton.Enabled = false;

                foreach (var wmiRegistryEventListener in _wmiRegistryEventListeners)
                    try
                    {
                        wmiRegistryEventListener.Start();
                    }
                    catch (Exception exception)
                    {
                        Log(
                            $"$[{wmiRegistryEventListener.RegistryPath.RegistryEvent}] {wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}{(wmiRegistryEventListener.RegistryPath.RegistryEvent == WmiRegistryEventListener.RegistryEvent.RegistryValueChangeEvent ? $" - {wmiRegistryEventListener.RegistryPath.Value}" : string.Empty)} - {exception.GetType().Name}: {exception.Message}",
                            LogLevel.Error);
                    }

                Log("Start tracking changes...");
            }
            else
            {
                startStopWmiRegistryEventListenersButton.Text = "Start wmi registry event listeners";
                addWmiRegistryEventListenerButton.Enabled = true;
                removeAllWmiRegistryEventListenersButton.Enabled = true;

                foreach (var wmiRegistryEventListener in _wmiRegistryEventListeners) wmiRegistryEventListener.Stop();

                Log("Stop tracking changes");
            }

            _wmiRegistryEventListenersStopped = !_wmiRegistryEventListenersStopped;
        }

        /**
         * This method is event triggered from wmiRegistryEventListener.
         */
        private void WmiRegistryWatcherEventArrived(object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            if (!(sender is WmiRegistryEventListener wmiRegistryEventListener)) return;

            var loggerMessage =
                $"[{wmiRegistryEventListener.RegistryPath.RegistryEvent}] {wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}";

            foreach (var prop in eventArrivedEventArgs.NewEvent.Properties) loggerMessage += $" [{prop.Name}:{prop.Value}]";

            if (wmiRegistryEventListener.RegistryPath.RegistryEvent != WmiRegistryEventListener.RegistryEvent.RegistryTreeChangeEvent)
                loggerMessage += $@"[ValueData:{(wmiRegistryEventListener.RegistryPath.RegistryEvent == WmiRegistryEventListener.RegistryEvent.RegistryKeyChangeEvent
                    ? Registry.GetValue($"{wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}", "", "").ToString()
                    : Registry.GetValue($"{wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}", wmiRegistryEventListener.RegistryPath.Value, "").ToString())}]";

            Log(loggerMessage);
        }

        /**
         * Call addRegistryPath form.
         */
        private void addWmiRegistryEventListenerButton_Click(object sender, EventArgs e)
        {
            var addWmiRegistryEventListener = new AddWmiRegistryEventListener();
            addWmiRegistryEventListener.FormClosed += AddWmiRegistryEventListener_FormClosed;
            addWmiRegistryEventListener.ShowDialog();
        }

        /**
         * On addRegistryPath form close, add registry paths params to registryPaths.
         */
        private void AddWmiRegistryEventListener_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!(sender is AddWmiRegistryEventListener addWmiRegistryEventListener) || addWmiRegistryEventListener.WmiRegistryEventListener == null) return;

            startStopWmiRegistryEventListenersButton.Enabled = true;
            removeAllWmiRegistryEventListenersButton.Enabled = true;

            registryWmiEventListenersRichTextBox.AppendText(
                $"[{addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.RegistryEvent}] {addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.Hive}\\{addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.RootPath}{(addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.RegistryEvent == WmiRegistryEventListener.RegistryEvent.RegistryValueChangeEvent ? $" - {addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.Value}" : string.Empty)}");
            registryWmiEventListenersRichTextBox.AppendText(Environment.NewLine);

            addWmiRegistryEventListener.WmiRegistryEventListener.EventArrivedEventHandler += WmiRegistryWatcherEventArrived;
            _wmiRegistryEventListeners.Add(addWmiRegistryEventListener.WmiRegistryEventListener);
        }

        /**
         * Remove registry paths.
         */
        private void removeAllWmiRegistryEventListenersButton_Click(object sender, EventArgs e)
        {
            startStopWmiRegistryEventListenersButton.Enabled = false;
            removeAllWmiRegistryEventListenersButton.Enabled = false;

            registryWmiEventListenersRichTextBox.Clear();

            foreach (var wmiRegistryEventListener in _wmiRegistryEventListeners) wmiRegistryEventListener.Dispose();
            _wmiRegistryEventListeners.Clear();
        }

        /**
         * If we close app and deleteFilesCheckBox.Checked is true, delete all snapshot files.
         */
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (saveLogCheckBox.Checked) File.WriteAllText($"{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fffffff}.log", logRichTextBox.Text);
        }

        #region Log

        public enum LogLevel
        {
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        /**
         * Simple logger that writes it to logRichTextBox. Print time, message type.
         */
        private void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            // Use invoke, because we can call Logger from another classes.
            logRichTextBox.Invoke(new Action(() => logRichTextBox.AppendText($"[{logLevel.ToString().ToUpper()}] {DateTime.Now:HH:mm:ss.fff} - {message}\n")));
        }

        #endregion
    }
}