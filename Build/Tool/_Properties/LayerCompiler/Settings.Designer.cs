﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LayerCompiler.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("RTCOP::Generated")]
        public string NameSpace {
            get {
                return ((string)(this["NameSpace"]));
            }
            set {
                this["NameSpace"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CR+LF")]
        public string LineTerminator {
            get {
                return ((string)(this["LineTerminator"]));
            }
            set {
                this["LineTerminator"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("utf-8n")]
        public string Encoding {
            get {
                return ((string)(this["Encoding"]));
            }
            set {
                this["Encoding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("WindowsX86")]
        public string DefaultDevelopmentTarget {
            get {
                return ((string)(this["DefaultDevelopmentTarget"]));
            }
            set {
                this["DefaultDevelopmentTarget"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("VisualStudio")]
        public string DefaultDevelopmentEnvironment {
            get {
                return ((string)(this["DefaultDevelopmentEnvironment"]));
            }
            set {
                this["DefaultDevelopmentEnvironment"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("RTCOP/")]
        public string RTCOPIncludePath {
            get {
                return ((string)(this["RTCOPIncludePath"]));
            }
            set {
                this["RTCOPIncludePath"] = value;
            }
        }
    }
}
