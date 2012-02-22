using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using MilestoneForWindows.Application.Interfaces;
using MilestoneForWindows.Collections;

namespace MilestoneForWindows.Repositories
{
    public class GenericRepository<T> : ThreadSafeObservableCollection<T>, IObservable<T>
    {
        private readonly IStorage _storage;
        private readonly Subject<T> _outgoingItems = new Subject<T>();
        private readonly Subject<T> _incomingUpdates = new Subject<T>();
        private readonly IObserver<T> _defaultObserver;

        public GenericRepository(IStorage storage)
        {
            _defaultObserver = Observer.Create<T>(CheckInternal);
            _incomingUpdates.Subscribe(_defaultObserver);
            _storage = storage;
        }

        private void CheckInternal(T obj)
        {
            _outgoingItems.OnNext(obj);
        }

        public new void Add(T item)
        {
            InsertItem(0, item);
            _incomingUpdates.OnNext(item);
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


        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _outgoingItems.Subscribe(observer);
        }
    }
}