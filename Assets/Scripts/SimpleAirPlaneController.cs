using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

namespace HeneGames.Airplane
{
    [RequireComponent(typeof(Rigidbody))]
    public class SimpleAirPlaneController : MonoBehaviour, iApplicableDamaged
    {
        #region Private variables

        private List<SimpleAirPlaneCollider> airPlaneColliders = new List<SimpleAirPlaneCollider>();

        private float maxSpeed = 0.6f;
        private float currentYawSpeed;
        private float currentPitchSpeed;
        private float currentRollSpeed;
        private float currentSpeed;
        private float currentEngineLightIntensity;
        private float currentEngineSoundPitch;
        private bool planeIsDead;
        private Rigidbody rb;

        private GameObject gamemanager;
        private gameManager manager;
        AudioSource impuctAudioSource;
        [SerializeField]
        AudioClip impuctSoundClip;
        #endregion+

        private int HP { get; set; }
        private int moral { get; set; }


        [Header("Wing trail effects")]
        [Range(0.01f, 1f)]
        [SerializeField] private float trailThickness = 0.045f;

        [Header("Rotating speeds")]
        [Range(5f, 500f)]
        [SerializeField] public float yawSpeed = 50f;

        [Range(5f, 500f)]
        [SerializeField] public float pitchSpeed = 100f;

        [Range(5f, 500f)]
        [SerializeField] public float rollSpeed = 200f;

        [Header("Rotating speeds multiplers when turbo is used")]
        [Range(0.1f, 5f)]
        [SerializeField] private float yawTurboMultiplier = 0.3f;

        [Range(0.1f, 5f)]
        [SerializeField] private float pitchTurboMultiplier = 0.5f;

        [Range(0.1f, 5f)]
        [SerializeField] private float rollTurboMultiplier = 1f;

        [Header("Moving speed")]
        [Range(5f, 100f)]
        [SerializeField] private float defaultSpeed = 10f;

        [Range(10f, 200f)]
        [SerializeField] private float turboSpeed = 20f;

        [Range(0.1f, 50f)]
        [SerializeField] private float accelerating = 10f;

        [Range(0.1f, 50f)]
        [SerializeField] private float deaccelerating = 5f;

        [Header("Engine sound settings")]
        [SerializeField] private AudioSource engineSoundSource;

        [SerializeField] private float defaultSoundPitch = 1f;

        [SerializeField] private float turboSoundPitch = 1.5f;

        [Header("Engine propellers settings")]
        [Range(10f, 10000f)]
        [SerializeField] private float propelSpeedMultiplier = 100f;

        [SerializeField] private GameObject[] propellers;

        [Header("Turbine light settings")]
        [Range(0.1f, 20f)]
        [SerializeField] private float turbineLightDefault = 1f;

        [Range(0.1f, 20f)]
        [SerializeField] private float turbineLightTurbo = 5f;

        [SerializeField] private Light[] turbineLights;

        [Header("Colliders")]
        [SerializeField] private Transform crashCollidersRoot;

        private void Start()
        {
            gamemanager = GameObject.Find("GameManager");
            manager = gamemanager.GetComponent<gameManager>();
            this.HP = manager.HP;
            this.moral = manager.moral;


            //Setup speeds
            maxSpeed = defaultSpeed;
            currentSpeed = defaultSpeed;

            //Get and set rigidbody
            rb = GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

            //SetupColliders(crashCollidersRoot);
            impuctAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {

            //Airplane move only if not dead
            if (!planeIsDead&&manager.getCurrentScene()==gameManager.MAIN_SCENE)
            {
                Movement();

            }



            //Crash
            

            
        }

        IEnumerator setKinematic()
        {
            yield return new WaitForSeconds(3f);
            if(this.HP>0)
            rb.isKinematic = true;
        }

        #region Movement

        

        public void damageMoral(int damage)
        {
            this.moral -= damage;
            manager.updateParam("Moral", damage);
        }

        private void Movement()
        {
            //Move forward
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            //Rotate airplane by inputs
            transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal") * currentRollSpeed * Time.deltaTime);
            transform.Rotate(Vector3.right * Input.GetAxis("Vertical") * currentPitchSpeed * Time.deltaTime);

            //Rotate yaw
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up * currentYawSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(-Vector3.up * currentYawSpeed * Time.deltaTime);
            }

            //Accelerate and deacclerate
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += accelerating * Time.deltaTime;
            }
            else
            {
                currentSpeed -= deaccelerating * Time.deltaTime;
            }

            //Turbo
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Set speed to turbo speed and rotation to turbo values
                maxSpeed = turboSpeed;

                currentYawSpeed = yawSpeed * yawTurboMultiplier;
                currentPitchSpeed = pitchSpeed * pitchTurboMultiplier;
                currentRollSpeed = rollSpeed * rollTurboMultiplier;

                //Engine lights
                currentEngineLightIntensity = turbineLightTurbo;

                //Audio
                currentEngineSoundPitch = turboSoundPitch;
            }
            else
            {
                //Speed and rotation normal
                maxSpeed = defaultSpeed;

                currentYawSpeed = yawSpeed;
                currentPitchSpeed = pitchSpeed;
                currentRollSpeed = rollSpeed;

                //Engine lights
                currentEngineLightIntensity = turbineLightDefault;

                //Audio
                currentEngineSoundPitch = defaultSoundPitch;
            }
        }

        #endregion


        #region Private methods
       

        private void Crash()
        {
            //Set rigidbody to non cinematic
            rb.isKinematic = false;
            rb.useGravity = true;

            //Change every collider trigger state and remove rigidbodys
            for (int i = 0; i < airPlaneColliders.Count; i++)
            {
                airPlaneColliders[i].GetComponent<Collider>().isTrigger = false;
                Destroy(airPlaneColliders[i].GetComponent<Rigidbody>());
            }

            //Kill player
            planeIsDead = true;

            //Here you can add your own code...
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Equals("Map1")&&planeIsDead==false)
            {
                die();
            }
            if (collision.gameObject.tag.Equals("Wall") && planeIsDead == false)
            {
                die();
            }
            impuctAudioSource.PlayOneShot(impuctSoundClip);


        }
        #endregion

        #region Variables

        /// <summary>
        /// Returns a percentage of how fast the current speed is from the maximum speed between 0 and 1
        /// </summary>
        /// <returns></returns>
        public float PercentToMaxSpeed()
        {
            float _percentToMax = currentSpeed / turboSpeed;

            return _percentToMax;
        }

        public bool PlaneIsDead()
        {
            return planeIsDead;
        }

        public bool UsingTurbo()
        {
            if(maxSpeed == turboSpeed)
            {
                return true;
            }

            return false;
        }

        public float CurrentSpeed()
        {
            return currentSpeed;
        }

        public void damaged(int amount)
        {
            this.HP -= amount;
            manager.updateParam("HP", amount);
            if (!planeIsDead && this.HP < 1)
            {
                die();
            }
        }

        public void die()
        {
            Crash();
            if(manager.getCurrentScene()==gameManager.MAIN_SCENE)
            {
                manager.sceneChange(gameManager.GAMEOVER_SCENE);
            }
           
        }

        public void checkAlive()
        {
            
        }

        #endregion
    }
}