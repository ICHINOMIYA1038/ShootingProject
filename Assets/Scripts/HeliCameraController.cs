using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


/// <summary>
/// プレイヤーの回転(カメラ操作)を制御する
/// </summary>
public class HeliCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHeliController playerController;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineVirtualCamera focusCamera;
    [SerializeField] GameManager gameManager;

    [Header("Sound")]
    [SerializeField] AudioSource turboSE;
    bool soundNow = false;

    private float cameraFocusFov = 5f;
    private float cameraDefaultFov = 40f;
    private float cameraTurboFov = 60f;

    [Header("Canvas")]
    [SerializeField]
    GameObject mapcanvas;
    [SerializeField]
    GameObject focusCanvas;

    

    private void Start()
    {
        //Lock and hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

    }



    public void SetCameraFocus()
    {
        ChangeCameraFov(cameraFocusFov);
        mainCamera.gameObject.SetActive(false);
        focusCamera.gameObject.SetActive(true);
        mapcanvas.SetActive(false);
        focusCanvas.SetActive(true);
        playerController.yawSpeed = 5f;
        playerController.pitchSpeed = 5f;
        playerController.rollSpeed = 5f;
    }

    public void SetCameraTurboWithoutFocus()
    {
        ChangeCameraFov(cameraDefaultFov);
        focusCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        mapcanvas.SetActive(true);
        focusCanvas.SetActive(false);
        float _deltatime = Time.deltaTime * 100f;
        mainCamera.m_Lens.FieldOfView = Mathf.Lerp(mainCamera.m_Lens.FieldOfView, cameraTurboFov, 0.05f * _deltatime);
        playerController.yawSpeed = playerController.defaultYawSpeed;
        playerController.pitchSpeed = playerController.defaultPitchSpeed;
        playerController.rollSpeed = playerController.defaultRollSpeed;
    }

    public void SetCameraDefault()
    {
        float _deltatime = Time.deltaTime * 100f;
        mainCamera.m_Lens.FieldOfView = Mathf.Lerp(mainCamera.m_Lens.FieldOfView, cameraDefaultFov, 0.05f * _deltatime);
        ChangeCameraFov(cameraDefaultFov);
        focusCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        mapcanvas.SetActive(true);
        focusCanvas.SetActive(false);
        playerController.yawSpeed = playerController.defaultYawSpeed;
        playerController.pitchSpeed = playerController.defaultPitchSpeed;
        playerController.rollSpeed = playerController.defaultRollSpeed;
    }

    public void setTurbo()
    {
        if (!soundNow)
        {
            soundNow = true;
            turboSE.Play();
        }
    }

    public void finishTurbo()
    {
        soundNow = false;
        turboSE.Stop();
    }

    public void ChangeCameraFov(float _fov)
    {
        float _deltatime = Time.deltaTime * 100f;
        focusCamera.m_Lens.FieldOfView = Mathf.Lerp(focusCamera.m_Lens.FieldOfView, _fov, 0.05f * _deltatime);
    }
}
