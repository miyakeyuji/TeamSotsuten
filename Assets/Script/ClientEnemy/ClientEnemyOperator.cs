//-------------------------------------------------------------
// データのやり取りを管理するクラス
//
// code by Gai Takakura
//-------------------------------------------------------------
using UnityEngine;
using System.Collections;

// サーバーとの通信を行う
public class ClientEnemyOperator : MonoBehaviour {
    /// <summary>
    /// 生成する攻撃
    /// </summary>
    [SerializeField]
    GameObject prefav = null;
    GameObject createdAttack = null;

    private float nextTime;
    public float interval = 1.0f;   // 点滅周期

    /// <summary>
    /// エネミーのID
    /// </summary>
    [SerializeField]
    private int id = -1;
    public int ID
    {
        get
        {
            return id;
        }
        set
        {
            if (id == -1) id = value;
        }
    }

    bool isLive = false;
    SpriteRenderer spriteRenderer = null;
    float flashingTimer = 0f;       // 点滅する時間を管理
    public float flashTime = 3f;    // 点滅する時間

    [SerializeField]
    Color defaultColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    Color flashColor = new Color(1f, 0f, 0f, 1f);

    // Use this for initialization
    void Start()
    {
        nextTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        //spriteRenderer.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        // 確認用
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Hit();
        }

        if(spriteRenderer != null)
        {
            if(flashingTimer > 0)
            {
                if (Time.time > nextTime)
                {
                    if(spriteRenderer.color != defaultColor)
                    {
                        spriteRenderer.color = defaultColor;
                    }
                    else
                    {
                        spriteRenderer.color = flashColor;
                    }
                    nextTime += interval;
                }
                flashingTimer -= Time.deltaTime;
                if (flashingTimer <= 0)
                {
                    flashingTimer = 0;
                    spriteRenderer.color = defaultColor;
                }

            }
        }
        else
        {
            Debug.Log("SpriteRendererがありません");
        }
    }

    /// <summary>
    /// 発生
    /// </summary>
    public void Spawn()
    {
        isLive = true;

        var hash = new Hashtable();
        {
            hash.Add("scale", new Vector3(1f, 1f, 1f)); // 設定するサイズ
            hash.Add("time", 1f);                       // 1秒で行う
            hash.Add("easetype", "easeOutQuad");        // イージングタイプを設定
            hash.Add("onstart", "ChangeActive");        // 初めにメソッドを呼ぶ
        }
        iTween.ScaleTo (this.gameObject, hash);
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead()
    {
        isLive = false;

        var hash = new Hashtable();
        {
            hash.Add("scale", new Vector3(0f, 0f, 0f)); // 設定するサイズ
            hash.Add("time", 1f);                       // 1秒で行う
            hash.Add("easetype", "easeOutQuad");        // イージングタイプを設定
            hash.Add("oncomplete", "ChangeActive");     // 最後にメソッドを呼ぶ
        }
        iTween.ScaleTo(this.gameObject, hash);
    }

    /// <summary>
    /// 衝突時の処理
    /// </summary>
    public void Hit()
    {
        nextTime = Time.time;
        flashingTimer = flashTime;
    }

    /// <summary>
    /// アクティブ状態を変更する
    /// </summary>
    void ChangeActive()
    {
        this.gameObject.SetActive(isLive);
    }
}
