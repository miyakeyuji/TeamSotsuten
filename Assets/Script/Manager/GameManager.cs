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
    [SerializeField]
    const int MAXIMUM_PLAYER_NUM = 1;   // 最大プレイヤー数
    public int MaxPlayerNum { get { return MAXIMUM_PLAYER_NUM; } }      // 外から最大数を取得したい場合、プレイヤー
    PlayerMasterData[] PlayerDataArray = new PlayerMasterData[MAXIMUM_PLAYER_NUM];

    // エネミーデータ
    [SerializeField]
    const int MAXIMUM_ENEMY_NUM = 1;
    public int MaxEnemyNum { get { return MAXIMUM_ENEMY_NUM; } }        // 外から最大数を取得したい場合、エネミー
    EnemyMasterData[] EnemyDataArray = new EnemyMasterData[MAXIMUM_ENEMY_NUM];

    // プレイヤーアタックデータ
    [SerializeField]
    const int MAXIMUM_PLAYER_ATTACK_NUM = 1;
    public int MaxPlayerAttackNum { get { return MAXIMUM_PLAYER_ATTACK_NUM; } }
    PlayerAttackMasterData[] PlayerAttackDataArray = new PlayerAttackMasterData[MAXIMUM_PLAYER_ATTACK_NUM];

    // エネミーアタックデータ
    [SerializeField]
    const int MAXIMUM_ENEMY_ATTACK_NUM = 1;
    public int MaxEnemyAttackNum { get { return MAXIMUM_ENEMY_ATTACK_NUM;} }       // 外から最大数を取得したい場合、アタック
    EnemyAttackMasterData[] EnemyAttackDataArray = new EnemyAttackMasterData[MAXIMUM_ENEMY_ATTACK_NUM];


    


    //初回のみの初期化処理
    public override void Awake()
    {
        base.Awake();

        view = GetComponent<PhotonView>();

        SendPlayerDataAwake();
        SendEnemyDataAwake();
        SendPlayerAttackDataAwake();
        SendEnemyAttackDataAwake();
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
        for (int i = 0; i < MaxPlayerNum; i++)
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
        for(int i = 0; i< MaxEnemyNum;i++)
        {
            EnemyDataArray[i] = new EnemyMasterData();
        }
    }



    /// <summary>
    /// 攻撃情報の初回初期化
    /// </summary>
    void SendPlayerAttackDataAwake()
    {
        view.RPC("SyncPlayerAttackDataAwake", PhotonTargets.All);
    }

    /// <summary>
    /// アウェイクで同期させる攻撃データ
    /// </summary>
    /// <param name="_info"></param>
    [PunRPC]
    void SyncPlayerAttackDataAwake(PhotonMessageInfo _info)
    {
        for (int i = 0; i < MaxPlayerAttackNum; i++)
        {
            PlayerAttackDataArray[i] = new PlayerAttackMasterData();
        }
    }



    /// <summary>
    /// 攻撃情報の初回初期化
    /// </summary>
    void SendEnemyAttackDataAwake()
    {
        view.RPC("SyncEnemyAttackDataAwake", PhotonTargets.All);
    }

    /// <summary>
    /// アウェイクで同期させる攻撃データ
    /// </summary>
    /// <param name="_info"></param>
    [PunRPC]
    void SyncEnemyAttackDataAwake(PhotonMessageInfo _info)
    {
        for (int i = 0; i < MaxEnemyAttackNum; i++)
        {
            EnemyAttackDataArray[i] = new EnemyAttackMasterData();
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
            Debug.LogError(_funcName + " 配列の範囲外です!!" + "Index = " + _arrayNumber);
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
            Debug.LogError(_funcName + " 配列の範囲外です!!" + "Index = " + _arrayNumber);
            return true;
        }
        return false;
    }


    /// <summary>
    /// プレイヤーアタック
    /// 配列インデクスの範囲外チェック
    /// 範囲外(Error) = true , 範囲内(ok) = false
    /// </summary>
    /// <param name="_arrayNumber"></param>
    /// <param name="_funcName"></param>
    /// <returns></returns>
    bool CheckOutRangeArrayNumberPlayerAttack(int _arrayNumber, string _funcName)
    {
        if (_arrayNumber < 0 || _arrayNumber >= MaxPlayerAttackNum)
        {
            Debug.LogError(_funcName + " 配列の範囲外です!!" + "Index = " + _arrayNumber);
            return true;
        }
        return false;
    }


    /// <summary>
    /// エネミーアタック
    /// 配列インデクスの範囲外チェック
    /// 範囲外(Error) = true , 範囲内(ok) = false
    /// </summary>
    /// <param name="_arrayNumber"></param>
    /// <param name="_funcName"></param>
    /// <returns></returns>
    bool CheckOutRangeArrayNumberEnemyAttack(int _arrayNumber, string _funcName)
    {
        if (_arrayNumber < 0 || _arrayNumber >= MaxEnemyAttackNum)
        {
            Debug.LogError(_funcName + " 配列の範囲外です!!" + "Index = " + _arrayNumber);
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
    /// プレイヤー攻撃の情報を取得したい場合、この関数を読んでください、
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public PlayerAttackMasterData GetPlayerAttackData(int _arrayNumber)
    {
        if (CheckOutRangeArrayNumberPlayerAttack(_arrayNumber, "GetPlayerAttackData"))        
            return null;

        return PlayerAttackDataArray[_arrayNumber];
    }


    /// <summary>
    /// エネミー攻撃の情報を取得したい場合、この関数を読んでください、
    /// ※取得したデータを直接書き換えないでください。
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public EnemyAttackMasterData GetEnemyAttackData(int _arrayNumber)
    {
        if (CheckOutRangeArrayNumberEnemyAttack(_arrayNumber, "GetEnemyAttackData"))
            return null;

        return EnemyAttackDataArray[_arrayNumber];
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
    /// プレイヤーの攻撃の有効化を変更します。（同期）
    /// </summary>
    public void SendPlayerAttackIsActive(int _arrayNumber,bool _isActive)
    {
        if (CheckOutRangeArrayNumberPlayerAttack(_arrayNumber, "SendPlayerAttackIsActive"))
            return;
        view.RPC("SyncPlayerAttackIsLife", PhotonTargets.All, new object[] { _arrayNumber ,_isActive});
    }

    [PunRPC]
    void SyncPlayerAttackIsLife(int _arrayNumber,bool _isActive, PhotonMessageInfo _info)
    {//攻撃IsLife送信
        PlayerAttackDataArray[_arrayNumber].IsActive = _isActive;
    }


    /// <summary>
    /// エネミーの攻撃の有効化を変更します。（同期）
    /// </summary>
    public void SendEnemyAttackIsActive(int _arrayNumber, bool _isActive)
    {
        if (CheckOutRangeArrayNumberEnemyAttack(_arrayNumber, "SendEnemyAttackIsActive"))
            return;
        view.RPC("SyncEnemyAttackIsLife", PhotonTargets.All, new object[] { _arrayNumber, _isActive });
    }

    [PunRPC]
    void SyncEnemyAttackIsLife(int _arrayNumber, bool _isActive, PhotonMessageInfo _info)
    {//攻撃IsLife送信
        EnemyAttackDataArray[_arrayNumber].IsActive = _isActive;
    }



}
