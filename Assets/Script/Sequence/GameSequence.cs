//-------------------------------------------------------------
//  ゲームシーン遷移クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class GameSequence : SequenceBehaviour
{
    /// <summary>
    /// ウォッチ端末の場合、隠すべきオブジェクト集
    /// </summary>
    [SerializeField]
    GameObject watchHidenObj = null;

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

	void Start () 
    {
        // ウォッチの場合の処理。
        // いらないゲームオブジェクトをアクティブにしない。
        if (SequenceManager.Instance.IsBuildWatch)
        {
            watchHidenObj.SetActive(false);
            return;
        }

        if (!ConnectionManager.IsSmartPhone) return;

        watchRecorder.StartDebugShow();
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SequenceManager.Instance.ChangeScene(SceneID.RESULT);
        }
	}
}
