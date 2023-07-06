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


    //�^�C�}�[
    public float CountDownTime = 10.0f;  // �J�E���g�_�E���^�C��
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
        if (isLeft) 
        {
            score[0] += num;
        }
        else
        {
            score[1] += num;
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
        Instantiate(hockey, spawnPos[0].position, Quaternion.identity);
    }
    
    private void Judge()
    {
        if(score[0] != score[1])
        {
            DestroyPlayer();
            DeActivePlayerConfiguration();
            Load();
        }
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
}
