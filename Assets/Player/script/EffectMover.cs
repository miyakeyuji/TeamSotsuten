///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour {

    /// <summary>
    /// 標的となるものを登録
    /// </summary>
    [SerializeField]
    GameObject target;

    /// <summary>
    /// プレイヤーオブジェクトを登録
    /// </summary>
    [SerializeField]
    GameObject player;

    /// <summary>
    /// 攻撃エフェクトの移動速度
    /// </summary>
    [SerializeField]
    float speed = 5.0f;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
        /// ↑キーで攻撃
	    if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            // プレイヤーの位置までいったん移動
            gameObject.transform.position = player.transform.position;
            //　targetに向かって等速で移動
            iTween.MoveTo(gameObject, iTween.Hash("position", target.transform.position, "speed", speed, "easetype", iTween.EaseType.linear));
        }
    }
}
