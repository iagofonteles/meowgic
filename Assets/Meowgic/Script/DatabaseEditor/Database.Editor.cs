#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Meowgic
{
    public partial class Database
    {
        [MenuItem("Meowgic/Refresh Database")]
        private static void RefreshDatabase()
        {
            var db = Resources.Load<Database>("Database");
            db.FetchData();
            Selection.activeObject = db;
        }

        [ContextMenu("FetchData")]
        private void FetchData()
        {
            GetAssets();
            AssetDatabase.SaveAssets();
        }

        private void GetAssets()
        {
            var itemTypes = Drafts.TypeCache<IDatabaseItem>.All;
            databases = itemTypes.Select(CreateBase).ToArray();

            IDatabase CreateBase(Type type)
            {
                var guids = AssetDatabase.FindAssets($"t:{type}", new[] { searchFolder });
                var paths = guids.Select(g => AssetDatabase.GUIDToAssetPath(g));
                var assets = paths.Select(p => AssetDatabase.LoadAssetAtPath(p, type));

                var dbType = typeof(Database<>).MakeGenericType(type);
                return (IDatabase)Activator.CreateInstance(dbType, assets);
            }

            _map = null;
            EditorUtility.SetDirty(this);
        }
    }
}
#endif