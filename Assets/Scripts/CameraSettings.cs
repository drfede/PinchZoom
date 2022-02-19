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
        [SerializeField, Range(0.01f, 1.0f)]
        private float decelerationFriction = 0.1f;
        [SerializeField, Min(0)]
        private float decelerationTime = 0.3f;
        [SerializeField]
        private float minScreenDimensionModifier = 1.0f;
        [SerializeField]
        private float maxScreenDimensionModifier = 1.5f;

        [Header("Zoom parameters")]
        [SerializeField, Range(0.1f, 1.0f)]
        private float zoomSpeed = 0.1f;

        [SerializeField]
        private float minCameraZoom = 1.5f;
        [SerializeField]
        private float maxCameraZoom = 6.0f;
        [SerializeField]
        private float minZoomTolerance = 0.5f;
        [SerializeField]
        private float maxZoomTolerance = 1.0f;
        [SerializeField]
        private float zoomCorrectionTime = 0.2f;

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
        public float DecelerationFriction { get => decelerationFriction; set => decelerationFriction = value; }
        public float DecelerationTime { get => decelerationTime; set => decelerationTime = value; }
        public float MinZoomTolerance { get => minZoomTolerance; set => minZoomTolerance = value; }
        public float ZoomCorrectionTime { get => zoomCorrectionTime; set => zoomCorrectionTime = value; }
        public float MaxZoomTolerance { get => maxZoomTolerance; set => maxZoomTolerance = value; }
        public float MinScreenDimensionModifier { get => minScreenDimensionModifier; set => minScreenDimensionModifier = value; }
        public float MaxScreenDimensionModifier { get => maxScreenDimensionModifier; set => maxScreenDimensionModifier = value; }
    }

}