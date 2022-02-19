using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinchZoom
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField]
        private float panAcceleration = 2.0f;

        [SerializeField]
        private Vector2 minCameraValues = new(-10, 10);
        [SerializeField]
        private Vector2 maxCameraValues = new(-10, 10);
        [SerializeField,Range(0.1f,3.0f)]
        private float dragSpeed = 1.0f;
        [SerializeField]
        private Camera camera;

        private Vector3 dragStartingPosition = new();

        private bool isPanEnabled = true;
        private bool isZoomEnabled = true;

        public bool IsPanEnabled { get => isPanEnabled; set => isPanEnabled = value; }
        public bool IsZoomEnabled { get => isZoomEnabled; set => isZoomEnabled = value; }

        private void Awake()
        {
            if (camera == null)
                camera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (IsPanEnabled)
                TryPan();


            //if (IsZoomEnabled)
            //    TryZoom();

            //Add Border Check

        }

        private void TryZoom()
        {
            throw new NotImplementedException();
        }

        private void TryPan()
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    dragStartingPosition = touch.position;
                }
                else
                {
                    var movement = -touch.deltaPosition * dragSpeed * Time.deltaTime;
                    Debug.Log(movement);
                    camera.transform.Translate(movement.x, movement.y, 0);
                }
            }
        }
    }
}
