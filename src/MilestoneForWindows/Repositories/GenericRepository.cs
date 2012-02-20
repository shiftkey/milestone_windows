using MilestoneForWindows.Application.Interfaces;
using MilestoneForWindows.Collections;

namespace MilestoneForWindows.Repositories
{
    public class GenericRepository<T> : ThreadSafeObservableCollection<T>
    {
        private readonly IStorage _storage;

        public GenericRepository(IStorage storage)
        {
            _storage = storage;
        }

        public void Load()
        {
            using (var session = _storage.DocumentStore.OpenSession())
            {
                var result = session.Query<T>();
                foreach (var r in result)
                    Add(r);
            }
        }

        public void Save()
        {
            using (var session = _storage.DocumentStore.OpenSession())
            {
                foreach (var item in ToSyncArray())
                {
                    session.Store(item);
                }

                session.SaveChanges();
            }
        }
    }
}