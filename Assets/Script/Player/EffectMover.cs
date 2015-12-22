///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
/// 攻撃エフェクト(オブジェクト)の移動スクリプト
/// 
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour
{
    /// <summary>
    /// ITweenの再生中の管理
    /// </summary>
    bool itweenCheck;

    /// <summary>
    /// 動作から取得したIDを取得
    /// </summary>
    MotionManager.MotionSkillType type;

    /// <summary>
    /// プレイヤーの挙動を受け取り攻撃タイプを判別
    /// </summary>
    /// <param name="attackType">動作から受け取った行動ID</param>
    public void OnObject(
        MotionManager.MotionSkillType attackType , 
        float speed , 
        GameObject target)
    {
        gameObject.SetActive(true);

        iTween.Stop(gameObject);

        //　エフェクトがどの攻撃タイプか情報を保存
        type = attackType;

        if (itweenCheck == false)
        {
            Move(target.transform.position, speed);
            
        }
        //　ITweenが再生中のフラグを立てる
        itweenCheck = true;
        
    }

    /// <summary>
    /// ITween開始時にプレイヤーとエフェクトのX、Z座標を合わせる
    /// エフェクトの出現位置の調整
    /// </summary>
    void ItweenOnStart()
    {
       // gameObject.transform.position = player.transform.position;
    }

    /// <summary>
    /// Move()のITweenが終了した時の座標修正とオブジェクトの非アクティブ化
    /// </summary>
    void ItweenOnComplete()
    {
        //プレイヤーの位置まで戻す
        //gameObject.transform.position = player.transform.position;

        // ITweenが無効になっているフラグたて
        itweenCheck = false;

        //オブジェクトの非アクティブ化
        gameObject.SetActive(false);
    }

    /// <summary>
    /// エフェクトの動きに関する挙動(ITween使用)
    /// </summary>
    void Move(Vector3 targetPosition ,float speed)
    {
        //　targetPositionに向かって等速で移動
        iTween.MoveTo(gameObject,
            iTween.Hash(
            "position", targetPosition,
            "speed", speed,
            "easetype", iTween.EaseType.linear,
            "onstart", "ItweenOnStart",
            "oncomplete", "ItweenOnComplete"
            ));
    }

    /// <summary>
    /// Update メソッドが最初に呼び出される前のフレームで呼び出されます
    /// </summary>
    void Start()
    {
        // プレイヤーの位置までいったん移動
       // gameObject.transform.position = player.transform.position;

        // オブジェクトの非アクティブ化
        gameObject.SetActive(false);

        // ITween用のフラグ初期化
        itweenCheck = false;

        // 攻撃タイプの初期化
        type = MotionManager.MotionSkillType.NONE;
    }

    /// <summary>
    /// 毎フレーム呼ばれる。
    /// </summary>
    void Update()
    {

    }
}
