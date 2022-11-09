using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ミサイルクラス、敵味方共通の抽象クラス
/// </summary>
abstract public class Bullet : MonoBehaviour
{
    [SerializeField]
    bool hasLifeTime;
    float lifeTime;
    [SerializeField]
    float defaultLifeTime;
    bool canMoveForward = true;
    float speed = 8;
    // Start is called before the first frame update
    protected void Start()
    {
        lifeTime = defaultLifeTime;
    }

    // Update is called once per frame
    protected void Update()
    {
        moveForward();
        updateLifetime();
    }

    protected void moveForward()
    {
        if (canMoveForward == true)
        {
            transform.position = transform.position + transform.forward * speed;
           
        }
    }

    protected void updateLifetime()
    {
        if(hasLifeTime)
        {
            lifeTime -=  1*Time.deltaTime;
            
            if(lifeTime<0)
            {
                die();
            }
        }
    }

    protected void die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
