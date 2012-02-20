using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Milestone.Core.Indexes;
using MilestoneForWindows.Application.Interfaces;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace MilestoneForWindows.Application
{
    public class Storage : IStorage
    {
        static Storage _instance;
        public static Storage Instance
        {
            get { return _instance ?? (_instance = new Storage()); }
        }

        private static readonly string DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Milestone");
        private readonly EmbeddableDocumentStore _documentStore;

        public Storage()
        {
            _instance = this;
            _documentStore = new EmbeddableDocumentStore { DataDirectory = DataPath };
            _documentStore.Initialize();
            Raven.Client.Indexes.IndexCreation.CreateIndexes(typeof(IssueByRepoId).Assembly, _documentStore);
        }

        public T Load<T>(string id)
        {
            using (var session = _documentStore.OpenSession())
            {
                var entity = session.Load<T>(id);

                return entity;
            }
        }

        public bool Save<T>(T obj)
        {
            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    session.Store(obj);
                    session.SaveChanges();
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Clear<T>()
        {
            using (var session = _documentStore.OpenSession())
            {
                var x = session.Query<T>().Take(100).ToList();
                x.ForEach(session.Delete);
                session.SaveChanges();
            }
        }

        public DocumentStore DocumentStore
        {
            get { return _documentStore; }
        }

        public IList<T> LoadAll<T>()
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<T>().Take(100).ToList();
            }
        }
    }
}
