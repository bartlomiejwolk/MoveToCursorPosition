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

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawLayerMaskDropdown();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawLayerMaskDropdown() {
            EditorGUILayout.PropertyField(
                layerMask,
                new GUIContent(
                    "Layer mask",
                    ""));
        }

        private void OnEnable() {
            Script = (MoveToCursorPosition)target;

            layerMask = serializedObject.FindProperty("layerMask");
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