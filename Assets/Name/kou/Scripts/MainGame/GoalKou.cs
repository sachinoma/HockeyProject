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
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hockey")
        {
            gameManager.HitEvent();
            gameManager.ScorePlus(isLeft, 1);//�X�R�A��1���Z
            gameManager.TriggerSpawnHockey();//�z�b�P�[�Đ���
            Destroy(other.gameObject.transform.parent.gameObject);//���Փ˂��Ă���z�b�P�[��destroy
        }
    }
}
