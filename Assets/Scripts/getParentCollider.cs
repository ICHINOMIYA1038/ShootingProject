using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getParentCollider : MonoBehaviour
{
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        parent = this.gameObject.transform.root.gameObject;
        Building buildingScript = parent.GetComponent<Building>();
    }
}
