using System;
using System.Management;
using Microsoft.Win32;

namespace Registry_Monitor.RegistryUtils
{
    public class WmiRegistryEventListener : IDisposable
    {
        public enum TrackTypes
        {
            RegistryKeyChangeEvent,
            RegistryTreeChangeEvent,
            RegistryValueChangeEvent
        }

        private readonly MainForm.LoggerDelegate _logger;

        private readonly ManagementEventWatcher _watcher;

        public readonly RegistryPath RegistryPath;
        private string _prevValueData;

        /**
         * Query registry using WMI.
         * Read more: https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher?view=dotnet-plat-ext-8.0
         */
        public WmiRegistryEventListener(RegistryPath registryPath, MainForm.LoggerDelegate logger)
        {
            _logger = logger;
            RegistryPath = registryPath;

            /*
             * Registry query.
             * Read more: https://learn.microsoft.com/en-us/previous-versions/windows/desktop/regprov/system-registry-provider
             * Also we need replace "\" to "\\" (specifics of the query).
             */
            string query;
            switch (RegistryPath.TrackType)
            {
                case TrackTypes.RegistryKeyChangeEvent:
                    query = $"SELECT * FROM RegistryKeyChangeEvent WHERE Hive='{RegistryPath.Hive}' AND KeyPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}'";
                    break;
                case TrackTypes.RegistryTreeChangeEvent:
                    _prevValueData = Registry.GetValue($"{RegistryPath.Hive}\\{RegistryPath.RootPath}", "", "").ToString();

                    query =
                        $"SELECT * FROM RegistryTreeChangeEvent WHERE Hive='{RegistryPath.Hive}' AND RootPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}'";
                    break;
                case TrackTypes.RegistryValueChangeEvent:
                    _prevValueData = Registry.GetValue($"{RegistryPath.Hive}\\{RegistryPath.RootPath}", RegistryPath.Value, "").ToString();

                    query =
                        $"SELECT * FROM RegistryValueChangeEvent WHERE Hive='{RegistryPath.Hive}' AND KeyPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}' AND ValueName='{RegistryPath.Value.Replace("\\", "\\\\")}'";
                    break;
                default:
                    _logger($"Unknown track type - {RegistryPath.TrackType}", MainForm.LoggerMessageType.Error);
                    return;
            }

            try
            {
                _watcher = new ManagementEventWatcher(query);
                _watcher.EventArrived += EventArrived;
                _watcher.Start();
            }
            catch (Exception exception)
            {
                _logger(
                    $"[{RegistryPath.TrackType}] {RegistryPath.Hive}\\{RegistryPath.RootPath}{(RegistryPath.TrackType == TrackTypes.RegistryValueChangeEvent ? $" - {RegistryPath.Value}" : string.Empty)} - {exception.Message}",
                    MainForm.LoggerMessageType.Error);
            }
        }

        public void Dispose()
        {
            _watcher?.Dispose();
        }

        private void EventArrived(object wmiRegistryEventSender, EventArrivedEventArgs wmiRegistryEventArrivedEventArgs)
        {
            var loggerMessage = $"[{RegistryPath.TrackType}] {RegistryPath.Hive}\\{RegistryPath.RootPath}";

            foreach (var prop in wmiRegistryEventArrivedEventArgs.NewEvent.Properties) loggerMessage += $" [{prop.Name}:{prop.Value}]";

            if (RegistryPath.TrackType != TrackTypes.RegistryTreeChangeEvent)
            {
                var valueData = RegistryPath.TrackType == TrackTypes.RegistryKeyChangeEvent
                    ? Registry.GetValue($"{RegistryPath.Hive}\\{RegistryPath.RootPath}", "", "").ToString()
                    : Registry.GetValue($"{RegistryPath.Hive}\\{RegistryPath.RootPath}", RegistryPath.Value, "").ToString();

                if (valueData != _prevValueData)
                {
                    loggerMessage += $" - '{_prevValueData}'->'{valueData}'";
                    _prevValueData = valueData;
                }
            }

            _logger(loggerMessage);
        }
    }
}