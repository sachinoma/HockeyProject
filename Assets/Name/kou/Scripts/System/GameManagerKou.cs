using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManagerKou : MonoBehaviour
{
    private PlayerConfigurationManager playerConfigurationManager;

    private PlayerInputManager playerInputManager;

    [SerializeField]
    private Transform[] spawnPos;

    [SerializeField]
    private GameObject hockey;

    [SerializeField]
    float resetTime;

    [SerializeField]
    private int[] score;

    public bool isGoal = false;


    //タイマー
    public float CountDownTime = 10.0f;  // カウントダウンタイム
    [SerializeField]
    private Text TextCountDown;
    void Start()
    {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();
        playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        SpawnHockey();
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        // カウントダウンタイムを整形して表示
        TextCountDown.text = String.Format("{0:00.0}", CountDownTime);
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;
        // 0.0秒以下になったらカウントダウンタイムを0.0で固定（止まったように見せる）
        if(CountDownTime <= 0.0F)
        {
            CountDownTime = 0.0F;
            Judge();
        }
    }

    public void HitEvent()
    {
        isGoal = true;
        Invoke("IsGoalFalse", 0.1f);
    }

    private void IsGoalFalse()
    {
        isGoal = false;
    }

    //スコア加算
    public void ScorePlus(bool isLeft,int num)
    {
        if (!isLeft) 
        {
            score[0] += num;
        }
        else
        {
            score[1] += num;
        }       
        
    }

    //Goalからこのメソッドを呼ぶ
    public void TriggerSpawnHockey()
    {
        SpawnHockey(resetTime);
    }

    //ホッケー生成の遅延バージョン
    public void SpawnHockey(float time)
    {
        Invoke("SpawnHockey", time);
    }

    //ホッケー生成
    public void SpawnHockey()
    {
        Instantiate(hockey, spawnPos[0].position, Quaternion.identity);
    }
    
    private void Judge()
    {
        int Player1 = 0;
        int Player2 = 1;

        if (score[0] != score[1])
        {
            if (score[0] > score[1])
            {
                SetWinner(Player1);
            }
            else
            {
                SetWinner(Player2);
            }
            DestroyPlayer();
            DeActivePlayerConfiguration();
            Load();
        }
    }

    //リザルトに移行
    private void Load()
    {
        SceneManager.LoadScene("Result");
    }

    private void DestroyPlayer()
    {
        //PlayerタグをつけたGameObjectを配列で取得しリストへ変換
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("Player").ToList();
        //取得したGameObjectを削除
        gameObjects.ForEach(gameObj => Destroy(gameObj));
    }

    //PlayerConfigurationをデアクティブにする
    public void DeActivePlayerConfiguration()
    {
        //PlayerConfigurationタグをつけたGameObjectを配列で取得しリストへ変換
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("PlayerConfiguration").ToList();
        //公式のDeactivateInput()を使う
        gameObjects.ForEach(gameObj => gameObj.GetComponent<PlayerInput>().DeactivateInput());
    }

    public void SetWinner(int winnerNum)
    {
        playerConfigurationManager.SetWinner(winnerNum);
    }
}
