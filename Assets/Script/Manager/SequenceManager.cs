//-------------------------------------------------------------
//  シーケンスのマネージャークラス
//  各オブジェクトの処理をここに記述する。
// 
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// シーンID
/// </summary>
public enum SceneID
{
    CONNECTION,         // コネクション
    TITLE,              // タイトル
    CHARACTER_SELECT,   // キャラクター選択
    GAME,               // ゲーム
    RESULT,             // リザルト
    MAX,
}


public class SequenceManager : Singleton<SequenceManager>
{
    /// <summary>
    /// シーン情報
    /// </summary>
    [System.Serializable]
    public struct SceneInfoData
    {
        public SceneID scene;
        public SequenceBehaviour behaviour;
    }
    
    /// <summary>
    /// シーンリスト
    /// </summary>
    [SerializeField]
    SceneInfoData[] sceneList = new SceneInfoData[(int)SceneID.MAX];

    /// <summary>
    /// 今のシーン情報
    /// </summary>
    [SerializeField]
    SceneID nowScene = SceneID.CONNECTION;

    public override void Awake() 
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        base.Awake();
        
        for (int i = 0; i < sceneList.Length; i++)
        {
            sceneList[i].behaviour.Reset();

            if (sceneList[i].scene == nowScene)
            {
                sceneList[i].behaviour.gameObject.SetActive(true);
            }
        }

    }

    public override void Start() 
    {
        base.Start();

	}

    /// <summary>
    /// 次のシーンに行かせる処理です。
    /// 切り替えたいシーンを引数で設定してください。
    /// </summary>
    /// <param name="nextScene"></param>
    public void ChangeScene(SceneID nextScene)
    {
        // 例外処理 ---------------------------------------------//

        if (nextScene == SceneID.MAX)
        {
            Debugger.LogError("そんなシーンはありません。");
        }

        if (nextScene == nowScene)
        {
            Debugger.LogWarning("同じシーンが設定されています。");
        }

        // -------------------------------------------------------//

        // 次のシーンを表示
        sceneList[(int)nextScene].behaviour.gameObject.SetActive(true);

        // 現在のシーンを非表示
        sceneList[(int)nowScene].behaviour.gameObject.SetActive(false);

        // 現在のシーンの終了処理
        sceneList[(int)nowScene].behaviour.Finish();

        nowScene = nextScene;
    }

    public override void Update()
    {
        base.Update();

        if (Input.touchCount >= 3)
        {
            Application.LoadLevel(0);
        }

    }
}
