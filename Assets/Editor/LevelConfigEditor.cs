using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Infrastructure.StaticData.LevelConfig))]
public class LevelConfigEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var levelConfig = (Infrastructure.StaticData.LevelConfig)target;
        var waveConfigsProperty = serializedObject.FindProperty("waveConfigs");

        for (int i = 0; i < waveConfigsProperty.arraySize; i++)
        {
            var waveConfigProperty = waveConfigsProperty.GetArrayElementAtIndex(i);
            var nameProperty = waveConfigProperty.FindPropertyRelative("name");

            EditorGUILayout.BeginHorizontal();
            nameProperty.stringValue = EditorGUILayout.TextField("Wave " + i, nameProperty.stringValue);
            if (GUILayout.Button("Remove"))
            {
                waveConfigsProperty.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(waveConfigProperty.FindPropertyRelative("waveDataList"), new GUIContent(nameProperty.stringValue), true);
            EditorGUI.indentLevel--;
        }

        if (GUILayout.Button("Add Wave Config"))
        {
            waveConfigsProperty.InsertArrayElementAtIndex(waveConfigsProperty.arraySize);
            var newWaveConfig = waveConfigsProperty.GetArrayElementAtIndex(waveConfigsProperty.arraySize - 1);
            newWaveConfig.FindPropertyRelative("name").stringValue = "New Wave Config";
        }

        serializedObject.ApplyModifiedProperties();
    }
}
