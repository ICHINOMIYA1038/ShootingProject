using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    [Range(0, 2)]
    int direction;
    [SerializeField]
    GameObject player;
    float distance;
    [SerializeField]
    float detectDistance;
    MeshRenderer meshrenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0)
        {
            distance = this.transform.position.x - player.transform.position.x;
            if (distance < 0)
            {
                distance = -distance;
            }
        }
        if (direction == 1)
        {
            distance = this.transform.position.y - player.transform.position.y;
            if (distance < 0)
            {
                distance = -distance;
            }
        }
        if (direction == 2)
        {
            distance = this.transform.position.z - player.transform.position.z;
            if (distance < 0)
            {
                distance = -distance;
            }
        }

        if(distance < detectDistance)
        {
            Debug.Log(1 - distance / detectDistance);
            meshrenderer.material.SetColor("_Color", new Color(0.27f, 1f, 0f, (1 - distance / detectDistance)*1f));
        }
    }
}
