using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class PlayerBulletController : LaunchBulletController
{
    public GameObject launchPositionRight;
    public GameObject launchPositionLeft;
    public new void launchRight()
    {
        if (canLaunch == true)
        {
            Instantiate(bulletPrefab, launchPositionRight.transform.position, launchPositionRight.transform.rotation);
           
            span = defaultSpan;
            canLaunch = false;
        }
    }
    public new void launchLeft()
    {
        if (canLaunch == true)
        {
            Instantiate(bulletPrefab, launchPositionLeft.transform.position, launchPositionLeft.transform.rotation);
            span = defaultSpan;
            canLaunch = false;
        }
    }
}
