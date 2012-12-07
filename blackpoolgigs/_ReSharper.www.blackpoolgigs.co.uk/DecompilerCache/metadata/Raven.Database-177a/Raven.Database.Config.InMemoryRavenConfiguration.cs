// Type: Raven.Database.Config.InMemoryRavenConfiguration
// Assembly: Raven.Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=37f41c7f99471593
// Assembly location: C:\business websites\lib\raven\Raven.Database.dll

using Raven.Database;
using Raven.Database.Storage;
using Raven.Http;
using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition.Hosting;
using System.Threading;

namespace Raven.Database.Config
{
    public class InMemoryRavenConfiguration : IRaveHttpnConfiguration
    {
        public string DefaultStorageTypeName { get; set; }
        public string ServerUrl { get; }
        public string PluginsDirectory { get; set; }
        public TransactionMode TransactionMode { get; set; }
        public string DataDirectory { get; set; }
        public AggregateCatalog Catalog { get; set; }
        public bool RunInUnreliableYetFastModeThatIsNotSuitableForProduction { get; set; }
        public bool RunInMemory { get; set; }
        public int MaxPageSize { get; set; }
        public int TempIndexPromotionThreshold { get; set; }
        public int TempIndexPromotionMinimumQueryCount { get; set; }
        public TimeSpan TempIndexCleanupPeriod { get; set; }
        public TimeSpan TempIndexCleanupThreshold { get; set; }
        public int MaxNumberOfItemsToIndexInSingleBatch { get; set; }
        public int MaxNumberOfParallelIndexTasks { get; set; }
        public ThreadPriority BackgroundTasksPriority { get; set; }

        #region IRaveHttpnConfiguration Members

        public string GetFullUrl(string baseUrl);
        public NameValueCollection Settings { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string WebDir { get; set; }
        public string AccessControlAllowOrigin { get; set; }
        public AnonymousUserAccessMode AnonymousUserAccessMode { get; set; }
        public string VirtualDirectory { get; set; }
        public CompositionContainer Container { get; set; }
        public bool HttpCompression { get; set; }

        #endregion

        public void Initialize();
        protected void ResetContainer();
        protected AnonymousUserAccessMode GetAnonymousUserAccessMode();
        public void LoadLoggingSettings();
        public T? GetConfigurationValue<T>(string configName) where T : struct, new();
        public ITransactionalStorage CreateTransactionalStorage(Action notifyAboutWork);
    }
}
