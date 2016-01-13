﻿//-------------------------------------------------------------
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

    PhotonView view = null;

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
        view = GetComponent(typeof(PhotonView)) as PhotonView;

        if (!ConnectionManager.IsSmartPhone) return;

        watchRecorder.StartDebugShow();
	}


    /// <summary>
    /// シーンを切り替える。
    /// </summary>
    public void ChangeScene()
    {
        if (ConnectionManager.IsSmartPhone)
        {
            view.RPC("SyncDecision", ConnectionManager.GetSmartPhonePlayer());
        }
    }

    int decisionPlayerNum = 0;

    /// <summary>
    /// 同期用の決定処理
    /// </summary>
    /// <param name="info"></param>
    [PunRPC]
    void SyncDecision(PhotonMessageInfo info)
    {
        decisionPlayerNum++;
    }

    /// <summary>
    /// 同期用のシーン切り替え処理
    /// </summary>
    /// <param name="info"></param>
    [PunRPC]
    void SyncChangeScene(PhotonMessageInfo info)
    {
        SequenceManager.Instance.ChangeScene(SceneID.GAME);
    }
	
    void Update ()
    {
        if (ConnectionManager.IsSmartPhone && decisionPlayerNum >= 1)
        {
            view.RPC("SyncChangeScene", PhotonTargets.All);
        }
	}


    /// <summary>
    /// 書かないといけない関数
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

}
