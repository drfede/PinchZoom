using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinchZoom
{
    public class UISpriteScaler : MonoBehaviour
    {
        [SerializeField]
        private CameraController cameraController;

        [SerializeField]
        private float startingScale = 1.0f;



        // Start is called before the first frame update
        void Start()
        {
            if (cameraController != null)
            {
                cameraController.onZoomCallback.AddListener(DynamicScale);
            }
        }

        private void DynamicScale(float newScale)
        {
            var newSpriteScale = newScale * startingScale / cameraController.StartingCameraScale;
            transform.localScale = new Vector3(newSpriteScale, newSpriteScale, newSpriteScale);
        }

        private void OnDestroy()
        {
            cameraController.onZoomCallback.RemoveListener(DynamicScale);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}