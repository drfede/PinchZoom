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


        private bool isDragging = false;
        private bool isZooming = false;

        private bool isPanEnabled = true;
        private bool isZoomEnabled = true;

        private float previousPinchDistance;


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


            if (IsZoomEnabled && camera.orthographic)
                TryZoom();

            CheckLimits();

        }

        private void CheckLimits()
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

            if (camera.orthographic)
            {
                if (camera.orthographicSize >= settings.MaxCameraZoom)
                    camera.orthographicSize = settings.MaxCameraZoom;
                if (camera.orthographicSize <= settings.MinCameraZoom)
                    camera.orthographicSize = settings.MinCameraZoom;
            }

        }

        private void TryZoom()
        {
            if (Input.touchCount == 2)
            {
                var firstTouch = Input.touches[0];
                var secondTouch = Input.touches[1];
                var distance = Vector2.Distance(firstTouch.position, secondTouch.position);
                float sign = (distance > previousPinchDistance) ? -1 : 1;


                var zoomChange = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude;
                Debug.Log("Distance: " + distance + " " + " Zoom Change: " + zoomChange);
                camera.orthographicSize += zoomChange * Time.deltaTime * settings.ZoomSpeed * sign;

                previousPinchDistance = distance;


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
                    var dragSpeed = settings.DragSpeed * Mathf.Clamp01(camera.orthographicSize / settings.MaxCameraZoom);
#if UNITY_EDITOR
                    var movement = -touch.deltaPosition * dragSpeed * Time.deltaTime * 10;
#else
                    var movement = -touch.deltaPosition * dragSpeed * Time.deltaTime;
#endif

                    //Debug.Log(movement);
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
