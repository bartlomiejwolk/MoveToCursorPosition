using UnityEditor;
using UnityEngine;

namespace MoveToCursor {

    [CustomEditor(typeof(MoveToCursorPosition))]
    public sealed class MoveToCursorPositionEditor : Editor {
        #region FIELDS

        private MoveToCursorPosition Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        private SerializedProperty layerMask;
        private SerializedProperty excludedTag;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        private void OnEnable() {
            layerMask = serializedObject.FindProperty("layerMask");
            excludedTag = serializedObject.FindProperty("excludedTag");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawLayerMaskDropdown();
            DrawExcludedTagDropdown();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawLayerMaskDropdown() {
            EditorGUILayout.PropertyField(
                layerMask,
                new GUIContent(
                    "Layer mask",
                    "Untagged"));
        }

        private void DrawExcludedTagDropdown() {
            excludedTag.stringValue = EditorGUILayout.TagField(
                new GUIContent(
                    "Exclude Tag",
                    ""),
                excludedTag.stringValue);
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR

        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(
                string.Format(
                    "{0} ({1})",
                    MoveToCursorPosition.Version,
                    MoveToCursorPosition.Extension));
        }

        #endregion INSPECTOR

        #region METHODS

        [MenuItem("Component/MyNamespace/MoveToCursorPosition")]
        private static void AddMoveToCursorPositionComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(typeof(MoveToCursorPosition));
            }
        }

        #endregion METHODS
    }

}