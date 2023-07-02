using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    private int[] score;

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager = gameObject.GetComponent<PlayerInputManager>();
        playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        SpawnHockey();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        Invoke("SpawnHockey", 1.5f);
        Judge();
    }

    public void SpawnHockey()
    {
        Instantiate(hockey, spawnPos[0].position, Quaternion.identity);
    }

    private void Judge()
    {
        if (score[0] >= 1 || score[1] >= 1)
        {
            DestroyPlayer();
            DeActivePlayerConfiguration();
            Load();
        }

    }

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

    public void DeActivePlayerConfiguration()
    {
        //PlayerConfiguration�^�O������GameObject��z��Ŏ擾�����X�g�֕ϊ�
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("PlayerConfiguration").ToList();

        gameObjects.ForEach(gameObj => gameObj.GetComponent<PlayerInput>().DeactivateInput());
    }
}
