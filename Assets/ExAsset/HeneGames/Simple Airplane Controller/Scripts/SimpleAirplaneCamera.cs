using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace HeneGames.Airplane
{
    public class SimpleAirplaneCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SimpleAirPlaneController airPlaneController;
        [SerializeField] private CinemachineVirtualCamera camera;
        [Header("Camera values")]
        [SerializeField] private float cameraDefaultFov = 60f;
        [SerializeField] private float cameraTurboFov = 40f;
        [SerializeField]
        gameManager gameManager;

        private void Start()
        {
            //Lock and hide mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            CameraFovUpdate();
        }

        private void CameraFovUpdate()
        {
            //Turbo
            if(!airPlaneController.PlaneIsDead())
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeCameraFov(cameraTurboFov);
                }
                else
                {
                    ChangeCameraFov(cameraDefaultFov);
                }
            }
        }

        public void ChangeCameraFov(float _fov)
        {
            float _deltatime = Time.deltaTime * 100f;
            camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, _fov, 0.05f * _deltatime);
        }
    }
}