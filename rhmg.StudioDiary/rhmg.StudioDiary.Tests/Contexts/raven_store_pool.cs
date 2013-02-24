//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using Machine.Specifications;
//using Raven.Client;
//using Raven.Client.Document;
//using Raven.Client.Embedded;
//using Raven.Client.Indexes;
//using Raven.Json.Linq;
//using rhmg.StudioDiary.Raven.Indexes;

//namespace rhmg.StudioDiary.Tests.Contexts
//{
//    public static class raven_store_pool
//    {
//        const int store_batch_size = 2;
//        const int max_stores = 20;
//        const int max_initialising_stores = 2;
//        static  int initialising_stores = 0;


//        static readonly ConcurrentBag<raven_store> allStores = new ConcurrentBag<raven_store>();
//        static readonly ConcurrentQueue<raven_store> availableStores = new ConcurrentQueue<raven_store>();
//        static readonly object mutex = new object();
//        static bool initialised;
        
//        public static raven_store get_store()
//        {
//            Initialise();

//            raven_store store;

//            while (!availableStores.TryDequeue(out store))
//            {
//                lock (mutex)
//                {
//                    if (availableStores.TryDequeue(out store))
//                        break;
//                    if (initialising_stores <= max_initialising_stores && allStores.Count <= max_stores)
//                        create_new_batch();
//                }
//                Console.WriteLine("Spin wait for available store");
//                Thread.Sleep(100);
//            }
//            store.open_session();
//            return store;
//        }

//        static void Initialise()
//        {            
//            if (initialised) return;
//            ThreadPool.SetMaxThreads(100, 200);
            
//            lock (mutex)
//            {
//                if (initialised) return;
//                create_new_batch();
//                initialised = true;

//                Console.WriteLine("Initialised");
//            }
//        }

//        static void create_new_batch()
//        {
//            Console.WriteLine("Creating new stores!");
//            for (var i = 0; i < store_batch_size; i++)
//            {
//                var store = new raven_store();
//                initialising_stores++;
//                allStores.Add(store);
//                ThreadPool.QueueUserWorkItem(init_store, store);
//            }
//        }

//        static void init_store(object state)
//        {

//            var store = state as raven_store;
//            if (store == null)
//                return;

//            store.init();
//            availableStores.Enqueue(store);
//            initialising_stores--;
//        }


//        public static void release_store(raven_store store)
//        {
//            ThreadPool.QueueUserWorkItem(release,store);
//        }

//        static void release(object state)
//        {
//            var store = state as raven_store;
//            if (store == null)
//                return;

//            store.reset();
//            availableStores.Enqueue(store);
//        }
//    }

//    public class raven_store
//    {
//        public EmbeddableDocumentStore store;

//        public IDocumentSession session { get; private set; }

//        public void init()
//        {
//            if (store != null)
//            {
//                close();
//            }

//            store = (EmbeddableDocumentStore)Raven.DocumentStoreFactory.GetInMemoryDocumentStoreForUnitTest();
//            store.Initialize();
//            IndexCreation.CreateIndexes(typeof(CustomerArrears).Assembly, store);

//        }

//        public void open_session()
//        {
//            session = store.OpenSession();
//        }

//        public void close()
//        {
//            foreach (var error in store.DocumentDatabase.Statistics.Errors)
//            {
//                Console.WriteLine(@"Raven error on document {0} / index {1} {2}", error.Document, error.Index, error.Error);
//            }
//            store = null;
//            session = null;
//        }

//        public void store_documents(params object[] docs) { store_documents<object>(docs); }

//        private void store_documents<T>(IEnumerable<T> documents)
//        {
//            foreach (var document in documents)
//                session.Store(document);

//            session.SaveChanges();
//        }

//        public void raven_contains<T>(IEnumerable<string> ids)
//        {
//            foreach (var id in ids)
//                raven_contains<T>(id);
//        }
//        public void raven_contains<T>(string id)
//        {
//            session.Advanced.Clear();
//            session.Load<T>(id).ShouldNotBeNull();
//        }

//        public T load_document<T>(string id)
//        {
//            session.Advanced.Clear();
//            var ret = session.Load<T>(id);
//            session.Advanced.Clear();
//            return ret;
//        }

//        public RavenJObject load_metadata<T>(string id)
//        {
//            var doc = load_document<T>(id);
//            return session.Advanced.GetMetadataFor(doc);
//        }

//        public T store_document<T>(T document)
//        {
//            session.Advanced.Clear();
//            session.Store(document);
//            session.SaveChanges();

//            //var entityType = typeof (call);
//            //var property = store.Conventions.GetIdentityProperty(entityType);
//            //var id = (string)property.GetValue(document, null);

//            return load_document<T>((string)((dynamic)document).Id);
//        }

//        public void wait(Action a)
//        {
//            a();
//            wait();
//        }

//        public void reset()
//        {
//            store.DocumentDatabase.StopBackgroundWorkers();
//            var documents = store.DocumentDatabase.GetDocuments(0, 128, null);
//            while (documents != null && documents.Length > 0)
//            {
//                foreach (var doc in documents)
//                {
//                    var docId = doc.SelectToken("@metadata").SelectToken("@id").Value<string>() ?? doc.Value<string>("__document_id");
//                    Console.WriteLine("Cleanup is Deleting document " + docId);
//                    if (!string.IsNullOrEmpty(docId))
//                        store.DocumentDatabase.Delete(docId, null, null);
//                }
//                Thread.Sleep(50);
//                documents = store.DocumentDatabase.GetDocuments(0, 128, null);
//            }
//            wait();

//            var keyGenerator = new MultiTypeHiLoKeyGenerator(store, 1024);
//            store.Conventions.DocumentKeyGenerator = entity => keyGenerator.GenerateDocumentKey(store.Conventions, entity);

//            session.Dispose();
//            session = null;
//        }

//        public IEnumerable<T> index<T>(string name)
//        {
//            return session
//                .Advanced.LuceneQuery<T>(name)
//                .WaitForNonStaleResults()
//                .ToArray();
//        }

//        public EmbeddableDocumentStore wait()
//        {
//            store.DocumentDatabase.SpinBackgroundWorkers();

//            while (store.DocumentDatabase.Statistics.StaleIndexes.Length > 0)
//            {
//                Thread.Sleep(5);
//            }
//            return store;
//        }


//        public void delete_document(string id)
//        {
//            session.Delete(session.Load<object>(id));
//            session.SaveChanges();
//            session.Advanced.Clear();
//        }
//    }
//}