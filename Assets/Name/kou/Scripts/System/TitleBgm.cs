using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBgm : MonoBehaviour
{
    // シングルトン
    private static TitleBgm instance;

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
        
    }

}
