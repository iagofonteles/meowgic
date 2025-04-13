using System.Collections;
using System.Collections.Generic;

namespace Drafts
{
    public interface ISearchSettings
    {
        string Title { get; }
        IEnumerable GetItens(object target);
        string GetName(object obj);
    }

    public interface ISearchSettings<T> : ISearchSettings
    {
        new IEnumerable<T> GetItens(object target);
        string GetName(T obj);

        IEnumerable ISearchSettings.GetItens(object target) => GetItens(target);
        string ISearchSettings.GetName(object obj) => GetName((T)obj);
    }
}