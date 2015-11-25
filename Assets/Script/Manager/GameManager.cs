//-------------------------------------------------------------
//  ゲーム管理クラス
// 
//  code by m_yamada
//  and ogata
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class GameManager : Singleton<CharacterSelectManager>
{
    //フォトン用view
    PhotonView view = null;

    // プレイヤーデータ
    //const int MAXIMUM_PLAYER_NUM = 2;   // 最大プレイヤー数
    //PlayerMasterData[] PlayerDataArray = new PlayerMasterData[MAXIMUM_PLAYER_NUM];

    // エネミーデータ
    const int MAXIMUM_ENEMY_NUM = 1;   // 最大エネミー数
    EnemyMasterData[] _EnemyDataArray = new EnemyMasterData[MAXIMUM_ENEMY_NUM];
    

    public EnemyMasterData GetEnemyData(int _id)
    {
        return _EnemyDataArray[_id];
    }



    private class EnemyDataArrayment
    {
        EnemyMasterData[] _data;

    }


    public override void Awake()
    {
        base.Awake();

        view = GetComponent<PhotonView>();

    }

    public override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        base.Update();

    }


    

    /// <summary>
    /// 敵がヒットしたら、この関数をよんでください。
    /// </summary>
    /// <param name="isHit"></param>
    /*
    public void EnemyHit(bool isHit)
    {
        view.RPC("Func", PhotonTargets.All, new object[] { isHit });
    }


    [PunRPC]
    void Func(bool isHit,PhotonMessageInfo info)
    {
        EnemyMansterData.IsHit = isHit;
    }
    */








}
