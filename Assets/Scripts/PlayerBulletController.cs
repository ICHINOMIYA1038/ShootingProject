using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;
using Cinemachine;

public class PlayerBulletController : LaunchBulletController
{
    public GameObject launchPositionRight;
    public GameObject launchPositionLeft;
    public GameObject focusCamera;
    public AudioSource launcherSE;

    public void Start()
    {
        launcherSE = GetComponent<AudioSource>();
    }
    
    public new void launchRight()
    {
        if (canLaunch == true && Input.GetAxis("Focus") == 0)
        {
            Instantiate(bulletPrefab, launchPositionRight.transform.position, launchPositionRight.transform.rotation);
            launcherSE.PlayOneShot(launcherSE.clip);
            span = defaultSpan;
            canLaunch = false;
        } else if (canLaunch == true && Input.GetAxis("Focus")!=0)
        {
            Vector3 cameraCenter = focusCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, focusCamera.GetComponent<Camera>().nearClipPlane + 3f));
            Instantiate(bulletPrefab, cameraCenter,focusCamera.transform.rotation);
            launcherSE.PlayOneShot(launcherSE.clip);
            span = defaultSpan;
            canLaunch = false;
        }
    }
    public new void launchLeft()
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
            Vector3 cameraCenter = focusCamera.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, focusCamera.GetComponent<Camera>().nearClipPlane + 3f));
            Instantiate(bulletPrefab, cameraCenter, focusCamera.transform.rotation);
            launcherSE.PlayOneShot(launcherSE.clip);
            span = defaultSpan;
            canLaunch = false;
        }

    }
}
