using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Animator animator;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private int detectRange;
    [SerializeField]
    private int HP { get; set; } = 40;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        detect();
    }

    void die()
    {
        animator.SetTrigger("die");
    }

    void damaged(int num)
    {
        this.HP = this.HP - num;
        if(HP<0)
        {
            die();
        }
    }

    void detect()
    {
       
        Vector3 targetVector = target.transform.position - transform.position;
        if(targetVector.magnitude<detectRange)
        {
            //Debug.Log("detect");
            transform.LookAt(target.transform);
            Vector3 ang = this.transform.localEulerAngles;
            ang.z = 0;
            ang.x = 0;
            transform.localEulerAngles = ang;
            animator.SetTrigger("detect");
        }
        
    }
}
