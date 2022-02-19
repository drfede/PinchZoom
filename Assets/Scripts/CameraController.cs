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
        private CameraSettings settings;

        [SerializeField]
        private Vector2 minCameraValues = new(-10, 10);
        [SerializeField]
        private Vector2 maxCameraValues = new(-10, 10);
        [SerializeField]
        private Camera camera;

        private Vector3 dragStartingPosition = new();

        private bool isDragging = false;

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


            if (IsZoomEnabled)
                TryZoom();

            CheckBorders();

        }

        private void CheckBorders()
        {
            var cameraPos = camera.transform.position;
            if (cameraPos.x <= settings.LeftLimit)
                camera.transform.position = new(settings.LeftLimit, cameraPos.y);
            if (cameraPos.x >= settings.RightLimit)
                camera.transform.position = new(settings.RightLimit, cameraPos.y);

            if (cameraPos.y >= settings.TopLimit)
                camera.transform.position = new(cameraPos.x, settings.TopLimit);
            if (cameraPos.y <= settings.BottomLimit)
                camera.transform.position = new(cameraPos.x, settings.BottomLimit);

        }

        private void TryZoom()
        {
            if (Input.touchCount == 2)
            {
                var firstTouch = Input.touches[0];
                var secondTouch = Input.touches[1];
            }
        }

        private void TryPan()
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    isDragging = true;
                }
                
                if (touch.phase == TouchPhase.Moved && isDragging)
                {
#if UNITY_EDITOR
                    var movement = -touch.deltaPosition * settings.DragSpeed * Time.deltaTime * 10;
#else
                    var movement = -touch.deltaPosition * settings.DragSpeed * Time.deltaTime;
#endif

                    Debug.Log(movement);
                    camera.transform.Translate(movement.x, movement.y, 0);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    isDragging = false;
                }
            }
        }

    }
}
