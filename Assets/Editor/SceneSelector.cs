using System.Linq;
using Infrastructure;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SceneSelector : EditorWindow
    {
        private string[] sceneNames;
        private int selectedSceneIndex;
        private string selectedSceneName;

        [MenuItem("Tools/Scene Selector")]
        public static void ShowWindow()
        {
            GetWindow<SceneSelector>("Scene Selector");
        }

        private void OnEnable()
        {
            sceneNames = EditorBuildSettings.scenes
                .Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path))
                .ToArray();
        }

        private void OnGUI()
        {
            GUILayout.Label("Выберите сцену", EditorStyles.boldLabel);

            selectedSceneIndex = EditorGUILayout.Popup("Сцена", selectedSceneIndex, sceneNames);

            SelectSceneData.SelectedSceneName = sceneNames[selectedSceneIndex];
        }
    }
}
