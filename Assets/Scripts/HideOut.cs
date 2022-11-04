using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class HideOut : Building,iApplicableDamaged
{
    [SerializeField]
    gameManager manager;
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

    public new void die()
    {
        manager.sceneChange(gameManager.CLEAR_SCENE);
        Debug.Log("die");
        base.die();
    }

    public new void damaged(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            die();
        }
        Debug.Log(hPGaugeController.panelWidth);
        hPGaugeController.panelWidth = ((float)HP / (float)maxHP) * hPGaugeController.panelWidth;
    }
}
