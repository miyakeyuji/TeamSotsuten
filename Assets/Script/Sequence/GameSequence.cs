//-------------------------------------------------------------
//  ゲームシーン遷移クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class GameSequence : SequenceBehaviour
{
    [SerializeField]
    Camera mainCamera = null;
    
    /// <summary>
    /// ウォッチ端末の場合、隠すべきオブジェクト集
    /// </summary>
    [SerializeField]
    GameObject watchHidenObj = null;

    public override void Reset()
    {
 	     base.Reset();

    }

    public override void Finish()
    {
        base.Finish();
    }

	void Start () 
    {
        // ウォッチの場合の処理。
        // いらないゲームオブジェクトをアクティブにしない。
        if (ConnectionManager.IsWacth)
        {
            watchHidenObj.SetActive(false);
            return;
        }

        mainCamera.enabled = false;
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            SequenceManager.Instance.ChangeScene(SceneID.RESULT);
        }
	}
}
