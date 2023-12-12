using System;
using System.Security.Principal;

namespace Registry_Monitor.RegistryUtils
{
    public class RegistryPath
    {
        // Enumeration representing various registry hives
        public enum RegistryHive
        {
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CURRENT_CONFIG
        }

        // Public readonly properties for registry hive, registry event, root path, and registry value
        public readonly RegistryHive Hive;
        public readonly WmiRegistryEventListener.RegistryEvent RegistryEvent;
        public readonly string RootPath;
        public readonly string Value;

        /**
         * Constructor for the RegistryPath class. Validates that registryPathText is a valid registry path.
         */
        public RegistryPath(WmiRegistryEventListener.RegistryEvent registryEvent, string pathText, string valueText)
        {
            // Find the index of the first backslash in the pathText
            var firstSlashIndex = pathText.IndexOf('\\');
            // If no backslash is found, throw a FormatException
            if (firstSlashIndex == -1) throw new FormatException("Registry hive not found");

            // Extract the registry hive text from the beginning of the pathText
            var registryHiveText = pathText.Substring(0, firstSlashIndex);
            // Try to parse the registry hive text into the RegistryHive enumeration
            if (!Enum.TryParse(registryHiveText, true, out RegistryHive registryHive))
                throw new ArgumentException($"{registryHiveText} is not a valid registry hive", nameof(registryHiveText));

            // Set properties based on the parsed registry hive and input parameters
            RegistryEvent = registryEvent;
            Value = valueText;

            /*
             * Resolve the problem where WMI can only access HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CURRENT_CONFIG.
             * Other registry hives are accessed using these root hives.
             */
            switch (registryHive)
            {
                case RegistryHive.HKEY_CLASSES_ROOT:
                    Hive = RegistryHive.HKEY_LOCAL_MACHINE;
                    RootPath = $"SOFTWARE\\Classes\\{pathText.Substring(firstSlashIndex + 1)}";
                    break;
                case RegistryHive.HKEY_CURRENT_USER:
                    Hive = RegistryHive.HKEY_USERS;
                    RootPath = $"{WindowsIdentity.GetCurrent().User?.Value}\\{pathText.Substring(firstSlashIndex + 1)}";
                    break;
                case RegistryHive.HKEY_LOCAL_MACHINE:
                case RegistryHive.HKEY_USERS:
                case RegistryHive.HKEY_CURRENT_CONFIG:
                    Hive = registryHive;
                    RootPath = pathText.Substring(firstSlashIndex + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(registryHive), registryHive, $"The registry hive '{registryHive}' is not handled");
            }
        }
    }
}