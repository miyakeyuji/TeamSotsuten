///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class GetDeviceData : MonoBehaviour {
    /// <summary>
    /// デバイスの情報を保持するオブジェクトを登録する
    /// </summary>
    [SerializeField]
    GameObject device;

	// Use this for initialization
	void Start ()
    {
        /// デバイスの座標を取得
        gameObject.transform.position = device.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /// デバイスの座標を取得
        gameObject.transform.position = device.transform.position;
    }
}
