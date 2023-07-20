using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscExit : MonoBehaviour
{
    // シングルトン
    private static EscExit instance;

    void Awake()
    {
        
        // シングルトンの呪文
        if(instance == null)
        {
            // 自身をインスタンスとする
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // インスタンスが複数存在しないように、既に存在していたら自身を消去する
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // エスケープキーでゲーム終了
        if(Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
