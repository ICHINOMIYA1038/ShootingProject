using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class EnemyBullet : Bullet,iCanDamage
{
    public void damage(int damageAmount, iApplicableDamaged target)
    {
        target.damaged(1);
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            iApplicableDamaged target;
            target = collision.gameObject.GetComponent<iApplicableDamaged>();
            if (target != null)
            {
                damage(1, target);
               
            }
            Destroy(this.gameObject);
        }
        
    }
}
