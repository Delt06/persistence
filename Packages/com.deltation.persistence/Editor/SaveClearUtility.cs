using System.IO;
using UnityEditor;
using UnityEngine;

namespace DELTation.Persistence.Editor
{
    internal static class SaveClearUtility
    {
        private const string MenuBase = "Tools/Persistence/";

        [MenuItem(MenuBase + "Locate")]
        public static void Locate()
        {
            if (Directory.Exists(PersistenceUtils.SavesPath))
                EditorUtility.OpenWithDefaultApp(PersistenceUtils.SavesPath);
            else
                Debug.LogWarning("Saves directory not found. It may indicate that there are no saves.");
        }

        [MenuItem(MenuBase + "Clear All")]
        public static void ClearAll()
        {
            if (EditorUtility.DisplayDialog("Clear All", "Are you sure you want to clear all saves?", "Yes", "No"))
                if (Directory.Exists(PersistenceUtils.SavesPath))
                    Directory.Delete(PersistenceUtils.SavesPath, true);
        }
    }
}