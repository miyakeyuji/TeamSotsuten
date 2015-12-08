//-------------------------------------------------------------
//  攻撃スキル発生クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class AttackSkillCreator : MonoBehaviour
{

    /// <summary>
    /// 判別した攻撃タイプの送信先
    /// </summary>
    [SerializeField]
    public EffectMover effect;

    /// <summary>
    /// スキルを生成
    /// 生成できるようになると、この関数が呼ばれます。
    /// ここに発生するエフェクト等を記述してください。
    /// </summary>
    public void OnMotionComplated()
    {
        if (MotionManager.Instance.MotionSkill != MotionManager.MotionSkillType.NONE) effect.OnObject(MotionManager.Instance.MotionSkill);
    }
}
