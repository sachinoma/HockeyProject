using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKou : MonoBehaviour
{
    private GameManagerKou gameManager;

    [SerializeField]
    private bool isLeft;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerKou>();
    }
  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hockey")
        {
            gameManager.HitEvent();
            gameManager.ScorePlus(isLeft, 1);//スコアを1加算
            gameManager.TriggerSpawnHockey();//ホッケー再生成
            Destroy(collision.gameObject);//今衝突しているホッケーをdestroy
        }
    }
}
