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
    /// <summary>
    /// 呼び出す攻撃エフェクトを登録
    /// </summary>
    [SerializeField]
    CheckActive strength;

    [SerializeField]
    CheckActive Weak;

    /// <summary>
    /// プレイヤーを登録
    /// </summary>
    [SerializeField]
    GameObject player;

    /// <summary>
    /// 攻撃エフェクトのターゲットを登録
    /// </summary>
    [SerializeField]
    GameObject enemy;

    /// <summary>
    /// どの攻撃タイプのエフェクトを生成するか確認
    /// </summary>
    /// <param name="skillType"></param>
    void CheckType(MotionManager.MotionSkillType skillType)
    {
        /// <summary>
        /// 攻撃タイプを判別
        /// </summary>
        switch (skillType)
        {
            case MotionManager.MotionSkillType.STRENGTH:
                strength.Check(skillType , player , enemy);
                break;
            case MotionManager.MotionSkillType.WEAK:
                Weak.Check(skillType,player , enemy);
                break;
            default:
                break;
        }
    }
}
