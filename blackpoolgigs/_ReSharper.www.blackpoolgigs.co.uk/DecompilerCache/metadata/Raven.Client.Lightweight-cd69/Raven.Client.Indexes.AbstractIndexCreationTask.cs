// Type: Raven.Client.Indexes.AbstractIndexCreationTask
// Assembly: Raven.Client.Lightweight, Version=1.0.0.0, Culture=neutral, PublicKeyToken=37f41c7f99471593
// Assembly location: C:\business websites\lib\raven\Raven.Client.Lightweight.dll

using Raven.Client;
using Raven.Database.Indexing;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Raven.Client.Indexes
{
    [InheritedExport]
    public abstract class AbstractIndexCreationTask
    {
        public virtual string IndexName { get; }
        public IDocumentStore DocumentStore { get; }
        public abstract IndexDefinition CreateIndexDefinition();
        protected IEnumerable<dynamic> Project<T>(IEnumerable<T> self, Func<T, object> projection);
        public virtual void Execute(IDocumentStore documentStore);
        public virtual Task ExecuteAsync(IDocumentStore documentStore);
    }
}
