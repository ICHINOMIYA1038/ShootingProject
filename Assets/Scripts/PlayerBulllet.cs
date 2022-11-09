using System.Collections; using System.Collections.Generic; using UnityEngine; using util;  /// <summary>
/// プレイヤーのミサイルのクラス
/// Bulletクラスを継承している
/// </summary> public class PlayerBulllet : Bullet, iCanDamage {
    /// <summary>
    ///プレイヤーにダメージを与える関数
    /// </summary>
    /// <param name="damageAmount">ダメージの量</param>
    /// <param name="target">ダメージを与える対象</param>     public void damage(int damageAmount, iApplicableDamaged target)     {         target.damaged(damageAmount);     }      /// <summary>
    ///衝突判定
    ///iapplicableDamagedインターフェースを持っている場合に処理をする
    /// </summary>     private void OnCollisionEnter(Collision collision)     {         iApplicableDamaged target;         target = collision.gameObject.GetComponent<iApplicableDamaged>();         if (target != null && !collision.gameObject.tag.Equals("Player"))         {             damage(1,target);             Destroy(this.gameObject);         }      } } 