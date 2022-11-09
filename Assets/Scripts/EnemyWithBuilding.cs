using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

//建物から攻撃してくる敵のスクリプト
public class EnemyWithBuilding : Enemy, iApplicableDamaged
{
    private Animator animator;
    private GameObject target;
    [Header("キャラクター")]
    [SerializeField]
    GameObject person;
    [Header("探知範囲")]
    [SerializeField]
    private int detectRange;
    private LaunchToPlayer toplayerScript;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        animator = person.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        toplayerScript = GetComponentInChildren<LaunchToPlayer>();

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        detect();
    }

    //プレイヤーを検知するスクリプト
    //detectRange以内にプレイヤーがいる時に処理をする。
    //検知した場合、プレイヤーの方を向き、アニメーションを再生する。
    void detect()
    {

        Vector3 targetVector = target.transform.position - transform.position;
        if (targetVector.magnitude < detectRange)
        {
            person.transform.LookAt(target.transform);
            Vector3 ang = person.transform.localEulerAngles;
            ang.z = 0;
            ang.x = 0;
            person.transform.localEulerAngles = ang;
            animator.SetTrigger("detect");
            toplayerScript.setIsDetecting(true);
        }
        else
        {
            //検知指定ない場合のアニメーションを設定する。
            animator.SetTrigger("not detect");
            toplayerScript.setIsDetecting(false);
        }

    }
}
