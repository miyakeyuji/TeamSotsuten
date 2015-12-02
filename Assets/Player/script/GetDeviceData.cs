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

    /// <summary>
    /// Update メソッドが最初に呼び出される前のフレームで呼び出されます
    /// </summary>
    void Start()
    {
        /// デバイスの座標を取得
        gameObject.transform.position = device.transform.position;
    }

    /// <summary>
    /// 毎フレーム呼ばれる。
    /// </summary>
    void Update()
    {
        /// デバイスの座標を取得
        gameObject.transform.position = device.transform.position;
    }
}
