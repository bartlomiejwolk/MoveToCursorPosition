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
        /// Keep position of the mouse cursor positon in 3d space.
        /// Helper field.
        #endregion

        #region INSPECTOR FIELDS

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private string excludedTag;

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

        #endregion

        #region UNITY MESSAGES

        private void Update() {
            UpdateCursor3dPosition();
        }

        #endregion

        #region METHODS

        /// Find cursor position in 3d space
        // todo extract
        private void UpdateCursor3dPosition() {
            if (Camera.main == null) {
                Debug.LogWarning("There's no camera tagged MainCamera in " +
                                 "the scene.");
                return;
            }

            // Create Ray from camera to the mouse cursor position
            var rayToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Set laser pointer's position
            // todo add max distance
            // todo add options
            var rayHitSth = Physics.Raycast(
                rayToCursor,
                out hit,
                Mathf.Infinity,
                LayerMask);

            var cursorPos = transform.position;

            if (rayHitSth) {
                // Allow shooting all-over the enemy
                // todo use tag dropdown
                if (hit.collider.tag == "Enemy") {
                    cursorPos = new Vector3(
                        hit.point.x,
                        hit.point.y,
                        hit.point.z);
                }
                // Don't modify cursor height when player doesn't aim high
                else if (hit.point.y <= transform.position.y + 1)
                    cursorPos = new Vector3(
                        hit.point.x,
                        hit.point.y,
                        hit.point.z);
                // Don't allow laser pointer to go above certain hight (like above walls)
                else
                    cursorPos =
                        new Vector3(
                            hit.point.x,
                            transform.position.y + 1,
                            hit.point.z);
            }

            // Update transform position.
            transform.position = cursorPos;
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
