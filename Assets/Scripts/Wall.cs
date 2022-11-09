using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが行動できる領域を制限するための壁のクラス
/// 近づくと、壁にアタッチされているマテリアルの透明度が変わりだんだん色が濃くなっていく。
/// 壁にぶつかった時のゲームオーバーの判定はPlayerHeliControllerで行う。
/// </summary>
public class Wall : MonoBehaviour
{
    /// <summary>
    ///　壁の設置されている方向を表す
    ///　0:x方向
    ///  1:y方向
    ///  2:z方向
    ///  この軸での壁との距離を測り、この距離に応じて透明度を変化させる。
    /// </summary>

    [SerializeField]
    GameObject player;
    [Space(10)]
    [Header("壁の方向と距離 0:x,1:y,2:z")]
    [SerializeField]
    [Range(0, 2)]
    int direction;
    [SerializeField]
    float detectDistance;
    float distance;
    MeshRenderer meshrenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //距離を計算する部分
        if(direction == 0)
        {
            distance = this.transform.position.x - player.transform.position.x;
            if (distance < 0)
            {
                distance = -distance;
            }
        }
        if (direction == 1)
        {
            distance = this.transform.position.y - player.transform.position.y;
            if (distance < 0)
            {
                distance = -distance;
            }
        }
        if (direction == 2)
        {
            distance = this.transform.position.z - player.transform.position.z;
            if (distance < 0)
            {
                distance = -distance;
            }
        }

        if(distance < detectDistance)
        {
            meshrenderer.material.SetColor("_Color", new Color(0.27f, 1f, 0f, (1 - distance / detectDistance)*1f));
        }
    }
}
