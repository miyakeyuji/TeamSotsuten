// -------------------------------------------------------
//  相手プレイヤー座標を同期するクラス
//
// code by m_yamada
// --------------------------------------------------------

using UnityEngine;
using System.Collections;

public class OtherPlayerSyncPosition : MonoBehaviour 
{
    [SerializeField]
    Transform syncObject = null;

    [SerializeField]
    int otherPlayerID = 0;

    PhotonView view = null;

	// Use this for initialization
	void Start () 
    {
        view = GetComponent(typeof(PhotonView)) as PhotonView;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (ConnectionManager.IsOwner)
        {
            view.RPC("SyncData", ConnectionManager.GetSmartPhonePlayer(otherPlayerID), new object[] { syncObject.position });
        }

        if (ConnectionManager.IsSmartPhone)
        {
            int id = ConnectionManager.ID == 0 ? 1 : 0;
            view.RPC("SyncData", ConnectionManager.GetSmartPhonePlayer(id), new object[] { syncObject.position });
        }
    }

    [PunRPC]
    void SyncData(Vector3 pos,PhotonMessageInfo info)
    {
        transform.position = pos;
    }

}
