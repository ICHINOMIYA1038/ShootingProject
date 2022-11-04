using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        if (!target)
        {
            this.gameObject.transform.LookAt(target.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            this.gameObject.transform.LookAt(target.transform);
        }
    }
}
