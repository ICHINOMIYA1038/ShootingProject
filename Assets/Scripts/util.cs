using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace util
{
    public interface iApplicableDamaged
    { 
        void damaged(int amount);
        void die();
        void checkAlive();
    }

    public interface iCanDamage
    {
        void damage(int damageAmount, iApplicableDamaged target);

    }

    public interface canExplode
    {
        void playEffect();

    }

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