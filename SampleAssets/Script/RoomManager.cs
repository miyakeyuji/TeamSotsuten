using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;

public class RoomManager : Photon.MonoBehaviour
{
    enum TerminalType
    {
        Null = -1,
        Owner,
        Phone,
        Watch,
    }

    /// <summary>
    /// デバッグ用のフィールド
    /// </summary>
    [SerializeField]
    Text debugField = null; 

    /// <summary>
    /// コネクションオブジェクト管理
    /// </summary>
    [SerializeField]
    GameObject connectionManager = null;

    /// <summary>
    /// ゲームメインのオブジェクト管理
    /// </summary>
    [SerializeField]
    GameObject gameMainManager = null;

    /// <summary>
    /// 退出ボタン
    /// </summary>
    [SerializeField]
    Button eegressButton = null;

    /// <summary>
    /// ロビーのオブジェクト
    /// </summary>
    [SerializeField]
    GameObject LobbyObject = null;

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


    void Awake()
    {
        



        //マスターサーバーへ接続
        PhotonNetwork.offlineMode = false;
        PhotonNetwork.ConnectUsingSettings("v0.1");

        view = GetComponent<PhotonView>();

        eegressButton.onClick.AddListener(OnEegress);

        eegressButton.gameObject.SetActive(false);

        Vector3 pos = Vector3.zero;
        Func(pos);
    }

    void Func(Vector3 vec)
    { 
        
    }


    /// <summary>
    /// スマホでのコネクションアクション
    /// </summary>
    public void OnPhoneConnection()
    {
        if (!PhotonNetwork.isMasterClient) return;

        StartCoroutine("OnConnection");
        type = TerminalType.Phone;
    }

    /// <summary>
    /// ウォッチのコネクションアクション
    /// </summary>
    public void OnWatchConnection()
    {
        if (!PhotonNetwork.isMasterClient) return;

        StartCoroutine("OnConnection");
        type = TerminalType.Watch;
    }

    /// <summary>
    /// オーナーのコネクションアクション
    /// </summary>
    public void OnOwnerConnection()
    {
        if (PhotonNetwork.isMasterClient) return;

        StartCoroutine("OnConnection");
        type = TerminalType.Owner;
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
        PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, new TypedLobby());
    }

    /// <summary>
    /// ルーム退室
    /// </summary>
    public void OnEegress()
    {
        view.RPC("AllEgressRoom", PhotonTargets.All);
    }

    /// <summary>
    /// 管理している所を切り替える。
    /// </summary>
    void ChangeManager()
    {
        connectionManager.SetActive(false);
        gameMainManager.SetActive(true);
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

        debugField.text += "SmartPhone : " + debugIndex + "\n";

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
        debugField.text += "Watch : " + debugIndex + "\n";

        watchID++;
    }


    /// <summary>
    /// ルームの接続状態をアクティブにする。
    /// </summary>
    /// <param name="info"></param>
    [PunRPC]
    public void ActiveRoomState(PhotonMessageInfo info)
    {
        connectionStateText.text = "接続中";
    }

    /// <summary>
    /// ARカメラのアクティブにする。
    /// </summary>
    /// <param name="info"></param>
    [PunRPC]
    public void ActiveARCamera(PhotonMessageInfo info)
    {
        ChangeManager();
    }

    /// <summary>
    /// マスターサーバーのロビー入室時
    /// </summary>
    void OnJoinedLobby()
    {
        Debug.Log(">> ロビー入室");
    }
    
    /// <summary>
    /// ルーム参加時
    /// </summary>
    void OnJoinedRoom()
    {
        Debug.Log(">> ルーム参加");

        OwnerPlayer = PhotonNetwork.masterClient;
        debugField.text = "Owner : " + OwnerPlayer.ID + "\n";

        var index = PhotonNetwork.playerList.Length - 1;

        switch (type)
        {
            case TerminalType.Phone:
                view.RPC("SetSmartPhoneID", PhotonTargets.All, new object[] { PhotonNetwork.playerList[index] });
                break;
          
            case TerminalType.Watch:
                view.RPC("SetWatchID", PhotonTargets.All, new object[] { PhotonNetwork.playerList[index] });
                view.RPC("ActiveRoomState", PhotonTargets.All);
                view.RPC("ActiveARCamera", PhotonTargets.All);
                break;

            case TerminalType.Owner:
                ChangeManager();
                break;

            default:
                Debug.LogError("端末の種類を設定できていません。");
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
        Debug.Log("他の人が退出しました。");
    }

    /// <summary>
    /// 他ユーザーがルームに接続した時
    /// </summary>
    /// <param name="newPlayer">New player.</param>
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("他の人が接続しました ");
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
