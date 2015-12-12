///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
/// デバイスの情報を取得し、プレイヤーの座標を取得する為のスクリプト
/// 
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetDevicePosition : MonoBehaviour
{
    /// <summary>
    /// デバイスの情報を保持するオブジェクトを登録する
    /// </summary>
    [SerializeField]
    Transform arCameraDevice = null;

    /// <summary>
    /// デバイスの情報を保持するオブジェクトを登録する
    /// </summary>
    [SerializeField]
    Transform singleCameraDevice = null;

    [SerializeField]
    Text debugText = null;

    /// <summary>
    /// Update メソッドが最初に呼び出される前のフレームで呼び出されます
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// 毎フレーム呼ばれる。
    /// </summary>
    void Update()
    {

        /// デバイスの座標を取得
        if (SequenceManager.Instance.IsBuildWatch)
        {
            transform.position = singleCameraDevice.position;
        }
        else
        {
            transform.position = arCameraDevice.position;
        }

        debugText.text = "プレイヤー座標 : " + transform.position;
    }
}
