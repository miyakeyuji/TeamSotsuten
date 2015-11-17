//-------------------------------------------------------------
//  テンプレートスクリプト (スクリプトの処理内容を記述する。)
//
//  code by m_yamada (作成者) を記述する。
//-------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class TemplateScript : MonoBehaviour 
{

    /// <summary>
    /// Editorのみ変更可能にする。
    /// </summary>
    [SerializeField]
    int value = 0;

    /// <summary>
    /// 生成した瞬間に呼ばれる処理
    /// 1度だけしか呼ばれない
    /// </summary>
    void Awake()
    { 
    
    }

    /// <summary>
    /// Awakeの1フレーム後に呼ばれる処理
    /// 1度だけしか呼ばれない
    /// </summary>
	void Start () 
    {
	
	}

    /// <summary>
    /// Startの1フレーム後に呼ばれる処理
    /// マイフレーム呼ばれる
    /// </summary>
	void Update () 
    {
	
	}


}
