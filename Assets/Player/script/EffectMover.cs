///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour {

    /// <summary>
    /// 目的地となる座標を登録
    /// </summary>
    [SerializeField]
    Vector3 targetPosition;

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

    /// <summary>
    /// 弱攻撃用のスプライト
    /// </summary>
    [SerializeField]
    Sprite weakAttackSprite;

    /// <summary>
    /// 強攻撃用のスプライト
    /// </summary>
    [SerializeField]
    Sprite strongAttackSprite;

    /// <summary>
    /// ITweenの再生中の管理
    /// </summary>
    bool itweenCheck;
    
    /// <summary>
    /// 攻撃用のエフェクトの貼り付け先
    /// </summary>
    SpriteRenderer attackSpriteRenderer;

    /// <summary>
    /// プレイヤーの挙動を受け取り攻撃タイプを判別
    /// </summary>
    /// <param name="attackType"></param>
    public void OnObject(string attackType)
    {
        gameObject.SetActive(true);
        targetPosition.x = player.transform.position.x;
        switch (attackType)
        {
            case "強":
                if (itweenCheck == false)
                {
                    attackSpriteRenderer.sprite = strongAttackSprite;
                    Move();
                }
                break;
            case "弱":
                if (itweenCheck == false)
                {
                    attackSpriteRenderer.sprite = weakAttackSprite;
                    Move();
                }
                break;
        }

        itweenCheck = true;
    }

    /// <summary>
    /// ITween開始時にプレイヤーとエフェクトのX、Z座標を合わせる
    /// エフェクトの出現位置の調整
    /// </summary>
    void ItweenOnStart()
    {
        gameObject.transform.position = new Vector3(
            player.transform.position.x,
            gameObject.transform.position.y,
            player.transform.position.z
            );
    }

    /// <summary>
    /// Move()のITweenが終了した時の座標修正とオブジェクトの非アクティブ化
    /// </summary>
    void ItweenOnComplete()
    {
        //プレイヤーの位置まで戻す
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            player.transform.position.z);

        itweenCheck = false;

        //オブジェクトの非アクティブ化
        gameObject.SetActive(false);
    }

    /// <summary>
    /// エフェクトの動きに関する挙動
    /// </summary>
    void Move()
    {
        //　targetPositionに向かって等速で移動
        iTween.MoveTo(gameObject, 
            iTween.Hash(
            "position", targetPosition, 
            "speed", speed,
            "easetype", iTween.EaseType.linear,
            "onstart","ItweenOnStart",
            "oncomplete","ItweenOnComplete"
            ));
    }

    /// <summary>
    /// Update メソッドが最初に呼び出される前のフレームで呼び出されます
    /// </summary>
    void Start()
    {
        //　エフェクト用のテクスチャを貼るコンポーネントを取得
        attackSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // プレイヤーの位置までいったん移動
        gameObject.transform.position = player.transform.position;
        
        // プレイヤーのX座標と終着点のX座標を合わせて出現地点から一直線に進ませる
        targetPosition.x = player.transform.position.x;
        targetPosition.y = player.transform.position.y;

        // オブジェクトの非アクティブ化
        gameObject.SetActive(false);

        // ITween用のフラグ初期化
        itweenCheck = false;
    }

    /// <summary>
    /// 毎フレーム呼ばれる。
    /// </summary>
    void Update()
    {

    }
}
