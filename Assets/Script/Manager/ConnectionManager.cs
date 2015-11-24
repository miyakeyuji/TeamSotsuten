//-------------------------------------------------------------
//  コネクション管理クラス
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectionManager : Singleton<CharacterSelectManager>
{
    enum TerminalType
    {
        Null = -1,
        Owner,
        Phone,
        Watch,
    }

    [SerializeField]
    Button hostButton = null;

    [SerializeField]
    Button smartPhoneButton = null;

    [SerializeField]
    Button watchButton = null;

    /// <summary>
    /// 退出ボタン
    /// </summary>
    [SerializeField]
    Button eegressButton = null;

    /// <summary>
    /// コネクション状態のテキスト
    /// </summary>
    [SerializeField]
    Text connectionStateText = null;

    /// <summary>
    /// 接続最大人数
    /// </summary>
    [SerializeField]
    byte roomMaxPlayerNum = 5;

    PhotonView view = null;

    static TerminalType type = TerminalType.Null;

    int smartPhoneID = 0;
    int watchID = 0;

    static public bool IsWacth { get { return type == TerminalType.Watch; } }
    static public bool IsSmartPhone { get { return type == TerminalType.Phone; } }
    static public bool IsOwner { get { return type == TerminalType.Owner; } }

    /// <summary>
    /// オーナー（ホスト）
    /// </summary>
    static public PhotonPlayer OwnerPlayer { get; private set; }

    /// <summary>
    /// スマホのプレイヤー情報
    /// </summary>
    static PhotonPlayer[] smartPhonePlayer = new PhotonPlayer[2];

    /// <summary>
    /// ウォッチのプレイヤー情報
    /// </summary>
    static PhotonPlayer[] watchPlayer = new PhotonPlayer[2];

    /// <summary>
    /// スマホのプレイヤーを取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    static public PhotonPlayer GetSmartPhonePlayer(int id)
    {
        return smartPhonePlayer[id];
    }

    /// <summary>
    /// ウォッチのプレイヤーを取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    static public PhotonPlayer GetWatchPlayer(int id)
    {
        return watchPlayer[id];
    }

    public override void Awake()
    {
        base.Awake();

        //マスターサーバーへ接続
        PhotonNetwork.offlineMode = false;
        PhotonNetwork.ConnectUsingSettings("v0.1");

        view = GetComponent(typeof(PhotonView)) as PhotonView;

    }

    public override void Start()
    {
        base.Start();

        eegressButton.gameObject.SetActive(false);

        eegressButton.onClick.AddListener(OnEegress);
        hostButton.onClick.AddListener(OnOwnerConnection);
        smartPhoneButton.onClick.AddListener(OnPhoneConnection);
        watchButton.onClick.AddListener(OnWatchConnection);
    }

    public override void Update()
    {
        base.Update();

    }

    /// <summary>
    /// すべてのConnectionボタンをDisableにする。
    /// </summary>
    void AllConnectionButtonDisable()
    {
        smartPhoneButton.enabled = false;
        watchButton.enabled = false;
        hostButton.enabled = false;
    }

    /// <summary>
    /// スマホでのコネクションアクション
    /// </summary>
    public void OnPhoneConnection()
    {
        Debugger.Log(">> OnPhoneConnection()");

        StartCoroutine("OnConnection");
        type = TerminalType.Phone;

        AllConnectionButtonDisable();

    }

    /// <summary>
    /// ウォッチのコネクションアクション
    /// </summary>
    public void OnWatchConnection()
    {
        Debugger.Log(">> OnWatchConnection()");

        StartCoroutine("OnConnection");
        type = TerminalType.Watch;
        watchButton.enabled = false;

        AllConnectionButtonDisable();

    }

    /// <summary>
    /// オーナーのコネクションアクション
    /// </summary>
    public void OnOwnerConnection()
    {
        if (PhotonNetwork.isMasterClient) return;

        Debugger.Log(">> OnOwnerConnection()");

        StartCoroutine("OnConnection");
        type = TerminalType.Owner;

        AllConnectionButtonDisable();
    }

    /// <summary>
    /// コネクションする。
    /// </summary>
    IEnumerator OnConnection()
    {
        yield return new WaitForSeconds(1.0f);

        // 部屋設定
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.isOpen = true;      // 部屋を開くか
        roomOptions.isVisible = true;   // 一覧に表示するか
        roomOptions.maxPlayers = roomMaxPlayerNum;

        // 部屋に参加、存在しない時作成して参加
        bool isJoin = PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, new TypedLobby());

        Debugger.Log("部屋に参加 : " + isJoin);
    }

    /// <summary>
    /// ルーム退室
    /// </summary>
    public void OnEegress()
    {
        view.RPC("AllEgressRoom", PhotonTargets.All);
    }

    /// <summary>
    /// スマホを設定する。
    /// 配列に入れていく。
    /// </summary>
    /// <param name="player"></param>
    /// <param name="info"></param>
    [PunRPC]
    public void SetSmartPhoneID(PhotonPlayer player, PhotonMessageInfo info)
    {
        smartPhonePlayer[smartPhoneID] = player;

        var debugIndex = smartPhoneID + 1;
        Debugger.Log("スマホ_" + debugIndex + " : 接続");

        connectionStateText.enabled = true;
        connectionStateText.text = "接続待ち";

        smartPhoneID++;
    }

    /// <summary>
    /// ウォッチを設定する。
    /// 配列に入れていく。
    /// </summary>
    /// <param name="player"></param>
    /// <param name="info"></param>
    [PunRPC]
    public void SetWatchID(PhotonPlayer player, PhotonMessageInfo info)
    {
        watchPlayer[watchID] = player;

        var debugIndex = watchID + 1;
        Debugger.Log("ウォッチ_" + debugIndex + " : 接続");

        watchID++;
    }


    /// <summary>
    /// ルームの接続状態をアクティブにする。
    /// </summary>
    /// <param name="info"></param>
    [PunRPC]
    public void ActiveRoomState(PhotonMessageInfo info)
    {
        connectionStateText.enabled = true;
        connectionStateText.text = "接続中";
    }

    /// <summary>
    /// シーンを切り替える。　同期
    /// </summary>
    /// <param name="info"></param>
    [PunRPC]
    public void SyncChangeScene(PhotonMessageInfo info)
    {
        ChangeScene();
    }
    
    /// <summary>
    /// シーンを切り替える
    /// </summary>
    void ChangeScene()
    {
        SequenceManager.Instance.ChangeScene(SceneID.TITLE);
    }

    /// <summary>
    /// マスターサーバーのロビー入室時
    /// </summary>
    void OnJoinedLobby()
    {
        Debugger.Log(">> ロビー入室");
    }

    /// <summary>
    /// ルーム参加時
    /// </summary>
    void OnJoinedRoom()
    {
        Debugger.Log(">> ルーム参加");

        OwnerPlayer = PhotonNetwork.masterClient;
        Debugger.Log("Owner : " + OwnerPlayer.ID);

        var index = PhotonNetwork.playerList.Length - 1;

        switch (type)
        {
            case TerminalType.Phone:
                view.RPC("SetSmartPhoneID", PhotonTargets.All, new object[] { PhotonNetwork.playerList[index] });
                break;

            case TerminalType.Watch:
                view.RPC("SetWatchID", PhotonTargets.All, new object[] { PhotonNetwork.playerList[index] });
                view.RPC("ActiveRoomState", PhotonTargets.All);
                view.RPC("SyncChangeScene", PhotonTargets.All);
                break;

            case TerminalType.Owner:
                ChangeScene();        
                Debugger.Log("オーナー : 接続中");
                break;

            default:
                Debugger.LogError("端末の種類を設定できていません。");

                break;
        }

        eegressButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// 全員を退出
    /// </summary>
    /// <param name="info"></param>
    [PunRPC]
    public void AllEgressRoom(PhotonMessageInfo info)
    {
        // ルーム退室
        PhotonNetwork.LeaveRoom();

        Application.LoadLevel(0);
    }


    /// <summary>
    /// 他のユーザーのルーム退室時
    /// </summary>
    /// <param name="otherPlayer">Other player.</param>
    void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debugger.Log("他の人が退出しました。");

    }

    /// <summary>
    /// 他ユーザーがルームに接続した時
    /// </summary>
    /// <param name="newPlayer">New player.</param>
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debugger.Log("他の人が接続しました");
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
