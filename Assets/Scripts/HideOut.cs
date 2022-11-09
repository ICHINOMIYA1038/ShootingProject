using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

///<summary>
///目的となる敵の拠点のスクリプト
///建物のクラスを継承
///</summary>
public class HideOut : Building,iApplicableDamaged
{
    [SerializeField]
    GameManager manager;
    HPGaugeController hPGaugeController; 
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        hPGaugeController = GetComponentInChildren<HPGaugeController>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();    
    }

    ///<summary>
    ///死ぬ時の処理
    /// </summary>
    public new void die()
    {
        if(manager.getCurrentScene()==GameManager.MAIN_SCENE)
        {
            manager.sceneChange(GameManager.CLEAR_SCENE);
        }
        Debug.Log("die");
        base.die();
    }

    ///<summary>
    ///ダメージ処理
    /// </summary>
    public new void damaged(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            die();
        }
        hPGaugeController.panelWidth = ((float)HP / (float)maxHP) * hPGaugeController.panelWidth;
    }
}
