//-------------------------------------------------------------
//  ゲーム管理クラス
// 
//  code by m_yamada
//  and ogata
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;



public class GameManager : Singleton<GameManager>
{
    //フォトン用view
    PhotonView view = null;

    // プレイヤーデータ
    public const int MAXIMUM_PLAYER_NUM = 1;   // 最大プレイヤー数
    PlayerMasterData[] PlayerDataArray = new PlayerMasterData[MAXIMUM_PLAYER_NUM];

    // エネミーデータ
    public const int MAXIMUM_ENEMY_NUM = 1;
    EnemyMasterData[] EnemyDataArray = new EnemyMasterData[MAXIMUM_ENEMY_NUM];


    //　アタックデータ
    public const int MAXIMUM_ATTACK_NUM = 1;
    AttackMasterData[] AttackDataArray = new AttackMasterData[MAXIMUM_ATTACK_NUM];


    public override void Awake()
    {
        base.Awake();

        view = GetComponent<PhotonView>();

        SendEnemyDataAwake();
    }

    //毎回の初期化処理
    public override void Start()
    {
        base.Start();
        
    }

    //更新
    public override void Update()
    {
        base.Update();

    }

    
    /// <summary>
    /// 敵情報のアウェイク
    /// </summary>
    void SendEnemyDataAwake()
    {
        view.RPC("SyncAwakeEnemyData", PhotonTargets.All);
    }

    /// <summary>
    /// アウェイクで同期させるエネミーデータ関数
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_id"></param>
    [PunRPC]
    void SyncAwakeEnemyData(PhotonMessageInfo _info)
    {
        for(int i = 0; i< MAXIMUM_ENEMY_NUM;i++)
        {
            EnemyDataArray[i] = new EnemyMasterData();
        }
        //エネミーＩＤの同期
        for (int i = 0;i<MAXIMUM_ENEMY_NUM;i++)
        {
            EnemyDataArray[i].ID = i;
        }
    }

    /// <summary>
    /// プレイヤーの情報を取得したい場合、この関数を呼んでください。
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public PlayerMasterData GetPlayerData(int _id)
    {
        return PlayerDataArray[_id];
    }

    /// <summary>
    /// エネミーの情報を取得したい場合、この関数を読んでください、
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public EnemyMasterData GetEnemyData(int _id)
    {
        return EnemyDataArray[_id];
    }



    /// <summary>
    /// 攻撃の情報を取得したい場合、この関数を読んでください、
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public AttackMasterData GetAttackData(int _id)
    {
        return AttackDataArray[_id];
    }

    

    /// <summary>
    /// エネミーを生成します。
    /// </summary>
    /// <param name="_id"></param>
    public void SendSpawnEnemy(int _id)
    {
        view.RPC("SyncSpawnEnemy", PhotonTargets.All,new object[] { _id});
    }

    [PunRPC]
    void SyncSpawnEnemy(int _id, PhotonMessageInfo _info)
    {//エネミースポーン情報送信
        EnemyDataArray[_id].IsLife = true;
    }


    /// <summary>
    /// エネミーのHPをセット(変更)します。
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_hp"></param>
    public void SendEnemyHP(int _id,int _hp)
    {
        view.RPC("SyncEnemyHP",PhotonTargets.All,new object[] { _id,_hp});
    }

    [PunRPC]
    void SyncEnemyHP(int _id,int _hp, PhotonMessageInfo _info)
    {//HP同期
        EnemyDataArray[_id].HP = _hp;
    }

    
    /// <summary>
    /// エネミーの座標をセットします。
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_pos"></param>
    public void SendEnemyPosition(int _id,Vector3 _pos)
    {
        view.RPC("SyncEnemyPosition", PhotonTargets.All, new object[] { _id,_pos });
    }

    [PunRPC]
    void SyncEnemyPosition(int _id,Vector3 _pos, PhotonMessageInfo _info)
    {
        EnemyDataArray[_id].Position = _pos;
    }


    /// <summary>
    /// エネミーのローテーションをセットします。
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_pos"></param>
    public void SendEnemyRotation(int _id, Vector3 _rotation)
    {
        view.RPC("SyncEnemyRotation", PhotonTargets.All, new object[] { _id, _rotation });
    }

    [PunRPC]
    void SyncEnemyRotation(int _id, Vector3 _rotation, PhotonMessageInfo _info)
    {//回転情報送信
        EnemyDataArray[_id].Rotation = _rotation;
    }






    /// <summary>
    /// 攻撃を生成します。
    /// </summary>
    public void CreateAttack(int _id)
    {
        view.RPC("SyncCreateAttack", PhotonTargets.All, new object[] { _id });
    }

    [PunRPC]
    void SyncCreateAttack(int _id, PhotonMessageInfo _info)
    {//攻撃生成情報送信
        AttackDataArray[_id].IsLife = true;
    }





}
