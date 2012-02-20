using System.Collections.Generic;
using Raven.Client.Document;

namespace MilestoneForWindows.Application.Interfaces
{
    public interface IStorage
    {
        T Load<T>(string id);
        bool Save<T>(T obj);
        IList<T> LoadAll<T>();
        void Clear<T1>();
        DocumentStore DocumentStore { get; }
    }
}