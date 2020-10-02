using GameDevHQ.Scripts.Utility;
using GameDevHQ.Scripts;
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
        private float _mouseBoarderSensitivity = 20f;

        [SerializeField]
        private Camera cam;

        private Vector3 _lastFrameDir;
        private bool _cameraBounds = false;

        private void Update()
        {
            //get player keyboard input WSAD
            KeyBoardInput();

            //get player mouse scroll input
            MouseScrollInput();

            //detect if mouse moves to bounds of screen
            MousePositionInput(); //ACTIVATE TO SHOW JON BUT ANNOYING IN DEV MODE
        }

        private void MousePositionInput()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 cameraDirection = new Vector3();

            if (mousePos.x < _mouseBoarderSensitivity)
            {
                cameraDirection = Vector3.left;
            }
            else if (mousePos.x > (Camera.main.pixelWidth - _mouseBoarderSensitivity))
            {
                cameraDirection = Vector3.right;
            }
            else if (mousePos.y < _mouseBoarderSensitivity)
            {
                cameraDirection = Vector3.back;
            }
            else if (mousePos.y > (Camera.main.pixelHeight - _mouseBoarderSensitivity))
            {
                cameraDirection = Vector3.forward;
            }
            //move camera in direction of bound
            if (cameraDirection != Vector3.zero)
            {
                MoveCamera(cameraDirection);
            }
        }

        private void KeyBoardInput()
        {
            float inputHorizontal = Input.GetAxis("Horizontal");
            float inputVertical = Input.GetAxis("Vertical");

            //move camera vertically and horizontally based on input and speed
            var cameraDirection = new Vector3(inputHorizontal, 0, inputVertical);
            if (cameraDirection != Vector3.zero)
                MoveCamera(cameraDirection);
        }

        private void MoveCamera(Vector3 cameraDirection)
        {
            transform.Translate(cameraDirection * _cameraPanSpeed * Time.deltaTime);

            //clamp x and z by level bounds
            float clampX = Mathf.Clamp(transform.position.x, -47f, -30f);
            float clampZ = Mathf.Clamp(transform.position.z, -11f, 3f);
            Vector3 clampedPos = new Vector3(clampX, transform.position.y, clampZ);
            transform.position = clampedPos;
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