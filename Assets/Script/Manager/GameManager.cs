//-------------------------------------------------------------
//  ゲーム管理クラス
//  データの管理、同期を行います 
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


    //初回のみの初期化処理
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
    /// プレイヤー情報の初回初期化
    /// </summary>
    void SendPlayerDataAwake()
    {
        view.RPC("SyncPlayerDataAwake", PhotonTargets.All);
    }

    /// <summary>
    /// 初回初期化で同期させるプレイヤー
    /// </summary>
    /// <param name="_info"></param>
    [PunRPC]
    void SyncPlayerDataAwake(PhotonMessageInfo _info)
    {
        for (int i = 0; i < MAXIMUM_PLAYER_NUM; i++)
        {
            PlayerDataArray[i] = new PlayerMasterData();
        }
    }



    /// <summary>
    /// 敵情報の初回初期化
    /// </summary>
    void SendEnemyDataAwake()
    {
        view.RPC("SyncEnemyDataAwake", PhotonTargets.All);
    }

    /// <summary>
    /// 初回初期化で同期させるエネミーデータ
    /// </summary>
    /// <param name="_info"></param>
    [PunRPC]
    void SyncEnemyDataAwake(PhotonMessageInfo _info)
    {
        for(int i = 0; i< MAXIMUM_ENEMY_NUM;i++)
        {
            EnemyDataArray[i] = new EnemyMasterData();
        }
    }



    /// <summary>
    /// 攻撃情報の初回初期化
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
    }



    /// <summary>
    /// プレイヤー
    /// 配列インデクスの範囲外チェック
    /// 範囲外(Error) = true , 範囲内(ok) = false
    /// </summary>
    /// <param name="_arrayNumber"></param>
    /// <param name="_funcName"></param>
    /// <returns></returns>
    bool CheckOutRangeArrayNumberPlayer(int _arrayNumber,string _funcName)
    {
        if (_arrayNumber < 0 || _arrayNumber >= MaxPlayerNum)
        {
            Debug.LogError(_funcName + " 配列の範囲外です!!");
            return true;
        }
        return false;
    }


    /// <summary>
    /// エネミー
    /// 配列インデクスの範囲外チェック
    /// 範囲外(Error) = true , 範囲内(ok) = false
    /// </summary>
    /// <param name="_arrayNumber"></param>
    /// <param name="_funcName"></param>
    /// <returns></returns>
    bool CheckOutRangeArrayNumberEnemy(int _arrayNumber, string _funcName)
    {
        if (_arrayNumber < 0 || _arrayNumber >= MaxEnemyNum)
        {
            Debug.LogError(_funcName + " 配列の範囲外です!!");
            return true;
        }
        return false;
    }


    /// <summary>
    /// プレイヤー
    /// 配列インデクスの範囲外チェック
    /// 範囲外(Error) = true , 範囲内(ok) = false
    /// </summary>
    /// <param name="_arrayNumber"></param>
    /// <param name="_funcName"></param>
    /// <returns></returns>
    bool CheckOutRangeArrayNumberAttack(int _arrayNumber, string _funcName)
    {
        if (_arrayNumber < 0 || _arrayNumber >= MaxAttackNum)
        {
            Debug.LogError(_funcName + " 配列の範囲外です!!");
            return true;
        }
        return false;
    }





    /// <summary>
    /// プレイヤーの情報を取得したい場合、この関数を呼んでください。
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public PlayerMasterData GetPlayerData(int _arrayNumber)
    {
        if (CheckOutRangeArrayNumberPlayer(_arrayNumber,"GetPlayerData"))
            return null;
        
        return PlayerDataArray[_arrayNumber];
    }

    /// <summary>
    /// エネミーの情報を取得したい場合、この関数を読んでください、
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public EnemyMasterData GetEnemyData(int _arrayNumber)
    {
        if(CheckOutRangeArrayNumberEnemy(_arrayNumber,"GetEnemyData"))       
            return null;
        
        return EnemyDataArray[_arrayNumber];
    }



    /// <summary>
    /// 攻撃の情報を取得したい場合、この関数を読んでください、
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public AttackMasterData GetAttackData(int _arrayNumber)
    {
        if (CheckOutRangeArrayNumberAttack(_arrayNumber, "GetAttackData"))        
            return null;

        return AttackDataArray[_arrayNumber];
    }

    

    /// <summary>
    /// エネミーの生存フラグを変更します。
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_islife"></param>
    public void SendEnemyIsLife(int _arrayNumber,bool _islife)
    {
        if (CheckOutRangeArrayNumberEnemy(_arrayNumber, "SendEnemyIsLife"))
            return;
        
        view.RPC("SyncEnemyIsLife", PhotonTargets.All,new object[] { _arrayNumber,_islife});
    }

    [PunRPC]
    void SyncEnemyIsLife(int _arrayNumber, bool _islife, PhotonMessageInfo _info)
    {//エネミースポーン情報送信
        EnemyDataArray[_arrayNumber].IsLife = _islife;
    }


    /// <summary>
    /// エネミーのHPをセット(変更)します。（同期）
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_hp"></param>
    public void SendEnemyHP(int _arrayNumber, int _hp)
    {
        if (CheckOutRangeArrayNumberEnemy(_arrayNumber, "SendEnemyHP"))
            return;

        view.RPC("SyncEnemyHP",PhotonTargets.All,new object[] { _arrayNumber, _hp});
    }

    [PunRPC]
    void SyncEnemyHP(int _arrayNumber, int _hp, PhotonMessageInfo _info)
    {//HP同期
        EnemyDataArray[_arrayNumber].HP = _hp;
    }

    
    /// <summary>
    /// エネミーの座標を変更します。（同期）
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_pos"></param>
    public void SendEnemyPosition(int _arrayNumber, Vector3 _pos)
    {
        if (CheckOutRangeArrayNumberEnemy(_arrayNumber, "SendEnemyPosition"))
            return;

        view.RPC("SyncEnemyPosition", PhotonTargets.All, new object[] { _arrayNumber, _pos });
    }

    [PunRPC]
    void SyncEnemyPosition(int _arrayNumber, Vector3 _pos, PhotonMessageInfo _info)
    {
        EnemyDataArray[_arrayNumber].Position = _pos;
    }


    /// <summary>
    /// エネミーのローテーションを変更します。（同期）
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_pos"></param>
    public void SendEnemyRotation(int _arrayNumber, Vector3 _rotation)
    {
        if (CheckOutRangeArrayNumberEnemy(_arrayNumber, "SendEnemyRotation"))
            return;

        view.RPC("SyncEnemyRotation", PhotonTargets.All, new object[] { _arrayNumber, _rotation });
    }

    [PunRPC]
    void SyncEnemyRotation(int _arrayNumber, Vector3 _rotation, PhotonMessageInfo _info)
    {//回転情報送信
        EnemyDataArray[_arrayNumber].Rotation = _rotation;
    }



    
    /// <summary>
    /// プレイヤーの座標を変更する関数です。（同期）
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_pos"></param>
    public void SendPlayerPosition(int _arrayNumber, Vector3 _pos)
    {
        if (CheckOutRangeArrayNumberPlayer(_arrayNumber, "SendPlayerPosition"))
            return;

        view.RPC("SyncPlayerPosition",PhotonTargets.All,new object[] { _arrayNumber, _pos});
    }

    [PunRPC]
    void SyncPlayerPosition(int _arrayNumber, Vector3 _pos, PhotonMessageInfo _info)
    {//プレイヤー座標同期
        PlayerDataArray[_arrayNumber].Position = _pos;
    }


    /// <summary>
    /// 攻撃のIsLifeを変更します。（同期）
    /// </summary>
    public void SendAttackIsLife(int _arrayNumber,bool _isLife)
    {
        if (CheckOutRangeArrayNumberAttack(_arrayNumber, "SendAttackIsLife"))
            return;
        view.RPC("SyncAttackIsLife", PhotonTargets.All, new object[] { _arrayNumber ,_isLife});
    }

    [PunRPC]
    void SyncAttackIsLife(int _arrayNumber,bool _isLife, PhotonMessageInfo _info)
    {//攻撃IsLife送信
        AttackDataArray[_arrayNumber].IsLife = _isLife;
    }





}
