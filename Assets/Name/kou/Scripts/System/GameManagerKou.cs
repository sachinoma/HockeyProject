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
    AudioSource audioSource;
    [SerializeField]
    private AudioClip spawnHockeySE;
    [SerializeField]
    private AudioClip overtimeSE;
    private bool overtimePlayed = false;

    [SerializeField]
    private Transform[] spawnPos;

    [SerializeField]
    private GameObject hockey;

    [SerializeField]
    float resetTime;

    [SerializeField]
    private int[] score;

    public bool isGoal = false;

    [SerializeField]
    private Text[] scoreText;

    [SerializeField]
    private GameObject overtimeUI;

    [SerializeField] private GameObject finishUI;


    //�^�C�}�[
    public float CountDownTime = 10.0f;  // �J�E���g�_�E���^�C��
    [SerializeField]
    private Text TextCountDown;
    void Start()
    {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();
        playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        audioSource = GetComponent<AudioSource>();
        SpawnHockey();
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        // �J�E���g�_�E���^�C���𐮌`���ĕ\��
        TextCountDown.text = String.Format("{0:00.0}", CountDownTime);
        // �o�ߎ����������Ă���
        CountDownTime -= Time.deltaTime;
        // 0.0�b�ȉ��ɂȂ�����J�E���g�_�E���^�C����0.0�ŌŒ�i�~�܂����悤�Ɍ�����j
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

    //�X�R�A���Z
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
        
        for(int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = (score[i]).ToString();
        }
    }

    //Goal���炱�̃��\�b�h���Ă�
    public void TriggerSpawnHockey()
    {
        SpawnHockey(resetTime);
    }

    //�z�b�P�[�����̒x���o�[�W����
    public void SpawnHockey(float time)
    {
        Invoke("SpawnHockey", time);
    }

    //�z�b�P�[����
    public void SpawnHockey()
    {
        audioSource.PlayOneShot(spawnHockeySE, 0.6f);
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
            finishUI.SetActive(true);
        }
        else
        {
            if(!overtimePlayed)
            {
                overtimeUI.SetActive(true);
                audioSource.PlayOneShot(overtimeSE, 1.0f);
                overtimePlayed = true;
            }         
        }
    }

    //�I����
    public void Finish()
    {
        DestroyPlayer();
        DeActivePlayerConfiguration();
        Load();
    }

    //���U���g�Ɉڍs
    private void Load()
    {
        SceneManager.LoadScene("Result");
    }

    private void DestroyPlayer()
    {
        //Player�^�O������GameObject��z��Ŏ擾�����X�g�֕ϊ�
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("Player").ToList();
        //�擾����GameObject���폜
        gameObjects.ForEach(gameObj => Destroy(gameObj));
    }

    //PlayerConfiguration���f�A�N�e�B�u�ɂ���
    public void DeActivePlayerConfiguration()
    {
        //PlayerConfiguration�^�O������GameObject��z��Ŏ擾�����X�g�֕ϊ�
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("PlayerConfiguration").ToList();
        //������DeactivateInput()���g��
        gameObjects.ForEach(gameObj => gameObj.GetComponent<PlayerInput>().DeactivateInput());
    }

    public void SetWinner(int winnerNum)
    {
        playerConfigurationManager.SetWinner(winnerNum);
    }
}
