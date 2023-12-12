using System;
using System.Security.Principal;

namespace Registry_Monitor.RegistryUtils
{
    public class RegistryPath
    {
        public enum RegistryHive
        {
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CURRENT_CONFIG
        }

        public readonly RegistryHive Hive;
        public readonly WmiRegistryEventListener.RegistryEvent RegistryEvent;
        public readonly string RootPath;
        public readonly string Value;

        /**
         * Validate that registryPathText is actually registry path.
         */
        public RegistryPath(WmiRegistryEventListener.RegistryEvent registryEvent, string pathText, string valueText)
        {
            var firstSlashIndex = pathText.IndexOf('\\');
            if (firstSlashIndex == -1) throw new FormatException("Registry hive not found");

            var registryHiveText = pathText.Substring(0, firstSlashIndex);
            if (!Enum.TryParse(registryHiveText, true, out RegistryHive registryHive))
                throw new ArgumentException($"{registryHiveText} is not a valid registry hive", nameof(registryHiveText));

            RegistryEvent = registryEvent;
            Value = valueText;

            /*
             * So we have a problem, WMI can only access to HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CURRENT_CONFIG.
             * But we can access to other regedit hives using these root hives.
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