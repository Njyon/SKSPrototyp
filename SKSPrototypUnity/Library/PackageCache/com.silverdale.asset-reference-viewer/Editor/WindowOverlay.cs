﻿using UnityEditor;
using UnityEngine;

namespace AssetReferenceViewer
{
    [InitializeOnLoad]
    public static partial class WindowOverlay
    {
        static WindowOverlay()
        {
            enabled = Enabled;
            EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUI;
        }

        private static void ProjectWindowItemOnGUI(string guid, Rect rect)
        {
            if (enabled) 
            {
                AssetInfo assetInfo = AssetReferenceViewer.GetAsset(AssetDatabase.GUIDToAssetPath(guid));
                if (assetInfo != null) 
                {
                    var content = new GUIContent(assetInfo.IsIncludedInBuild ? ProjectIcons.LinkBlue : ProjectIcons.LinkBlack, assetInfo.IncludedStatus.ToString());
                    GUI.Label(new Rect(rect.width + rect.x - 20, rect.y + 1, 16, 16), content);
                }
            }
        }

        private static bool enabled;

        public static bool Enabled {
            get {
                return enabled = EditorPrefs.GetBool("AssetReferenceViewer");
            }
            set => EditorPrefs.SetBool("AssetReferenceViewer", enabled = value);
        }
    }
}