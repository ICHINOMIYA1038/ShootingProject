using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    GameObject targetCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        targetCamera = this.gameObject;
        
        Vector3 position = new Vector3(0f,0f,0f);
        position.x = target.transform.position.x;
        position.z = target.transform.position.z-100f;
        position.y = targetCamera.transform.position.y;
        targetCamera.transform.position = position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(0f, 0f, 0f);
        position.x = target.transform.position.x;
        position.z = target.transform.position.z - 100f;
        position.y = targetCamera.transform.position.y;
        targetCamera.transform.position = position;
    }
}
