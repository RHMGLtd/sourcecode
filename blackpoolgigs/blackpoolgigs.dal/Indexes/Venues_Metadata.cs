using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal.Indexes
{
    public class Venues_Metadata : AbstractIndexCreationTask
    {
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition
            {
                Map = @"from doc in docs.metadata
                    select new{doc.VenueName }"
            };
        }
    }
}