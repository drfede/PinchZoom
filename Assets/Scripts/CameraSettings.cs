using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinchZoom
{
    [CreateAssetMenu(fileName = "New Camera Settings", menuName = "PinchZoomCamera/CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        [Header("Drag Parameters")]
        [SerializeField, Range(0.1f, 1.0f)]
        private float dragSpeed = 0.3f;

        [Header("Zoom parameters")]
        [SerializeField, Range(0.1f, 1.0f)]
        private float zoomSpeed = 0.1f;
        [SerializeField]
        private float minCameraZoom = 1.5f;
        [SerializeField]
        private float maxCameraZoom = 6.0f;

        [Header("Map Limits")]
        [SerializeField]
        private float topLimit;
        [SerializeField]
        private float bottomLimit;
        [SerializeField]
        private float leftLimit;
        [SerializeField]
        private float rightLimit;



        public float DragSpeed { get => dragSpeed; private set => dragSpeed = value; }
        public float TopLimit { get => topLimit; set => topLimit = value; }
        public float RightLimit { get => rightLimit; set => rightLimit = value; }
        public float LeftLimit { get => leftLimit; set => leftLimit = value; }
        public float BottomLimit { get => bottomLimit; set => bottomLimit = value; }
        public float ZoomSpeed { get => zoomSpeed; set => zoomSpeed = value; }
        public float MinCameraZoom { get => minCameraZoom; set => minCameraZoom = value; }
        public float MaxCameraZoom { get => maxCameraZoom; set => maxCameraZoom = value; }
    }

}