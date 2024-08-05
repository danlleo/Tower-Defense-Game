using System.Linq;
using Logic.PathFinders;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
	[CustomEditor(typeof(MoveKeyPoint))]
	public class LevelStaticDataEditor : UnityEditor.Editor
	{
		private const string MoveKeyPointTag = "MoveKeyPoint";

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			LevelStaticData levelData = (LevelStaticData) target;
			
			if (GUILayout.Button("Collect"))
			{

				levelData.LevelKey = SceneManager.GetActiveScene().name;
			}
      
			EditorUtility.SetDirty(target);
		}
	}
}