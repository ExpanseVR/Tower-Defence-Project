using GameDevHQ.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevHQ.Scripts.Managers
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField]
        private float _cameraPanSpeed;

        [SerializeField]
        private float _cameraZoomSpeed;

        [SerializeField]
        private float _maxZoomIn;

        [SerializeField]
        private float _maxZoomOut;

        [SerializeField]
        private Camera cam;

        private void Update()
        {
            //get player keyboard input WSAD
            KeyBoardInput();

            //get player mouse scroll input
            MouseScrollInput();

            //detect if mouse moves to bounds of screen
            //move camera in direction of bound

            //camera cannot move pass camera bounding box

        }

        private void KeyBoardInput()
        {
            float inputHorizontal = Input.GetAxis("Horizontal");
            float inputVertical = Input.GetAxis("Vertical");

            //move camera vertically and horizontally based on input and speed
            var cameraDirection = new Vector3(inputHorizontal, 0, inputVertical) * _cameraPanSpeed * Time.deltaTime;
            transform.Translate(cameraDirection);
        }

        private void MouseScrollInput()
        {
            float mouseScroll = Input.mouseScrollDelta.y;
            if (mouseScroll != 0)
            {
                //zoom in and out based on scroll
                Vector3 cameraZoom = cam.transform.forward * mouseScroll * _cameraZoomSpeed * Time.deltaTime;
                //restrctin zoom in and out levels
                Vector3 newCameraPos = transform.position + cameraZoom;
                if (newCameraPos.y > _maxZoomIn && newCameraPos.y < _maxZoomOut)
                    transform.position = newCameraPos;
            }
        }
    }
}