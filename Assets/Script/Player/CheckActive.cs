///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
/// エフェクトオブジェクトがアクティブになっているか
/// 
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CheckActive : MonoBehaviour {
    /// <summary>
    /// エフェクトのデータベース
    /// </summary>
    EffectDB effctDB;

    /// <summary>
    /// 子に置いているオブジェクトで非アクティブのオブジェクトを探し、
    /// 最初に見つけたものを使用する
    /// </summary>
    /// <param name="skillType"></param>
    public void Check(
        MotionManager.MotionSkillType sType,
        GameObject player, 
        GameObject enemy)
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                var effect = child.GetComponent<EffectMover>();
                effect.OnObject(
                    effctDB.dataList[(int)sType].skillType,
                    effctDB.dataList[(int)sType].speed,
                    enemy,
                    player,
                    effctDB.dataList[(int)sType].scale,
                    effctDB.dataList[(int)sType].damage
                    );
                break;
            }
        }
    }
    /// <summary>
    /// Update メソッドが最初に呼び出される前のフレームで呼び出されます
    /// </summary>
    void Start()
    {
        effctDB = GetComponentInParent<EffectDB>();
    }
}
