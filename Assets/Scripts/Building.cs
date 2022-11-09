using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;


/// <summary>
/// 建物のクラス
/// </summary>
public class Building : MonoBehaviour ,iApplicableDamaged
{
   
    protected int HP;
    [SerializeField]
    protected int maxHP;
    [SerializeField]
    GameObject explosionEffect;

    public void checkAlive()
    {
        
    }

    public void damaged(int amount)
    {
       HP -= amount;
        if(HP <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Instantiate(explosionEffect, transform.position + new Vector3(0f, 5f, 0f), transform.rotation);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    protected void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
