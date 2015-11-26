//-------------------------------------------------------------
//  コネクションシーン遷移クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class ConnnectionSequence : SequenceBehaviour
{

    public override void Reset()
    {
        base.Reset();
    }

    public override void Finish()
    {
        base.Finish();
    }

    void Start()
    {

    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SequenceManager.Instance.ChangeScene(SceneID.GAME);
        }
#endif
    }
}
