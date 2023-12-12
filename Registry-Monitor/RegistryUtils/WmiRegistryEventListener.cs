using System;
using System.Management;

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

        private readonly ManagementEventWatcher _watcher;

        public readonly RegistryPath RegistryPath;
        private readonly EventArrivedEventHandler _eventArrivedEventHandler;

        /**
         * Query registry using WMI.
         * Read more: https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher?view=dotnet-plat-ext-8.0
         */
        public WmiRegistryEventListener(RegistryPath registryPath, EventArrivedEventHandler eventArrivedEventHandler)
        {
            RegistryPath = registryPath;
            _eventArrivedEventHandler = eventArrivedEventHandler;

            /*
             * Registry query.
             * Read more: https://learn.microsoft.com/en-us/previous-versions/windows/desktop/regprov/system-registry-provider
             * Also we need replace "\" to "\\" (specifics of the query).
             */
            WqlEventQuery wqlEventQuery;
            switch (RegistryPath.TrackType)
            {
                case TrackTypes.RegistryKeyChangeEvent:
                    wqlEventQuery = new WqlEventQuery(
                        $"SELECT * FROM RegistryKeyChangeEvent WHERE Hive='{RegistryPath.Hive}' AND KeyPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}'");
                    break;
                case TrackTypes.RegistryTreeChangeEvent:
                    wqlEventQuery = new WqlEventQuery(
                        $"SELECT * FROM RegistryTreeChangeEvent WHERE Hive='{RegistryPath.Hive}' AND RootPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}'");
                    break;
                case TrackTypes.RegistryValueChangeEvent:
                    wqlEventQuery = new WqlEventQuery(
                        $"SELECT * FROM RegistryValueChangeEvent WHERE Hive='{RegistryPath.Hive}' AND KeyPath='{RegistryPath.RootPath.Replace("\\", "\\\\")}' AND ValueName='{RegistryPath.Value.Replace("\\", "\\\\")}'");
                    break;
                default:
                    throw new Exception($"Unknown track type - {RegistryPath.TrackType}");
            }

            _watcher = new ManagementEventWatcher(wqlEventQuery);
            _watcher.EventArrived += EventArrived;
            _watcher.Start();
        }

        public void Dispose()
        {
            _watcher?.Dispose();
        }

        private void EventArrived(object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            _eventArrivedEventHandler?.Invoke(this, eventArrivedEventArgs);
        }
    }
}