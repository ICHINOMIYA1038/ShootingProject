using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace util
{
    //ダメージを受ける処理のインターフェース
    public interface iApplicableDamaged
    { 
        void damaged(int amount);
        void die();
    }

    //ダメージを与えることを保証するインターフェース
    public interface iCanDamage
    {
        void damage(int damageAmount, iApplicableDamaged target);

    }

    //爆発のためのインターフェース
    public interface canExplode
    {
        void playEffect();

    }

    //弾を発射するためのインターフェース
    public abstract class LaunchBulletController : MonoBehaviour
    {
        
        [SerializeField]
        GameObject positionTarget;
        [SerializeField]
        GameObject rotationTarget;
        [SerializeField]
        protected GameObject bulletPrefab;
        public Vector3 launchPosition;
        public Quaternion targetRotation;
        protected float span;
        [SerializeField]
        protected float defaultSpan;
        public bool canLaunch = true;

        private void Start()
        {
            span = defaultSpan;
            launchPosition = positionTarget.transform.position;
            targetRotation = rotationTarget.transform.rotation;
        }
        bool getCanLaunch()
        {
            return canLaunch;
        }
        public void launch()
        {
            if (canLaunch == true)
            {
                Instantiate(bulletPrefab, launchPosition, targetRotation);
                span = defaultSpan;
                canLaunch = false;
            }
        }
        

        public void updateSpan()
        {
            if (canLaunch == false)
            {
                span -= Time.deltaTime;
                if (span < 0)
                {
                    canLaunch = true;
                }
            }
        }
        private void Update()
        {
            updateSpan();
            launchPosition = positionTarget.transform.position;
            targetRotation = rotationTarget.transform.rotation;
        }
    }
    
}