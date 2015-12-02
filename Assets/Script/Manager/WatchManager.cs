//-------------------------------------------------------------
//  ウォッチ管理クラス
//  加速度センサー・ジャイロ
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WatchManager : Singleton<WatchManager>
{
    [SerializeField]
    Text debugText = null;

    PhotonView view = null;

    /// <summary>
    /// 加速度センサー
    /// </summary>
    public Vector3 Acc { get; private set; }

    /// <summary>
    /// ジャイロセンサー
    /// </summary>
    public Vector3 GyroAngle { get; private set; }
    
    public override void Awake()
    {
        base.Awake();

        view = GetComponent<PhotonView>();

        Input.gyro.enabled = true;

        if (ConnectionManager.IsOwner)
        {
            enabled = false;
        }

    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (ConnectionManager.IsWacth)
        {
            var acc = Input.acceleration;
            var gyroAngle = Input.gyro.attitude.eulerAngles;

            view.RPC("SyncData", 
                ConnectionManager.GetSmartPhonePlayer(ConnectionManager.ID), 
                new object[] { acc, gyroAngle });
            
            DebugTextShow(acc, gyroAngle);
        }
    }

    [PunRPC]
    void SyncData(Vector3 acc,Vector3 gyroAngle,PhotonMessageInfo info)
    {
        Acc = acc;
        GyroAngle = gyroAngle;

        DebugTextShow(Acc, GyroAngle);
    }

    void DebugTextShow(Vector3 acc, Vector3 gyroAngle)
    {
        debugText.text = "Acc : " + acc.ToString() + "\n";
        debugText.text += "GyroAngle : " + gyroAngle.ToString();
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
