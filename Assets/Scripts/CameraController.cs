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
        private Camera camera;
        [SerializeField]
        private CameraSettings settings;

        private bool isPanEnabled = true;
        private bool isZoomEnabled = true;

        private bool isDragging = false;
        private bool isZooming = false;
        private float previousPinchDistance;
        private Vector2 dragDeceleration;
        private float decelerationTime = 0;


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

                if (isZooming)
                {
                    if (camera.orthographicSize >= settings.MaxCameraZoom + settings.MaxZoomTolerance)
                        camera.orthographicSize = settings.MaxCameraZoom + settings.MaxZoomTolerance;
                    if (camera.orthographicSize <= settings.MinCameraZoom - settings.MinZoomTolerance)
                        camera.orthographicSize = settings.MinCameraZoom - settings.MinZoomTolerance;
                } else
                {
                    if (camera.orthographicSize > settings.MaxCameraZoom)
                        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, settings.MaxCameraZoom, settings.ZoomCorrectionTime);
                    if (camera.orthographicSize < settings.MinCameraZoom)
                        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, settings.MinCameraZoom, settings.ZoomCorrectionTime);
                }
            }

        }

        private void TryZoom()
        {
            if (Input.touchCount == 2)
            {
                isZooming = true;
                var firstTouch = Input.touches[0];
                var secondTouch = Input.touches[1];
                var distance = Vector2.Distance(firstTouch.position, secondTouch.position);
                float sign = (distance > previousPinchDistance) ? -1 : 1;


                var zoomChange = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude;
                camera.orthographicSize += zoomChange * Time.deltaTime * settings.ZoomSpeed * sign;

                previousPinchDistance = distance;
            } else
            {
                isZooming = false;
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
                    var movement = 10 * dragSpeed * Time.deltaTime * -touch.deltaPosition;
#else
                    var movement = dragSpeed * Time.deltaTime * -touch.deltaPosition;
#endif
                    camera.transform.Translate(movement.x, movement.y, 0);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    decelerationTime = 0;
                    dragDeceleration = -touch.deltaPosition;
                    isDragging = false;
                }
            }
            else if (Input.touchCount == 0)
            {
                Decelerate();
            }
            
        }

        private void Decelerate()
        {
            if (decelerationTime < settings.DecelerationTime)
            {
                Vector3 speed = Vector3.zero;
                var current = camera.transform.position;
                var dragDecelerationModifier = new Vector3(dragDeceleration.x * ScreenDimensionModifier().x,
                                                           dragDeceleration.y * ScreenDimensionModifier().y);

                var destination = current + (dragDecelerationModifier * settings.DecelerationFriction
                                            * Mathf.Clamp01(1 - decelerationTime / settings.DecelerationTime));

                camera.transform.position = Vector3.SmoothDamp(current, destination, ref speed, settings.DecelerationTime);
                decelerationTime += Time.deltaTime;
            }
        }

        private Vector2 ScreenDimensionModifier()
        {
            Vector2 val= Vector2.zero;
            if (Screen.width < Screen.height)
            {
                var screenDifference = Mathf.Clamp01((float)Screen.width / (float)Screen.height);
                val.x = Mathf.Lerp(settings.MinScreenDimensionModifier, settings.MaxScreenDimensionModifier, screenDifference);
                val.y = settings.MinScreenDimensionModifier;
            } else
            {
                var screenDifference = Mathf.Clamp01((float)Screen.width / (float)Screen.height);
                val.x = settings.MinScreenDimensionModifier;
                val.y = Mathf.Lerp(settings.MinScreenDimensionModifier, settings.MaxScreenDimensionModifier, screenDifference);
            }
            return val;
        }
    }
}
