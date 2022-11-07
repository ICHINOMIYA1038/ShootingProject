using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraMove : MonoBehaviour
{
    Animation animation;
    CinemachineVirtualCamera vmabove;
    [SerializeField]
    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        animation = this.GetComponent<Animation>();
        vmabove = this.GetComponent<CinemachineVirtualCamera>();
        StartCoroutine("animate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator animate()
    {
        yield return new WaitForSeconds(waitTime);
        animation.Play();
    }
}
