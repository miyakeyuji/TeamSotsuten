//-------------------------------------------------------------
//  デバッグ機能をラッピングしたスクリプト
//
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Debugger : MonoBehaviour 
{
    static Text debugText = null;

    void Awake()
    {
        debugText = GetComponent<Text>();
        Reset();
    }

    /// <summary>
    /// デバッグ用のテキストをリセットする。
    /// </summary>
    public static void Reset()
    {
        debugText.text = "";
    }


    /// <summary>
    /// ログにメッセージを表示させる。
    /// </summary>
    /// <param name="message"></param>
    public static void Log(object message)
    { 
#if UNITY_EDITOR
        Debug.Log(message);
#endif
        debugText.text += message + "\n";
    }

    /// <summary>
    /// ログにエラーを表示させる。
    /// </summary>
    /// <param name="message"></param>
    public static void LogError(object message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
#endif

        debugText.text += "<color=red>" + message + "</color>" + "\n";
    }

    /// <summary>
    /// ログに警告を表示させる。
    /// </summary>
    /// <param name="message"></param>
    public static void LogWarning(object message)
    {
#if UNITY_EDITOR
        Debug.LogWarning(message);
#endif

        debugText.text += "<color=yellow>" + message + "</color>" + "\n";

    }
}
