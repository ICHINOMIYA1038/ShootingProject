using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;
using System;

/// <summary>
/// プレイヤーを制御するクラス
/// ダメージ、速度、衝突判定を制御
/// 回転(カメラ制御)に関しては、HeliCameraControllerで制御
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerHeliController : MonoBehaviour, iApplicableDamaged
{
    #region Private variables

    //maxspeedは速度を制御するための目標値。この値になるように、速度を制御する。
    private float maxSpeed = 0.6f;
    private float currentSpeed;
    private bool isDead;
    public bool isTurbo { get; set; } = false;
    private Rigidbody rb;

    //onStartで設定
    private GameObject gamemanager;
    private GameManager manager;
    private AudioSource impuctAudioSource;
    [SerializeField] AudioClip impuctSoundClip;
    #endregion+

    private int HP { get; set; }

    public float yawSpeed{ get; set; } = 50f;
    public float pitchSpeed{ get; set; } = 50f;
    public float rollSpeed{ get; set; } = 50f;

    [Space(10)]
    [Header("角度制限")]
    [SerializeField]
    bool rotationRestrict = true;
    [SerializeField]
    float rotationRangeX = 40f;
    [SerializeField]
    float rotationRangeZ = 60f;

    [Space(10)]
    [Header("移動速度")]
    [Range(5f, 100f)]
    [SerializeField] private float defaultSpeed = 20f;

    [Range(10f, 200f)]
    [SerializeField] private float turboSpeed = 50f;

    [Range(0.1f, 50f)]
    [SerializeField] private float accelerating = 10f;

    [Range(0.1f, 50f)]
    [SerializeField] private float deaccelerating = 5f;

    [Space(10)]
    [Header("回転速度")]
    [Range(5f, 500f)]
    [SerializeField] public float defaultYawSpeed = 50f;

    [Range(5f, 500f)]
    [SerializeField] public float defaultPitchSpeed = 100f;

    [Range(5f, 500f)]
    [SerializeField] public float defaultRollSpeed = 200f;

    public bool isMovie = true;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        manager = gamemanager.GetComponent<GameManager>();
        this.HP = manager.HP;

        //maxspeedをデフォルトに設定。
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
        if (!isDead && manager.getCurrentScene() == GameManager.MAIN_SCENE)
        {
            if (isMovie == true)
            {
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }
            else
            {
                Movement();
            }
        }
        Debug.Log(checkCanRotate());
    }

    #region Movement



    private void Movement()
    {
        //移動の処理
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        //回転の処理

        if(rotationRestrict)
        {
            transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal") * rollSpeed * Time.deltaTime);
            if (!checkCanRotate())
            {
                transform.Rotate(Vector3.forward * Input.GetAxis("Horizontal") * rollSpeed * Time.deltaTime);
            }
                transform.Rotate(Vector3.right * Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime);
            if (!checkCanRotate())
            {
                transform.Rotate(Vector3.right * -Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime);
            }
            transform.Rotate(Vector3.up * Input.GetAxis("anotherHorizontal") * yawSpeed * Time.deltaTime);
            if (!checkCanRotate())
            {
                transform.Rotate(Vector3.up * Input.GetAxis("anotherHorizontal") * yawSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal") * rollSpeed * Time.deltaTime);
            transform.Rotate(Vector3.right * Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up * Input.GetAxis("anotherHorizontal") * yawSpeed * Time.deltaTime);
        }



        //加速処理
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += accelerating * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deaccelerating * Time.deltaTime;
        }

        //ターボしているかどうかの判定。InputManagerによってisTurboは決定される。
        if (isTurbo)
        {
            //最大速度をターボの速度にする
            maxSpeed = turboSpeed;
        }
        else
        {
            //最大速度をデフォルトに戻す
            maxSpeed = defaultSpeed;
        }
    }

    #endregion


    #region Private methods


    private void Crash()
    {
        //重力を有効にして落下させる
        rb.isKinematic = false;
        rb.useGravity = true;

        //Kill player
        isDead = true;
    }

    //衝突の判定
    //オブジェクトの名前や、オブジェクトのタグによって判定している。
    //プレイヤー自身の弾や機体に当たった時に判定されるのを防ぐ。
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Map1") && isDead == false)
        {
            die();
        }
        if (collision.gameObject.tag.Equals("Wall") && isDead == false)
        {
            die();
        }
        if (collision.gameObject.tag.Equals("anti-air-gun") && isDead == false)
        {
            die();
        }
        if (collision.gameObject.tag.Equals("machinegun") && isDead == false)
        {
            die();
        }
        if (collision.gameObject.tag.Equals("Enemy") && isDead == false)
        {
            die();
        }
        //効果音
        impuctAudioSource.PlayOneShot(impuctSoundClip);


    }
    #endregion

    #region Variables

    //ターボ時の速度に対する現在の速度の割合
    public float PercentToMaxSpeed()
    {
        float _percentToMax = currentSpeed / turboSpeed;

        return _percentToMax;
    }

    //プレイヤーが死んだいるかどうかの判定
    public bool PlaneIsDead()
    {
        return isDead;
    }

    //ターボしているかどうかの判定
    public bool UsingTurbo()
    {
        if (maxSpeed == turboSpeed)
        {
            return true;
        }

        return false;
    }

    //現在の速度を返す
    public float CurrentSpeed()
    {
        return currentSpeed;
    }

    //攻撃を受けたときの処理。
    //canDamageのInterfaceから呼び出される。
    public void damaged(int amount)
    {
        this.HP -= amount;
        manager.updateParam("HP", amount);
        if (!isDead && this.HP < 1)
        {
            die();
        }
    }

    //プレイヤーが倒された時の処理。
    //クラッシュした後、画面を遷移する。
    public void die()
    {
        Crash();
        //if文でゲームクリア時には発生しないように処理。
        if (manager.getCurrentScene() == GameManager.MAIN_SCENE)
        {
            manager.sceneChange(GameManager.GAMEOVER_SCENE);
        }

    }

    public bool checkCanRotate()
    {
        float anglez = transform.localEulerAngles.z;
        float anglex = transform.localEulerAngles.x;

        if (anglex > 180)
        {
            anglex = anglex - 360;
        }
        if (anglez > 180)
        {
            anglez = anglez - 360;
        }
        if (!(Math.Abs(anglez) < rotationRangeZ))
        {
            return false;
        }
        if (!(Math.Abs(anglex) < rotationRangeX))
        {
            return false;
        }
        return true;
    }

}
        #endregion
    