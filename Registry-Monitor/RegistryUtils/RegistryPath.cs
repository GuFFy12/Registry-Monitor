using System;
using System.Security.Principal;
using Microsoft.Win32;

namespace Registry_Monitor.RegistryUtils
{
    public class RegistryPath
    {
        public enum RegistryHives
        {
            HKEY_CLASSES_ROOT,
            HKEY_CURRENT_USER,
            HKEY_LOCAL_MACHINE,
            HKEY_USERS,
            HKEY_CURRENT_CONFIG
        }

        private readonly MainForm.LoggerDelegate _logger;

        public readonly bool Error;
        public readonly RegistryHives Hive;
        public readonly string RootPath;
        public readonly WmiRegistryEventListener.TrackTypes TrackType;
        public readonly string Value;

        /**
         * Validate that registryPathText is actually registry path.
         */
        public RegistryPath(WmiRegistryEventListener.TrackTypes trackType, string pathText, string valueText, MainForm.LoggerDelegate loggerDelegate)
        {
            _logger = loggerDelegate;

            var firstSlashIndex = pathText.IndexOf('\\');
            if (firstSlashIndex == -1)
            {
                Error = true;
                _logger("Failed to parse registry path", MainForm.LoggerMessageType.Error);
                return;
            }

            var registryHiveText = pathText.Substring(0, firstSlashIndex).ToUpper();
            if (!Enum.IsDefined(typeof(RegistryHives), registryHiveText))
            {
                Error = true;
                _logger("Failed to parse registry hive", MainForm.LoggerMessageType.Error);
                return;
            }

            TrackType = trackType;
            Value = valueText;

            /*
             * So we have a problem, WMI can only access to HKEY_LOCAL_MACHINE, HKEY_USERS, HKEY_CURRENT_CONFIG.
             * But we can access to other regedit hives using these root hives.
             */
            var registryHive = (RegistryHives)Enum.Parse(typeof(RegistryHives), registryHiveText);
            switch (registryHive)
            {
                case RegistryHives.HKEY_CLASSES_ROOT:
                    Hive = RegistryHives.HKEY_LOCAL_MACHINE;
                    RootPath = $"SOFTWARE\\Classes\\{pathText.Substring(firstSlashIndex + 1)}";
                    break;
                case RegistryHives.HKEY_CURRENT_USER:
                    Hive = RegistryHives.HKEY_USERS;
                    RootPath = $"{WindowsIdentity.GetCurrent().User?.Value}\\{pathText.Substring(firstSlashIndex + 1)}";
                    break;
                case RegistryHives.HKEY_LOCAL_MACHINE:
                case RegistryHives.HKEY_USERS:
                case RegistryHives.HKEY_CURRENT_CONFIG:
                default:
                    Hive = registryHive;
                    RootPath = pathText.Substring(firstSlashIndex + 1);
                    break;
            }
        }
    }
}