using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class Enemy : MonoBehaviour, iApplicableDamaged, canExplode
{
    bool isAlive = true;
    bool canMoveForward=false;
    [SerializeField]
    protected int maxHP;
    protected int HP;
    float speed = 1.0f;
    [SerializeField]
    GameObject explosionEffect;
    AudioSource explosionSound;



    // Start is called before the first frame update
    protected void Start()
    {
        HP = maxHP;
        explosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    void moveForward()
    {
        if(canMoveForward==true)
        {
            transform.Translate(this.gameObject.transform.forward * speed);
        }
    }

    public void damaged(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            die();
        }

    }

    public void die()
    {
        
        Instantiate(explosionEffect, transform.position + new Vector3(0f, 5f, 0f), transform.rotation);
        Destroy(gameObject);
    }

    public void checkAlive()
    {
        
    }

    void canExplode.playEffect()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
