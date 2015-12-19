using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CheckActive : MonoBehaviour {
    EffectDB effctDB;

    /// <summary>
    /// 子に置いているオブジェクトで非アクティブのオブジェクトを探し、
    /// 最初に見つけたものを使用する
    /// </summary>
    /// <param name="skillType"></param>
    public void Check(MotionManager.MotionSkillType sType)
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                var a = child.GetComponent<EffectMover>();
                a.OnObject(
                    effctDB.dataList[(int)sType].skillType,
                    effctDB.dataList[(int)sType].speed,
                    GameObject.Find("Enemy").gameObject
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
