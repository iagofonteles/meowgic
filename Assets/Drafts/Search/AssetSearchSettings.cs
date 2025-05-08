using System;
using System.Collections.Generic;
using System.Linq;
using UObj = UnityEngine.Object;

namespace Drafts
{
    public class AssetSearchSettings : ISearchSettings<UObj>
    {
        public static Func<Type, string, IEnumerable<UObj>> _findAssets;

        public Type Type { get; }
        public string Folder { get; }
        public string Title => $"{Type.Name} in {Folder}";
        public IEnumerable<UObj> GetItens(object target) => _findAssets(Type, Folder);
        public string GetName(UObj o) => o?.name;

        public AssetSearchSettings(Type type, string folder)
        {
            Type = type;
            Folder = folder;
        }
    }

    public class AssetSearchSettings<T> : ISearchSettings<T> where T : UObj
    {
        public Type Type { get; }
        public string Folder { get; }
        public string Title => $"{Type.Name} in {Folder}";
        public IEnumerable<T> GetItens(object target) => AssetSearchSettings._findAssets(Type, Folder).OfType<T>();
        public string GetName(T o) => o?.name;

        public AssetSearchSettings(string folder)
        {
            Type = typeof(T);
            Folder = folder;
        }
    }

    public class AssetNameSearchSettings : ISearchSettings<string>
    {
        public static Func<Type, string, IEnumerable<UnityEngine.Object>> _findAssets;

        public Type Type { get; }
        public string Folder { get; }
        public string Title => $"{Type.Name} in {Folder}";
        public IEnumerable<string> GetItens(object target) => _findAssets(Type, Folder).Select(a => a.name);
        public string GetName(string o) => o;

        public AssetNameSearchSettings(Type type, string folder)
        {
            Type = type;
            Folder = folder;
        }
    }
}