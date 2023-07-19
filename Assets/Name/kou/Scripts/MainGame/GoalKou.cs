using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class GoalKou : MonoBehaviour
{
    private GameManagerKou gameManager;

    AudioSource audioSource;
    [SerializeField]
    private AudioClip goal;

    [SerializeField]
    private bool isLeft;

    [SerializeField]
    GameObject goalFX;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerKou>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hockey")
        {
            audioSource.PlayOneShot(goal, 1.0f);
            gameManager.HitEvent();
            gameManager.ScorePlus(isLeft, 1);//�X�R�A��1���Z
            gameManager.TriggerSpawnHockey();//�z�b�P�[�Đ���
            Instantiate(goalFX, transform.position, Quaternion.identity);
            Destroy(other.gameObject.transform.parent.gameObject);//���Փ˂��Ă���z�b�P�[��destroy
        }
    }
}
