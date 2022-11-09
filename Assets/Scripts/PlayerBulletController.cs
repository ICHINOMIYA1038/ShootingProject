using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;
using Cinemachine;

//プレイヤーのミサイルの発射制御
//LaunchBulletControllerを継承
//入力処理に関してはInputManagerで処理
//ミサイル本体の処理はPlayerBulletで処理
public class PlayerBulletController : LaunchBulletController
{
    [Header("発射点")]
    [SerializeField] private GameObject launchPositionRight;
    [SerializeField] private GameObject launchPositionLeft;
    [Header("References")]
    [SerializeField] private GameObject focusCamera;

    private AudioSource launcherSE;

    public void Start()
    {
        launcherSE = GetComponent<AudioSource>();
    }

    //右のミサイルラウンチャーから発射
    public void launchRight()
    {
        if (canLaunch == true && Input.GetAxis("Focus") == 0)
        {
            Instantiate(bulletPrefab, launchPositionRight.transform.position, launchPositionRight.transform.rotation);
            launcherSE.PlayOneShot(launcherSE.clip);
            span = defaultSpan;
            canLaunch = false;
        } else if (canLaunch == true && Input.GetAxis("Focus")!=0)
        {
            //Focusの場合は、中心からミサイルを発射
            Vector3 cameraCenter = focusCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, focusCamera.GetComponent<Camera>().nearClipPlane + 3f));
            Instantiate(bulletPrefab, cameraCenter,focusCamera.transform.rotation);
            launcherSE.PlayOneShot(launcherSE.clip);
            span = defaultSpan;
            canLaunch = false;
        }
    }

    //左のミサイルラウンチャーから発射
    public void launchLeft()
    {
        if (canLaunch == true && Input.GetAxis("Focus") == 0)
        {
            Instantiate(bulletPrefab, launchPositionLeft.transform.position, launchPositionLeft.transform.rotation);
            span = defaultSpan;
            canLaunch = false;
            launcherSE.PlayOneShot(launcherSE.clip);
        }
        else if (canLaunch == true && Input.GetAxis("Focus") != 0)
        {
            //Focusの場合は、中心からミサイルを発射
            Vector3 cameraCenter = focusCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, focusCamera.GetComponent<Camera>().nearClipPlane + 3f));
            Instantiate(bulletPrefab, cameraCenter, focusCamera.transform.rotation);
            launcherSE.PlayOneShot(launcherSE.clip);
            span = defaultSpan;
            canLaunch = false;
        }

    }
}
