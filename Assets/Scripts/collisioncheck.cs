using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisioncheck : MonoBehaviour
{
    GameObject effect1Object;
    ParticleSystem effect1;
    // Start is called before the first frame update
    void Start()
    {
        effect1Object = transform.GetChild(0).gameObject;
        effect1 = effect1Object.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(effect1 != null)
        {
            effect1.Play();
        }
    }
}
