﻿using UnityEngine;
using System.Collections;

namespace MoveToCursor {

    /// Move GO to cursor position in 3d space.
    public class MoveToCursorPosition : MonoBehaviour {

        #region CONSTANTS

        public const string Version = "v0.1.0";
        public const string Extension = "MoveToCursor";

        #endregion
 
        /// Keep position of the mouse cursor positon in 3d space.
        /// Helper field.
        private Vector3 _cursorPos;

        /// Info about collided object
        private RaycastHit hit;

        private void Start() {

        }

        private void Update() {
            FindCursor3dPosition();
            transform.position = _cursorPos;
        }

        /// Find cursor position in 3d space
        private void FindCursor3dPosition() {
            Ray rayToCursor;
            // Create Ray from camera to the mouse cursor position
            rayToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Set laser pointer's position
            if (Physics.Raycast(rayToCursor, out hit)) {
                // Allow shooting all-over the enemy
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

    }

}