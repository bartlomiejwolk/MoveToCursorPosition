#define DEBUG_LOGGER

using UnityEngine;
using System.Collections;
using FileLogger;

namespace MoveToCursorPositionEx {

    /// Move GO to cursor position in 3d space.
    public class MoveToCursorPosition : MonoBehaviour {

        #region CONSTANTS

        public const string Version = "v0.1.0";
        public const string Extension = "MoveToCursor";

        #endregion

        #region FIELDS

        /// <summary>
        /// Allows identify component in the scene file when reading it with
        /// text editor.
        /// </summary>
#pragma warning disable 0414
        [SerializeField]
        private string componentName = "MyClass";
#pragma warning restore0414

        private RaycastHit hitInfo;

        #endregion

        #region INSPECTOR FIELDS

        [SerializeField]
        private string description = "Description";
 
        #endregion

        #region INSPECTOR FIELDS

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private string excludedTag = "";

        /// <summary>
        /// Max height for the transform on y axis.
        /// </summary>
        [SerializeField]
        private float maxHeight;

        #endregion

        #region PROPERTIES
        public LayerMask LayerMask {
            get { return layerMask; }
            set { layerMask = value; }
        }

        public string ExcludedTag {
            get { return excludedTag; }
            set { excludedTag = value; }
        }

        public float MaxHeight {
            get { return maxHeight; }
            set { maxHeight = value; }
        }

        public string Description {
            get { return description; }
            set { description = value; }
        }

        #endregion

        #region UNITY MESSAGES

        private void Update() {
            UpdateCursor3dPosition();
        }

        #endregion

        #region METHODS

        /// Find cursor position in 3d space
        private void UpdateCursor3dPosition() {
            if (Camera.main == null) {
                Debug.LogWarning("There's no camera tagged MainCamera in " +
                                 "the scene.");
                return;
            }

            // Throw ray and return if did not hit anything.
            if (!ThrowRay()) return;
            // Handle excluded tag.
            if (hitInfo.transform.tag == ExcludedTag) return;
            // Handle max height option.
            if (hitInfo.point.y > MaxHeight) return;

            // Calculate new cursor position.
            var cursorPos = new Vector3(
                hitInfo.point.x,
                hitInfo.point.y,
                hitInfo.point.z);

            // Update transform position.
            transform.position = cursorPos;
        }

        private bool ThrowRay() {
            // Create Ray from camera to the mouse cursor position
            var rayToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Set laser pointer's position
            // todo add max distance
            // todo add options
            return Physics.Raycast(
                rayToCursor,
                out hitInfo,
                Mathf.Infinity,
                LayerMask);
        }

        // todo move it to a utility class
        public void AddLayer(string layerName) {
            Logger.LogCall();

            var layerIndex = LayerMask.NameToLayer(layerName);
            LayerMask |= 1 << layerIndex;
        }

        /// <summary>
        /// Sets layer mask value to specified layer. All other layers are
        /// unset.
        /// </summary>
        /// <param name="layerName"></param>
        // todo move it to a utility class
        public void SetLayer(string layerName) {
            Logger.LogCall();

            var layerIndex = LayerMask.NameToLayer(layerName);
            LayerMask = 1 << layerIndex;
        }

        // todo move it to a utility class
        public void UnsetLayer(string layerName) {
            var layerIndex = LayerMask.NameToLayer(layerName);
            LayerMask ^= 1 << layerIndex;
        }

        #endregion

    }

}
