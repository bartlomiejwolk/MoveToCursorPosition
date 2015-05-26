using UnityEditor;
using UnityEngine;

namespace MoveToCursorPositionEx {

    [CustomEditor(typeof (MoveToCursorPosition))]
    public sealed class MoveToCursorPositionEditor : Editor {
        #region FIELDS

        private MoveToCursorPosition Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        private SerializedProperty description;
        private SerializedProperty excludedTag;
        private SerializedProperty layerMask;
        private SerializedProperty maxHeight;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawDescriptionTextArea();

            EditorGUILayout.Space();

            DrawLayerMaskDropdown();
            DrawExcludedTagDropdown();
            DrawMaxHeightField();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable() {
            description = serializedObject.FindProperty("description");
            layerMask = serializedObject.FindProperty("layerMask");
            excludedTag = serializedObject.FindProperty("excludedTag");
            maxHeight = serializedObject.FindProperty("maxHeight");
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR

        private void DrawDescriptionTextArea() {
            description.stringValue = EditorGUILayout.TextArea(
                description.stringValue);
        }

        private void DrawExcludedTagDropdown() {
            excludedTag.stringValue = EditorGUILayout.TagField(
                new GUIContent(
                    "Exclude Tag",
                    "GOs with this tag will be excluded."),
                excludedTag.stringValue);
        }

        private void DrawLayerMaskDropdown() {
            EditorGUILayout.PropertyField(
                layerMask,
                new GUIContent(
                    "Layer mask",
                    "Layers to be included when raycasting from the camera."));
        }

        private void DrawMaxHeightField() {
            EditorGUILayout.PropertyField(
                maxHeight,
                new GUIContent(
                    "Max Height",
                    "Max height that this transform can achieve."));
        }

        private void DrawVersionLabel() {
            EditorGUILayout.LabelField(
                string.Format(
                    "{0} ({1})",
                    MoveToCursorPosition.Version,
                    MoveToCursorPosition.Extension));
        }

        #endregion INSPECTOR

        #region METHODS

        [MenuItem("Component/MoveToCursorPosition")]
        private static void AddMoveToCursorPositionComponent() {
            if (Selection.activeGameObject != null) {
                Selection.activeGameObject.AddComponent(
                    typeof (MoveToCursorPosition));
            }
        }

        #endregion METHODS
    }

}