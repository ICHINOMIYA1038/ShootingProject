using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class EnemyWithBuilding : Enemy, iApplicableDamaged, canExplode
{
    Animator animator;
    GameObject target;
    [SerializeField]
    GameObject person;
    [SerializeField]
    private int detectRange;
    toPlayer toplayerScript;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        animator = person.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        toplayerScript = GetComponentInChildren<toPlayer>();

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        detect();
    }

    void detect()
    {

        Vector3 targetVector = target.transform.position - transform.position;
        if (targetVector.magnitude < detectRange)
        {
            //Debug.Log("detect");
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
            animator.SetTrigger("not detect");
            toplayerScript.setIsDetecting(false);
        }

    }
}
