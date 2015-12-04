///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
/// 攻撃エフェクト(オブジェクト)の移動スクリプト
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
    /// 攻撃用のスプライト
    /// </summary>
    [SerializeField]
    Sprite upDownAttackSprite;
    [SerializeField]
    Sprite downUpAttackSprite;
    [SerializeField]
    Sprite leftRightAttackSprite;
    [SerializeField]
    Sprite rightLeftAttackSprite;

    /// <summary>
    /// ITweenの再生中の管理
    /// </summary>
    bool itweenCheck;
    
    /// <summary>
    /// 攻撃用のエフェクトの貼り付け先
    /// </summary>
    SpriteRenderer attackSpriteRenderer;
    private Sprite strongAttackSprite;

    /// <summary>
    /// 動作から取得したIDを取得
    /// </summary>
    MotionManager.MotionSkillType type;

    /// <summary>
    /// プレイヤーの挙動を受け取り攻撃タイプを判別
    /// </summary>
    /// <param name="attackType">動作から受け取った行動ID</param>
    public void OnObject(MotionManager.MotionSkillType attackType)
    {
        //　エフェクトオブジェクトをアクティブ化
        gameObject.SetActive(true);

        //　エフェクトがどの攻撃タイプか情報を保存
        type = attackType;

        //　エフェクトのターゲットを設定
        targetPosition = new Vector3( player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        //　動きから受け取ったタイプからテクスチャなどをオブジェクトに張り付ける
        switch (attackType)
        {
            case MotionManager.MotionSkillType.VERTICAL_UP_DOWN:
                if (itweenCheck != false) break;
                attackSpriteRenderer.sprite = upDownAttackSprite;
                Move();
                break;
            case MotionManager.MotionSkillType.VERTICAL_DOWN_UP:
                if (itweenCheck != false) break;
                attackSpriteRenderer.sprite = downUpAttackSprite;
                Move();
                break;
            case MotionManager.MotionSkillType.HORIZONTAL_LEFT_RIGHT:
                if (itweenCheck != false) break;
                attackSpriteRenderer.sprite = leftRightAttackSprite;
                Move();
                break;
            case MotionManager.MotionSkillType.HORIZONTAL_RIGHT_LEFT:
                if (itweenCheck != false) break;
                attackSpriteRenderer.sprite = rightLeftAttackSprite;
                Move();
                break;
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

        // ITweenが無効になっているフラグたて
        itweenCheck = false;

        //オブジェクトの非アクティブ化
        gameObject.SetActive(false);
    }

    /// <summary>
    /// エフェクトの動きに関する挙動(ITween使用)
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

        //　プレイヤーとエフェクトのX、Y座標を同期
        targetPosition.x = player.transform.position.x;
        targetPosition.y = player.transform.position.y;

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
