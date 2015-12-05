//-------------------------------------------------------------
//  キャラクター選択シーン遷移クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class CharacterSelectSequence : SequenceBehaviour
{

    [SerializeField]
    WatchDeviceInfoRecorder watchRecorder = null;


    public override void Reset()
    {
        base.Reset();

        watchRecorder.StopDebugShow();
    }

    public override void Finish()
    {
        base.Finish();

        watchRecorder.StopDebugShow();

    }

	// Use this for initialization
	void Start () 
    {
        if (!ConnectionManager.IsSmartPhone) return;

        watchRecorder.StartDebugShow();
	}
	
	// Update is called once per frame
	void Update ()
    {

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SequenceManager.Instance.ChangeScene(SceneID.GAME);
        //}
	}
}
