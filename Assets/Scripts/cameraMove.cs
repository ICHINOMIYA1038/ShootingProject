using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

///カメラを動かす処理
///実際にはアニメーションを再生しているだけ
public class cameraMove : MonoBehaviour
{
    Animation animation1;
    CinemachineVirtualCamera vmabove;
    [SerializeField]
    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        animation1 = this.GetComponent<Animation>();
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
        animation1.Play();
    }
}
