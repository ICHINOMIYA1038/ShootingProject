using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setLifeTime : MonoBehaviour
{
    public float LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        LifeTime = 0;
        StartCoroutine("startLife");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerable startLife()
    {
        yield return new WaitForSeconds(LifeTime);
    }
}
