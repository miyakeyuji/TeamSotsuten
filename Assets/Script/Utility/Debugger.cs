//-------------------------------------------------------------
//  デバッグ機能をラッピングしたスクリプト
//
//  code by m_yamada
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Debugger : MonoBehaviour 
{
    /// <summary>
    /// ログにメッセージを表示させる。
    /// </summary>
    /// <param name="message"></param>
    public static void Log(string message)
    { 
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }

    /// <summary>
    /// ログにエラーを表示させる。
    /// </summary>
    /// <param name="message"></param>
    public static void LogError(string message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
#endif
    }

    /// <summary>
    /// ログに警告を表示させる。
    /// </summary>
    /// <param name="message"></param>
    public static void LogWarning(string message)
    {
#if UNITY_EDITOR
        Debug.LogWarning(message);
#endif
    }
}
