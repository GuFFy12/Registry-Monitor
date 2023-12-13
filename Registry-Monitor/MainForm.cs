using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;
using Microsoft.Win32;
using Registry_Monitor.Properties;
using Registry_Monitor.RegistryUtils;

namespace Registry_Monitor
{
    public partial class MainForm : Form
    {
        // List to store instances of WmiRegistryEventListener for tracking registry events
        private readonly List<WmiRegistryEventListener> _wmiRegistryEventListeners = new List<WmiRegistryEventListener>();

        // Flag to indicate whether WmiRegistryEventListeners are currently stopped or running
        private bool _wmiRegistryEventListenersStopped = true;

        // Constructor for the MainForm
        public MainForm()
        {
            InitializeComponent();
        }

        /**
         * Open the Windows Registry Editor (regedit).
         */
        private void openRegeditButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Attempt to start the Registry Editor
                Process.Start("regedit.exe");
            }
            catch (Exception exception)
            {
                // Log any exceptions that occur during the attempt to start the Registry Editor
                Log($"{exception.GetType().Name}: {exception.Message}", LogLevel.Warn);
            }
        }

        /**
         * Start or stop tracking registry changes.
         */
        private void startStopWmiRegistryEventListenersButton_Click(object sender, EventArgs e)
        {
            // Toggle the state of WmiRegistryEventListeners and update UI accordingly
            if (_wmiRegistryEventListenersStopped)
            {
                addWmiRegistryEventListenerButton.Enabled = false;
                removeAllWmiRegistryEventListenersButton.Enabled = false;

                // Start each WmiRegistryEventListener and log any exceptions
                foreach (var wmiRegistryEventListener in _wmiRegistryEventListeners)
                    try
                    {
                        wmiRegistryEventListener.Start();
                    }
                    catch (Exception exception)
                    {
                        Log(
                            $"[{wmiRegistryEventListener.RegistryPath.RegistryEvent}] {wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}{(wmiRegistryEventListener.RegistryPath.RegistryEvent == WmiRegistryEventListener.RegistryEvent.RegistryValueChangeEvent ? $" - {wmiRegistryEventListener.RegistryPath.Value}" : string.Empty)} - {exception.GetType().Name}: {exception.Message}",
                            LogLevel.Error
                        );
                    }

                Log(Registry_Monitor.Log.MainForm_startStopWmiRegistryEventListenersButton_Click_Start_tracking_changes___);
            }
            else
            {
                addWmiRegistryEventListenerButton.Enabled = true;
                removeAllWmiRegistryEventListenersButton.Enabled = true;

                // Stop each WmiRegistryEventListener
                foreach (var wmiRegistryEventListener in _wmiRegistryEventListeners) wmiRegistryEventListener.Stop();

                Log(Registry_Monitor.Log.MainForm_startStopWmiRegistryEventListenersButton_Click_Stop_tracking_changes);
            }

            _wmiRegistryEventListenersStopped = !_wmiRegistryEventListenersStopped;
        }

        /**
         * Event handler for WmiRegistryEventListener events.
         */
        private void WmiRegistryWatcherEventArrived(object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            // Check if the sender is a WmiRegistryEventListener
            if (!(sender is WmiRegistryEventListener wmiRegistryEventListener)) return;

            // Create a log message based on the received registry event
            var loggerMessage =
                $"[{wmiRegistryEventListener.RegistryPath.RegistryEvent}] {wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}";

            // Append properties of the event to the log message
            foreach (var prop in eventArrivedEventArgs.NewEvent.Properties) loggerMessage += $" [{prop.Name}:{prop.Value}]";

            // Append registry value data if applicable
            if (wmiRegistryEventListener.RegistryPath.RegistryEvent != WmiRegistryEventListener.RegistryEvent.RegistryTreeChangeEvent)
                loggerMessage += $@"[ValueData:{(wmiRegistryEventListener.RegistryPath.RegistryEvent == WmiRegistryEventListener.RegistryEvent.RegistryKeyChangeEvent
                    ? Registry.GetValue($"{wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}", "", "").ToString()
                    : Registry.GetValue($"{wmiRegistryEventListener.RegistryPath.Hive}\\{wmiRegistryEventListener.RegistryPath.RootPath}", wmiRegistryEventListener.RegistryPath.Value, "").ToString())}]";

            // Log the final message
            Log(loggerMessage);
        }

        /**
         * Open the form to add a new WmiRegistryEventListener.
         */
        private void addWmiRegistryEventListenerButton_Click(object sender, EventArgs e)
        {
            // Create and show the AddWmiRegistryEventListener form
            var addWmiRegistryEventListener = new AddWmiRegistryEventListener();
            addWmiRegistryEventListener.FormClosed += AddWmiRegistryEventListener_FormClosed;
            addWmiRegistryEventListener.ShowDialog();
        }

        /**
         * Event handler for the AddWmiRegistryEventListener form close event.
         */
        private void AddWmiRegistryEventListener_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Check if the sender is the AddWmiRegistryEventListener form and if a valid WmiRegistryEventListener is obtained
            if (!(sender is AddWmiRegistryEventListener addWmiRegistryEventListener) || addWmiRegistryEventListener.WmiRegistryEventListener == null) return;

            // Enable buttons and update the UI with the added registry path
            startStopWmiRegistryEventListenersButton.Enabled = true;
            removeAllWmiRegistryEventListenersButton.Enabled = true;

            registryWmiEventListenersRichTextBox.AppendText(
                $"[{addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.RegistryEvent}] {addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.Hive}\\{addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.RootPath}{(addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.RegistryEvent == WmiRegistryEventListener.RegistryEvent.RegistryValueChangeEvent ? $" - {addWmiRegistryEventListener.WmiRegistryEventListener.RegistryPath.Value}" : string.Empty)}"
            );
            registryWmiEventListenersRichTextBox.AppendText(Environment.NewLine);

            // Attach the event handler and add the WmiRegistryEventListener to the list
            addWmiRegistryEventListener.WmiRegistryEventListener.EventArrivedEventHandler += WmiRegistryWatcherEventArrived;
            _wmiRegistryEventListeners.Add(addWmiRegistryEventListener.WmiRegistryEventListener);
        }

        /**
         * Remove all registered WmiRegistryEventListeners.
         */
        private void removeAllWmiRegistryEventListenersButton_Click(object sender, EventArgs e)
        {
            // Disable buttons and clear the UI log
            startStopWmiRegistryEventListenersButton.Enabled = false;
            removeAllWmiRegistryEventListenersButton.Enabled = false;
            registryWmiEventListenersRichTextBox.Clear();

            // Dispose each WmiRegistryEventListener and clear the list
            foreach (var wmiRegistryEventListener in _wmiRegistryEventListeners) wmiRegistryEventListener.Dispose();
            _wmiRegistryEventListeners.Clear();
        }

        /**
         * Handle actions before closing the application.
         */
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Don't save the log to a file if the corresponding checkbox is not checked
            if (!saveLogCheckBox.Checked) base.OnFormClosing(e);

            try
            {
                // Attempt to write to the file
                File.WriteAllText($"{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fffffff}.log", logRichTextBox.Text);
            }
            catch (Exception exception)
            {
                if (MessageBox.Show(Resources.MainForm_OnFormClosing_Exit_without_saving_log_file_, exception.Message, MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
                    DialogResult.Yes) e.Cancel = true;

                // Log any exceptions that occur during the attempt to write to the file
                Log($"{exception.GetType().Name}: {exception.Message}", LogLevel.Warn);

                base.OnFormClosing(e);
            }
        }

        #region Log

        /**
         * Enumeration for different log levels.
         */
        public enum LogLevel
        {
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        /**
         * Simple logger that writes messages to logRichTextBox. Prints timestamp, message type.
         */
        private void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            // Use invoke to safely call Logger from other classes
            logRichTextBox.Invoke(new Action(() => logRichTextBox.AppendText($"[{logLevel.ToString().ToUpper()}] {DateTime.Now:HH:mm:ss.fff} - {message}\n")));
        }

        #endregion
    }
}