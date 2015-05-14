#define DEBUG_LOGGER

using UnityEngine;
using System.Collections;
using FileLogger;

namespace MoveToCursor {

    /// Move GO to cursor position in 3d space.
    public class MoveToCursorPosition : MonoBehaviour {

        #region CONSTANTS

        public const string Version = "v0.1.0";
        public const string Extension = "MoveToCursor";

        #endregion

        #region FIELDS
        /// Keep position of the mouse cursor positon in 3d space.
        /// Helper field.
        private Vector3 _cursorPos;

        /// Info about collided object
        private RaycastHit hit;
        #endregion

        #region INSPECTOR FIELDS

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private string excludedTag;

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

        #endregion

        #region METHODS

        private void Update() {
            FindCursor3dPosition();
            transform.position = _cursorPos;
        }

        /// Find cursor position in 3d space
        // todo extract
        private void FindCursor3dPosition() {
            // Create Ray from camera to the mouse cursor position
            var rayToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Set laser pointer's position
            // todo add max distance
            // todo add options
            if (Physics.Raycast(rayToCursor, out hit, Mathf.Infinity, LayerMask)) {
                // Allow shooting all-over the enemy
                // todo use tag dropdown
                if (hit.collider.tag == "Enemy") {
                    _cursorPos = new Vector3(
                        hit.point.x,
                        hit.point.y,
                        hit.point.z);
                }
                // Don't modify cursor height when player doesn't aim high
                else if (hit.point.y <= transform.position.y + 1)
                    _cursorPos = new Vector3(
                        hit.point.x,
                        hit.point.y,
                        hit.point.z);
                // Don't allow laser pointer to go above certain hight (like above walls)
                else
                    _cursorPos =
                        new Vector3(
                            hit.point.x,
                            transform.position.y + 1,
                            hit.point.z);
            }
        }

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
        public void SetLayer(string layerName) {
            Logger.LogCall();

            var layerIndex = LayerMask.NameToLayer(layerName);
            LayerMask = 1 << layerIndex;
        }

        public void UnsetLayer(string layerName) {
            var layerIndex = LayerMask.NameToLayer(layerName);
            LayerMask ^= 1 << layerIndex;
        }

        #endregion

    }

}
