using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    /// <summary>
    /// エフェクトを呼び出すためのクラスを情報
    /// </summary>
    [SerializeField]
    public EffectMover effectMover;

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
        // ↑矢印ボタン入力
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            effectMover.OnObject("強");
        }

        // ↑矢印ボタン入力
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            effectMover.OnObject("弱");
        }
    }
}
