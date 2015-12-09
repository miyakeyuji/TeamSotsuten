//-------------------------------------------------------------
//  攻撃スキル発生クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class AttackSkillCreator : MonoBehaviour
{
    private GameObject[] effectArray = new GameObject[5];

    // Use this for initialization
    void Start()
    {
        effectArray[0] = transform.FindChild("Attact Effect0").gameObject;
        effectArray[1] = transform.FindChild("Attact Effect1").gameObject;
        effectArray[2] = transform.FindChild("Attact Effect2").gameObject;
        effectArray[3] = transform.FindChild("Attact Effect3").gameObject;
        effectArray[4] = transform.FindChild("Attact Effect4").gameObject;
    }

    /// <summary>
    /// スキルを生成
    /// 生成できるようになると、この関数が呼ばれます。
    /// ここに発生するエフェクト等を記述してください。
    /// 
    /// ・switch文での分岐をif文での分岐に変更
    /// ・effectMoverにて判断 
    /// ・呼び出すオブジェクトがアクティブかを確認する
    /// update by miyake
    /// </summary>
    public void OnMotionComplated()
    {
        if (MotionManager.Instance.MotionSkill != MotionManager.MotionSkillType.NONE)
        {
            foreach (var effect in effectArray)
            {
                if (effect.GetActive() != false) continue;
                effect.SetActive(true);
                effect.SendMessage("OnObject",MotionManager.Instance.MotionSkill);
                break;
            }
        }
    }
}
