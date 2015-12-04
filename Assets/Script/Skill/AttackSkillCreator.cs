//-------------------------------------------------------------
//  攻撃スキル発生クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class AttackSkillCreator : MonoBehaviour {

    /// <summary>
    /// 判別した攻撃タイプの送信先
    /// </summary>
    [SerializeField]
    GameObject attackEffect;

    /// <summary>
    /// スキルを生成
    /// 生成できるようになると、この関数が呼ばれます。
    /// ここに発生するエフェクト等を記述してください。
    /// </summary>
    public void OnMotionComplated()
    {
        attackEffect.SendMessage("OnObject", MotionManager.Instance.MotionSkill);

        switch (MotionManager.Instance.MotionSkill)
        {
            case MotionManager.MotionSkillType.VERTICAL_DOWN_UP:

                break;

            case MotionManager.MotionSkillType.VERTICAL_UP_DOWN:

                break;

            case MotionManager.MotionSkillType.HORIZONTAL_LEFT_RIGHT:

                break;

            case MotionManager.MotionSkillType.HORIZONTAL_RIGHT_LEFT:

                break;
        }
    }
}
