﻿//-------------------------------------------------------------
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
    PlayerMasterData PlayerDataArray = new PlayerMasterData();

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

        UpdateClient();
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
        // ターゲット画像が読み込まれていないなら処理しない。
        if (!Vuforia.VuforiaBehaviour.IsMarkerLookAt)
        {
           // Debugger.Log("ターゲットロスト");
            return;
        }
        //  スマートフォンのポジションの同期
        PlayerDataArray.Position = SequenceManager.Instance.ARCamera.transform.position ;
    }


    /// <summary>
    /// プレイヤー情報の初回初期化
    /// </summary>
    void SendPlayerDataAwake()
    {
        PlayerDataArray = new PlayerMasterData();
    }


    /// <summary>
    /// 敵情報の初回初期化
    /// </summary>
    void SendEnemyDataAwake()
    {
        for (int i = 0; i < MaxEnemyNum; i++)
        {
            EnemyDataArray[i] = new EnemyMasterData();
        }
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
    public PlayerMasterData GetPlayerData()
    {
        return PlayerDataArray;
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
        //if (CheckOutRangeArrayNumberEnemy(_arrayNumber, "SendEnemyIsActive"))
        //    return;

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

        EnemyDataArray[_arrayNumber].Rotation = _rotation;

    }


    /// <summary>
    /// プレイヤーの座標を変更する関数です。（同期）
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_pos"></param>
    public void SendPlayerPosition( Vector3 _pos)
    {
        PlayerDataArray.Position = _pos;
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
    
        //TODO 後で追加らしい 
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
        {
            //現在アクティブなエネミーを検索
            if (EnemyDataArray[i].IsActive == true)
            {
                enemyIndex = i;
            }
        }

        // FIX 判定がおかしかった。
        if (CheckOutRangeArrayNumberEnemy(enemyIndex, "SendEnemyHit"))
        {
            //return;
        }

        EnemyDataArray[enemyIndex].HP -= _damage;
        EnemyDataArray[enemyIndex].HitAttackType = _playerAttackType;
        EnemyDataArray[enemyIndex].IsHit = true;
    }

    /// <summary>
    /// リザルトシーンに遷移
    /// </summary>
    public void ChangeResultScene()
    {
        view.RPC("SyncChangeResultScene", PhotonTargets.All);
    }

    private void SyncChangeResultScene(PhotonMessageInfo info)
    {
        SequenceManager.Instance.ChangeScene(SceneID.RESULT);
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
