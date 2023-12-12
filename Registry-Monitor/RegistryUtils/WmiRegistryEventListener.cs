using System;
using System.Management;

namespace Registry_Monitor.RegistryUtils
{
    public class WmiRegistryEventListener : IDisposable
    {
        public enum RegistryEvent
        {
            RegistryKeyChangeEvent,
            RegistryTreeChangeEvent,
            RegistryValueChangeEvent
        }

        private readonly ManagementEventWatcher _watcher;

        public readonly RegistryPath RegistryPath;

        /**
         * Query registry using WMI.
         * Read more: https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher?view=dotnet-plat-ext-8.0
         */
        public WmiRegistryEventListener(RegistryPath registryPath)
        {
            RegistryPath = registryPath;

            /*
             * Registry query.
             * Read more: https://learn.microsoft.com/en-us/previous-versions/windows/desktop/regprov/system-registry-provider
             * Also we need replace "\" to "\\" (specifics of the query).
             */
            WqlEventQuery wqlEventQuery;
            switch (RegistryPath.RegistryEvent)
            {
                case RegistryEvent.RegistryKeyChangeEvent:
                    wqlEventQuery = new WqlEventQuery(
                        $"SELECT * FROM RegistryKeyChangeEvent WHERE Hive='{RegistryPath.Hive}' AND KeyPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}'");
                    break;
                case RegistryEvent.RegistryTreeChangeEvent:
                    wqlEventQuery = new WqlEventQuery(
                        $"SELECT * FROM RegistryTreeChangeEvent WHERE Hive='{RegistryPath.Hive}' AND RootPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}'");
                    break;
                case RegistryEvent.RegistryValueChangeEvent:
                    wqlEventQuery = new WqlEventQuery(
                        $"SELECT * FROM RegistryValueChangeEvent WHERE Hive='{RegistryPath.Hive}' AND KeyPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}' AND ValueName='{RegistryPath.Value.Replace("\\", "\\\\")}'");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(RegistryEvent), RegistryPath.RegistryEvent,
                        $"The registry track type '{RegistryPath.RegistryEvent}' is not handled");
            }

            _watcher = new ManagementEventWatcher(wqlEventQuery);
            _watcher.EventArrived += EventArrived;
            _watcher.Start();
        }

        public void Dispose()
        {
            _watcher?.Dispose();
        }

        public event EventArrivedEventHandler EventArrivedEventHandler;

        private void EventArrived(object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            EventArrivedEventHandler?.Invoke(this, eventArrivedEventArgs);
        }
    }
}