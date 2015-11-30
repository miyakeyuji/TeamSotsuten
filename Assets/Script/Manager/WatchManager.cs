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
    
#if UNITY_ANDROID && !UNITY_EDITOR
    static AndroidJavaObject androidJavaObject = null;

    public class Data
    {
        public float x, y, z;
    }
    
    Data data = null;
#endif

    public override void Awake()
    {
        base.Awake();

        view = GetComponent<PhotonView>();

        Input.gyro.enabled = true;

#if UNITY_ANDROID && !UNITY_EDITOR
        androidJavaObject = new AndroidJavaObject("com.vantanps13.teamsotsuten.MainActivity");
#endif

        if (!ConnectionManager.IsSmartPhone)
        {
            enabled = false;
        }
    }

    public override void Start()
    {
        base.Start();


        Debugger.Log("IsWatch : " + ConnectionManager.IsWacth);

        Debugger.Log(ConnectionManager.GetSmartPhonePlayer(ConnectionManager.ID));

    }

    public override void Update()
    {
        base.Update();

        if (ConnectionManager.IsSmartPhone)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            data = androidJavaObject.Call<Data>("getAccData");
            var acc = new Vector3(data.x, data.y, data.z);
#else
            var acc = Input.acceleration;
#endif

            var gyroAngle = Input.gyro.attitude.eulerAngles;

            //view.RPC("DataAsync", ConnectionManager.GetSmartPhonePlayer(ConnectionManager.ID),
            //    new object[] { acc, gyroAngle });

            //DebugTextShow(acc, gyroAngle);

            Acc = acc;
            GyroAngle = gyroAngle;

            DebugTextShow(Acc, GyroAngle);

        }
    }

    [PunRPC]
    public void DataAsync(Vector3 acc, Vector3 gyroAngle, PhotonMessageInfo info)
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
