using UnityEditor;
using UnityEngine;

namespace MoveToCursor {

    [CustomEditor(typeof(MoveToCursorPosition))]
    public sealed class MoveToCursorPositionEditor : Editor {
        #region FIELDS

        private MoveToCursorPosition Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable() {
            Script = (MoveToCursorPosition)target;

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