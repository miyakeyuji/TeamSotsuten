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
    const int MAXIMUM_PLAYER_NUM = 1;   // 最大プレイヤー数
    public int MaxPlayerNum { get { return MAXIMUM_PLAYER_NUM; } }      // 外から最大数を取得したい場合、プレイヤー
    PlayerMasterData[] PlayerDataArray = new PlayerMasterData[MAXIMUM_PLAYER_NUM];

    // エネミーデータ
    const int MAXIMUM_ENEMY_NUM = 1;
    public int MaxEnemyNum { get { return MAXIMUM_ENEMY_NUM; } }        // 外から最大数を取得したい場合、エネミー
    EnemyMasterData[] EnemyDataArray = new EnemyMasterData[MAXIMUM_ENEMY_NUM];


    //　アタックデータ
    const int MAXIMUM_ATTACK_NUM = 1;
    public int MaxAttackNum { get { return MAXIMUM_ATTACK_NUM;} }       // 外から最大数を取得したい場合、アタック
    AttackMasterData[] AttackDataArray = new AttackMasterData[MAXIMUM_ATTACK_NUM];


    public override void Awake()
    {
        base.Awake();

        view = GetComponent<PhotonView>();

        SendPlayerDataAwake();
        SendEnemyDataAwake();
        SendAttackDataAwake();
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
    void SendPlayerDataAwake()
    {
        view.RPC("SyncPlayerDataAwake", PhotonTargets.All);
    }

    /// <summary>
    /// アウェイクで同期させるプレイヤーデータ
    /// </summary>
    /// <param name="_info"></param>
    [PunRPC]
    void SyncPlayerDataAwake(PhotonMessageInfo _info)
    {
        for (int i = 0; i < MAXIMUM_PLAYER_NUM; i++)
        {
            PlayerDataArray[i] = new PlayerMasterData();
        }
        //プレイヤーＩＤの同期
        for (int i = 0; i < MAXIMUM_PLAYER_NUM; i++)
        {
            PlayerDataArray[i].ID = i;
        }
    }



    /// <summary>
    /// 敵情報のアウェイク
    /// </summary>
    void SendEnemyDataAwake()
    {
        view.RPC("SyncEnemyDataAwake", PhotonTargets.All);
    }

    /// <summary>
    /// アウェイクで同期させるエネミーデータ
    /// </summary>
    /// <param name="_info"></param>
    [PunRPC]
    void SyncEnemyDataAwake(PhotonMessageInfo _info)
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
    /// 敵情報のアウェイク
    /// </summary>
    void SendAttackDataAwake()
    {
        view.RPC("SyncAttackDataAwake", PhotonTargets.All);
    }

    /// <summary>
    /// アウェイクで同期させる攻撃データ
    /// </summary>
    /// <param name="_info"></param>
    [PunRPC]
    void SyncAttackDataAwake(PhotonMessageInfo _info)
    {
        for (int i = 0; i < MAXIMUM_ATTACK_NUM; i++)
        {
            AttackDataArray[i] = new AttackMasterData();
        }
        //エネミーＩＤの同期
        for (int i = 0; i < MAXIMUM_ATTACK_NUM; i++)
        {
            AttackDataArray[i].ID = i;
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
    /// エネミーの生存フラグを変更します。
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_islife"></param>
    public void SendEnemyIsLife(int _id,bool _islife)
    {
        view.RPC("SyncEnemyIsLife", PhotonTargets.All,new object[] { _id,_islife});
    }

    [PunRPC]
    void SyncEnemyIsLife(int _id,bool _islife, PhotonMessageInfo _info)
    {//エネミースポーン情報送信
        EnemyDataArray[_id].IsLife = _islife;
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
