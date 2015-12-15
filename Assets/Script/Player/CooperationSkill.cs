///-------------------------------------------------------------------------
///
/// code by miyake yuji
///
/// 協力技の管理
/// 
///-------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class CooperationSkill : MonoBehaviour
{
    /// <summary>
    /// 攻撃エフェクトの移動速度
    /// </summary>
    [SerializeField]
    float speed = 5.0f;

    /// <summary>
    /// 目的地となる座標を登録
    /// </summary>
    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject Player1;

    [SerializeField]
    GameObject Player2;

    SpriteRenderer spriteRenderer;

    /// <summary>
    /// エフェクトオブジェクトのスプライト
    /// </summary>
    [SerializeField]
    Sprite sprite;

    /// <summary>
    /// エフェクトオブジェクトを登録
    /// </summary>
    private GameObject effect;

    /// <summary>
    /// 各プレイヤーの行動情報を取得
    /// </summary>
    private bool[] isCooperationArray = new bool[2];

    /// <summary>
    /// 協力技の発動可能時間？？？
    /// </summary>
    private float resetTime;

    /// <summary>
    /// 自分じゃないプレイヤーが協力技のための動きをしているか確認
    /// </summary>
    void IsCooperation()
    {
        Debug.Log("通った？？");
        effect.SetActive(true);
        spriteRenderer.sprite = sprite;
        Move(target.transform.position);
    }

    /// <summary>
    ///　プレイヤーが協力の動きをしてるかを受け取る
    /// </summary>
    public void SetMoveType(int arrayNum , bool PlayerMoveType)
    {
        isCooperationArray[arrayNum] = PlayerMoveType;
        Debug.Log(arrayNum.ToString() + isCooperationArray[arrayNum].ToString());
        Debug.Log(isCooperationArray[0].ToString() + isCooperationArray[1].ToString());
    }

    /// <summary>
    /// 一定時間でフラグをfalseにする
    /// </summary>
    void Reset()
    {
        Debug.Log("reset");
        isCooperationArray[0] = false;
        isCooperationArray[1] = false;
    }

    /// <summary>
    /// オブジェクトのITween再生
    /// </summary>
    void Move(Vector3 targetPosition)
    {
        //　targetPositionに向かって等速で移動
        iTween.MoveTo(effect,
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
	void Start ()
    {
        effect = transform.FindChild("CooperationSkillEffect").gameObject;
        isCooperationArray[0] = false;
        isCooperationArray[1] = false;
        resetTime = 0.0f;
        effect.SetActive(false);
        spriteRenderer = effect.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 毎フレーム呼ばれる。
    /// </summary>
    void Update ()
    {
        // 受け取った動きの情報がプレイヤー同士同じかつどちらかがtrueか確認
        if (isCooperationArray[0] == isCooperationArray[1] && isCooperationArray[0] == true)
        {
            IsCooperation();
        }
    }
}
