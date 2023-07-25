using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ReSpawnHockey : MonoBehaviour
{
    private GameManagerKou gameManager;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerKou>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hockey")
        {
            gameManager.SpawnHockey();//ホッケー再生成
            Destroy(other.gameObject.transform.parent.gameObject);//今衝突しているホッケーをdestroy
        }
    }
}
