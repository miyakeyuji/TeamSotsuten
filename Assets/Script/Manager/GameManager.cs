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
    const int MAXIMUM_PLAYER_NUM = 1;   // 最大数　プレイヤー
    public int MaxPlayerNum { get { return MAXIMUM_PLAYER_NUM; } }      // 外から最大数を取得したい場合、プレイヤー
    PlayerMasterData[] PlayerDataArray = new PlayerMasterData[MAXIMUM_PLAYER_NUM];

    // エネミーデータ
    const int MAXIMUM_ENEMY_NUM = 1;    // 最大数　エネミー
    public int MaxEnemyNum { get { return MAXIMUM_ENEMY_NUM; } }        // 外から最大数を取得したい場合、エネミー
    EnemyMasterData[] EnemyDataArray = new EnemyMasterData[MAXIMUM_ENEMY_NUM];
    


    


    //初回のみの初期化処理
    public override void Awake()
    {
        base.Awake();

        view = GetComponent<PhotonView>();


        // ARCameraが入っているか（クライアントのみの処理）
        if (ConnectionManager.IsSmartPhone) {
            if (SequenceManager.Instance.ARCamera == null)
            {
                Debugger.LogError("GameManagerのARCameraがnullです！！");
            }
        }

        SendPlayerDataAwake();
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

        UpdateOwner();

        UpdateClient();
    }


    //オーナー(ホスト)のみ行うアップデートです。
    private void UpdateOwner()
    {
        //オーナー確認
        if (!ConnectionManager.IsOwner){
            return;
        }
        
    }


    //クライアントのみ行うアップデートです。
    private void UpdateClient()
    {
        //クライアント確認
        if(!ConnectionManager.IsSmartPhone){
            return;
        }

        //スマフォ位置更新
        UpdateClientPosition();
    }


    /// <summary>
    /// スマートフォンの位置情報を更新します。
    /// </summary>
    private void UpdateClientPosition()
    {
        //プレイヤー(スマートフォンＩＤ)を取得
        int index = ConnectionManager.ID;

        // ターゲット画像が読み込まれていないなら処理しない。
        if (!Vuforia.VuforiaBehaviour.IsMarkerLookAt)
        {
           // Debugger.Log("ターゲットロスト");
            return;
        }

        view.RPC("SyncClientPosition", PhotonTargets.All, new object[] {index, SequenceManager.Instance.ARCamera.transform.position });
    }

    [PunRPC]
    private void SyncClientPosition(int _arrayNum,Vector3 _pos,PhotonMessageInfo _info)
    {//  スマートフォンのポジションの同期
        PlayerDataArray[_arrayNum].Position = _pos;

        //Debugger.Log("PlayerDataArray index = " + _arrayNum + "Position x = " + _pos.x + " y = " +_pos.y + " z = " + _pos.z);
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
    /// エネミーの生存フラグを変更します。
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_isActive"></param>
    public void SendEnemyIsActive(int _arrayNumber,bool _isActive)
    {
        if (CheckOutRangeArrayNumberEnemy(_arrayNumber, "SendEnemyIsActive"))
            return;
        
        view.RPC("SyncEnemyIsActive", PhotonTargets.All,new object[] { _arrayNumber,_isActive});
    }

    [PunRPC]
    void SyncEnemyIsActive(int _arrayNumber, bool _isActive, PhotonMessageInfo _info)
    {//エネミースポーン情報送信
        EnemyDataArray[_arrayNumber].IsActive = _isActive;
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
    /// プレイヤーの攻撃を生成します。
    /// </summary>
    /// <param name="_arrayNumber"></param>
    /// <param name="_attackType"></param>
    /// <param name="_enemyTargetIndex"></param>
    public void CreatePlayerAttack(int _arrayNumber, MotionManager.MotionSkillType _attackType, int _enemyTargetIndex)
    {
        if (CheckOutRangeArrayNumberEnemy(_enemyTargetIndex, "CreatePlayerAttack _enemyTargetIndex"))
            return;
        view.RPC("SyncCreatePlayerAttack",PhotonTargets.All,new object[] { _arrayNumber,_attackType,_enemyTargetIndex});
    }

    [PunRPC]
    void SyncCreatePlayerAttack(int _arrayNumber, MotionManager.MotionSkillType _attackType, int _enemyTargetIndex,PhotonMessageInfo _info)
    {
        //とりあえず空白,今後処理を記述、もしくは関数ごと削除します。
    }


    


    

    
    /// <summary>
    /// エネミーが攻撃に当たった時用の関数
    /// </summary>
    /// <param name="_playerAttackArrayNumber"></param>
    /// <param name="_damage"></param>
    public void SendEnemyHit(MotionManager.MotionSkillType _playerAttackType,int _damage)
    {
        int enemyIndex = -1;
        for (int i = 0; i<MaxEnemyNum;i++)
        {//現在アクティブなエネミーを検索
            if (EnemyDataArray[i].IsActive == true)
                enemyIndex = i;
        }

        // FIX 判定がおかしかった。
        if (CheckOutRangeArrayNumberEnemy(enemyIndex, "SendEnemyHit"))
        {
            //return;
        }

        Debugger.Log(">>> SendEnemyHit()");

        // TODO サーバーと通信を起こす。
        EnemyDataArray[enemyIndex].HP -= _damage;
        EnemyDataArray[enemyIndex].HitAttackType = _playerAttackType;
        EnemyDataArray[enemyIndex].IsHit = true;

        //view.RPC("SyncEnemyHit", PhotonTargets.All, new object[] { enemyIndex, _playerAttackType, _damage });
    }

    [PunRPC]
    private void SyncEnemyHit(int _enemyArrayNumber, MotionManager.MotionSkillType _playerAttackType, int _damage, PhotonMessageInfo _info)
    {
        //エネミーが攻撃に当たった時の同期
        EnemyDataArray[_enemyArrayNumber].HP -= _damage;
        EnemyDataArray[_enemyArrayNumber].HitAttackType = _playerAttackType;
        EnemyDataArray[_enemyArrayNumber].IsHit = true;
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
