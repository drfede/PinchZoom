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
    }

}