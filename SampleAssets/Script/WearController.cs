using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WearController : MonoBehaviour {

    [SerializeField]
    ParticleSystem effect = null;

    PhotonView view = null;

    Text text = null;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        view = GetComponent<PhotonView>();

        Input.gyro.enabled = true;

        text.text = "パラメーター";
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.room == null) return;

        TextDataUpdate();
    }

    void TextDataUpdate()
    {
        var acc = Input.acceleration;
        var gyroAngle = Input.gyro.attitude.eulerAngles;

        if (RoomManager.IsWacth)
        {
            view.RPC("DataAsync", PhotonTargets.All, new object[] { acc, gyroAngle });
            view.RPC("EffectAsync", PhotonTargets.Others, new object[] { acc });
        }
    }

    [PunRPC]
    public void DataAsync(Vector3 acc, Vector3 gyroAngle, PhotonMessageInfo info)
    {
        text.text = "Acc : " + acc.ToString() + "\n";
        text.text += "GyroAngle : " + gyroAngle.ToString();
        Debug.Log(text, text);
    }
    
    [PunRPC]
    public void EffectAsync(Vector3 acc, PhotonMessageInfo info)
    {
        if (acc.y <= -0.8f)
        {
            effect.emissionRate = 100;
        }
        else
        {
            effect.emissionRate = 0;
        }
    }

    

    public void ClearText()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }

        effect.emissionRate = 0;
        text.text = " ";
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

}
