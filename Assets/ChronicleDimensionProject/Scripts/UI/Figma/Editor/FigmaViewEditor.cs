using UnityEditor;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    [CustomEditor(typeof(FigmaView))]
    public class FigmaViewEditor :UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!GUILayout.Button("Auto Assign")) return;
            var view = target as FigmaView;
            if (view == null) return;
            EditorUtil.AutoAssignForView(view.gameObject);
            EditorUtility.SetDirty(view.gameObject);
        }
    }
}