using UnityEngine;
using System.Collections;

public class ARObjectController : MonoBehaviour {

    MeshRenderer meshRenderer = null;
    PhotonView view = null;

	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        view = GetComponent<PhotonView>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PhotonNetwork.room == null) return;
        if (RoomManager.IsWacth) return;
        if (!meshRenderer.enabled) return;

        transform.Translate(10 * Time.deltaTime, 0, 0);
        view.RPC("DataAsync", PhotonTargets.All, new object[] { transform.position });
	}

    [PunRPC]
    public void DataAsync(Vector3 pos, PhotonMessageInfo info)
    {
        transform.position = pos;

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

}
