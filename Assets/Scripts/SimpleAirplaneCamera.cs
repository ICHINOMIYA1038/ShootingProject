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
        [SerializeField] private CinemachineVirtualCamera focusCamera;
        [Header("Camera values")]
        [SerializeField] private float cameraDefaultFov = 60f;
        [SerializeField] private float cameraTurboFov = 80f;
        [SerializeField]
        gameManager gameManager;
        [SerializeField]
        GameObject mapcanvas = null;
        GameObject focusCanvas;
        [SerializeField]
        AudioSource turboSE;
        bool soundNow = false;

        private void Start()
        {
            //Lock and hide mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mapcanvas = GameObject.Find("mapCanvas");
            focusCanvas = GameObject.Find("focusCanvas");
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
                if (Input.GetAxis("Focus")!=0)
                {
                    ChangeCameraFov(cameraTurboFov);
                    camera.gameObject.SetActive(false);
                    focusCamera.gameObject.SetActive(true);
                    mapcanvas.SetActive(false);
                    focusCanvas.SetActive(true);
                    airPlaneController.yawSpeed = 5f;
                    airPlaneController.pitchSpeed = 5f;
                    airPlaneController.rollSpeed = 5f;
                }
                else if (Input.GetAxis("Turbo") != 0)
                {
                    ChangeCameraFov(cameraDefaultFov);
                    focusCamera.gameObject.SetActive(false);
                    camera.gameObject.SetActive(true);
                    mapcanvas.SetActive(true);
                    focusCanvas.SetActive(false);
                    float _deltatime = Time.deltaTime * 100f;
                    camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, cameraDefaultFov + 20f, 0.05f * _deltatime);
                    airPlaneController.yawSpeed =80f;
                    airPlaneController.pitchSpeed = 20f;
                    airPlaneController.rollSpeed = 30f;
                }
                else
                {
                    float _deltatime = Time.deltaTime * 100f;
                    camera.m_Lens.FieldOfView = Mathf.Lerp(camera.m_Lens.FieldOfView, cameraDefaultFov, 0.05f * _deltatime);
                    ChangeCameraFov(cameraDefaultFov);
                    focusCamera.gameObject.SetActive(false);
                    camera.gameObject.SetActive(true);
                    mapcanvas.SetActive(true);
                    focusCanvas.SetActive(false);
                    airPlaneController.yawSpeed = 80f;
                    airPlaneController.pitchSpeed = 20f;
                    airPlaneController.rollSpeed = 30f;

                }

                if (Input.GetAxis("Turbo") != 0)
                {
                    if (!soundNow)
                    {
                        soundNow = true;
                        turboSE.Play();
                    }
                    
                }
                else
                {
                    soundNow = false;
                    turboSE.Stop();
                }
            }
        }

        public void ChangeCameraFov(float _fov)
        {
            float _deltatime = Time.deltaTime * 100f;
            focusCamera.m_Lens.FieldOfView = Mathf.Lerp(focusCamera.m_Lens.FieldOfView, _fov, 0.05f * _deltatime);
        }
    }
}