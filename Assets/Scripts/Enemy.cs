using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

//敵キャラクターの派生元クラス
//ダメージを受けることを保証するインターフェースを実装
public class Enemy : MonoBehaviour, iApplicableDamaged
{
    //canMoveForwardがtrueの時、自動で前にすすむ。
    [Header("プロパティ設定")]
    [SerializeField]
    bool canMoveForward=false;
    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    protected int maxHP;
    protected int HP;
    [SerializeField]
    GameObject explosionEffect;

    protected void Start()
    {
        HP = maxHP;
        
    }

    protected void Update()
    {
        moveForward();
    }

    //canMoveForwardがtrueの時、自動で前にすすむ。
    void moveForward()
    {
        if(canMoveForward==true)
        {
            transform.Translate(this.gameObject.transform.forward * speed);
        }
    }

    //ダメージ処理
    public void damaged(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            die();
        }

    }

    //死ぬ時の処理
    public void die()
    {
        
        Instantiate(explosionEffect, transform.position + new Vector3(0f, 5f, 0f), transform.rotation);
        Destroy(gameObject);
    }

}
