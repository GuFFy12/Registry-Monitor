using System;
using System.Management;

namespace Registry_Monitor.RegistryUtils
{
    public class WmiRegistryEventListener : IDisposable
    {
        // Enumeration representing various registry events to track
        public enum RegistryEvent
        {
            RegistryKeyChangeEvent,
            RegistryTreeChangeEvent,
            RegistryValueChangeEvent
        }

        // ManagementEventWatcher instance for monitoring WMI events
        private readonly ManagementEventWatcher _managementEventWatcher;

        // Readonly property representing the registry path associated with this listener
        public readonly RegistryPath RegistryPath;

        /**
         * Constructor for WmiRegistryEventListener. Initializes the listener with the specified registry path.
         * Query registry using WMI. Read more: https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher?view=dotnet-plat-ext-8.0
         */
        public WmiRegistryEventListener(RegistryPath registryPath)
        {
            // Set the registry path for this listener
            RegistryPath = registryPath;

            /*
             * Registry query.
             * Read more: https://learn.microsoft.com/en-us/previous-versions/windows/desktop/regprov/system-registry-provider
             * Also, we need to replace "\" with "\\" (specifics of the query).
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

            // Create a new ManagementEventWatcher using the defined WQL query
            _managementEventWatcher = new ManagementEventWatcher(wqlEventQuery);
            // Attach the EventArrived method as an event handler
            _managementEventWatcher.EventArrived += EventArrived;
        }

        // Implementation of the IDisposable interface to allow proper resource cleanup
        public void Dispose()
        {
            _managementEventWatcher?.Dispose();
        }

        // Start monitoring registry events
        public void Start()
        {
            _managementEventWatcher.Start();
        }

        // Stop monitoring registry events
        public void Stop()
        {
            _managementEventWatcher.Stop();
        }

        // Event handler delegate for handling arrived WMI events
        public event EventArrivedEventHandler EventArrivedEventHandler;

        // Method invoked when a WMI event is received
        private void EventArrived(object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            // Invoke the event handler delegate if it is subscribed
            EventArrivedEventHandler?.Invoke(this, eventArrivedEventArgs);
        }
    }
}