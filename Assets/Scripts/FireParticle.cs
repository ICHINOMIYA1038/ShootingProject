using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炎のエフェクトクラス
/// </summary>
public class FireParticle : MonoBehaviour
{
    GameObject[] effect;
    ParticleSystem[] particle;
    // Start is called before the first frame update
    void Start()
    {
        effect = new GameObject[5];
        particle = new ParticleSystem[5];
        for (int i = 0; i < 5; i++)
        {
            effect[i] = transform.GetChild(i).gameObject;
            particle[i] = effect[i].GetComponent<ParticleSystem>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            particle[i].Play();
        }
    }
}
