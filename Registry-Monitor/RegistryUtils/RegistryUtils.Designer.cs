﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Registry_Monitor.RegistryUtils {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class RegistryUtils {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RegistryUtils() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Registry_Monitor.RegistryUtils.RegistryUtils", typeof(RegistryUtils).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid registry hive.
        /// </summary>
        internal static string RegistryPath_RegistryPath__0__is_not_a_valid_registry_hive {
            get {
                return ResourceManager.GetString("RegistryPath_RegistryPath__0__is_not_a_valid_registry_hive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registry hive not found.
        /// </summary>
        internal static string RegistryPath_RegistryPath_Registry_hive_not_found {
            get {
                return ResourceManager.GetString("RegistryPath_RegistryPath_Registry_hive_not_found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The registry hive &apos;{0}&apos; is not handled.
        /// </summary>
        internal static string RegistryPath_RegistryPath_The_registry_hive___0___is_not_handled {
            get {
                return ResourceManager.GetString("RegistryPath_RegistryPath_The_registry_hive___0___is_not_handled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The registry event type &apos;{0}&apos; is not handled.
        /// </summary>
        internal static string WmiRegistryEventListener_WmiRegistryEventListener_The_registry_event_type___0___is_not_handled {
            get {
                return ResourceManager.GetString("WmiRegistryEventListener_WmiRegistryEventListener_The_registry_event_type___0___i" +
                        "s_not_handled", resourceCulture);
            }
        }
    }
}
