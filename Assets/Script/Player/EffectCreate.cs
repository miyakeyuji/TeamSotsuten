using UnityEngine;
using System.Collections;

public class EffectCreate : MonoBehaviour
{
    [SerializeField]
    public GameObject start;

    /// <summary>
    /// どの攻撃タイプのエフェクトを生成するか確認
    /// </summary>
    /// <param name="skillType"></param>
    void CheckType(MotionManager.MotionSkillType skillType)
    {
        GameObject checkObject = null;
        switch (skillType)
        {
            case MotionManager.MotionSkillType.HORIZONTAL_LEFT_RIGHT:
                checkObject = transform.FindChild("LeftRight").gameObject;
                break;
            case MotionManager.MotionSkillType.HORIZONTAL_RIGHT_LEFT:
                checkObject = transform.FindChild("RightLeft").gameObject;
                break;
            case MotionManager.MotionSkillType.VERTICAL_DOWN_UP:
                checkObject = transform.FindChild("DownUp").gameObject;
                break;
            case MotionManager.MotionSkillType.VERTICAL_UP_DOWN:
                checkObject = transform.FindChild("UpDown").gameObject;
                break;
            default:
                break;
        }

        /// <summary>
        /// 送られてきたスキルタイプがNONEじゃないなら使用オブジェクトの確認へ
        /// </summary>
        if (skillType != MotionManager.MotionSkillType.NONE)
        {
            checkObject.SendMessage("Check", skillType);
        }
    }
}
