// Type: System.Configuration.Install.Installer
// Assembly: System.Configuration.Install, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Configuration.Install.dll

using System.Collections;
using System.ComponentModel;

namespace System.Configuration.Install
{
    [DefaultEvent("AfterInstall")]
    public class Installer : Component
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public InstallContext Context { get; set; }

        public virtual string HelpText { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
        public InstallerCollection Installers { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(true)]
        [TypeConverter(typeof (InstallerParentConverter))]
        public Installer Parent { get; set; }

        public virtual void Commit(IDictionary savedState);
        public virtual void Install(IDictionary stateSaver);
        protected virtual void OnCommitted(IDictionary savedState);
        protected virtual void OnAfterInstall(IDictionary savedState);
        protected virtual void OnAfterRollback(IDictionary savedState);
        protected virtual void OnAfterUninstall(IDictionary savedState);
        protected virtual void OnCommitting(IDictionary savedState);
        protected virtual void OnBeforeInstall(IDictionary savedState);
        protected virtual void OnBeforeRollback(IDictionary savedState);
        protected virtual void OnBeforeUninstall(IDictionary savedState);
        public virtual void Rollback(IDictionary savedState);
        public virtual void Uninstall(IDictionary savedState);

        public event InstallEventHandler Committed;
        public event InstallEventHandler AfterInstall;
        public event InstallEventHandler AfterRollback;
        public event InstallEventHandler AfterUninstall;
        public event InstallEventHandler Committing;
        public event InstallEventHandler BeforeInstall;
        public event InstallEventHandler BeforeRollback;
        public event InstallEventHandler BeforeUninstall;
    }
}
