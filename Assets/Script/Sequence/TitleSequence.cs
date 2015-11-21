//-------------------------------------------------------------
//  タイトルシーン遷移クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class TitleSequence : SequenceBehaviour
{

    public override void Reset()
    {
        base.Reset();
    }

    public override void Finish()
    {
        base.Finish();
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            SequenceManager.Instance.ChangeScene(SceneID.MAIN_MENU);
        }
	}
}
