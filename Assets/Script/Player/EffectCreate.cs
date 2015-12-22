///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
/// エフェクトの作成管理スクリプト
/// 
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class EffectCreate : MonoBehaviour
{
    [SerializeField]
    CheckActive strength;

    [SerializeField]
    CheckActive Weak;

    [SerializeField]
    GameObject player;

    /// <summary>
    /// どの攻撃タイプのエフェクトを生成するか確認
    /// </summary>
    /// <param name="skillType"></param>
    void CheckType(MotionManager.MotionSkillType skillType)
    {
        GameObject checkObject = null;

        switch (skillType)
        {
            case MotionManager.MotionSkillType.STRENGTH:
                strength.Check(skillType , player);
                checkObject = transform.FindChild("Strength").gameObject;
                break;
            case MotionManager.MotionSkillType.WEAK:
                checkObject = transform.FindChild("Weak").gameObject;
                Weak.Check(skillType,player);
                break;
            default:
                break;
        }
    }
}
