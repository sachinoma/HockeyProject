using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    }

    public void SpawnHockey()
    {
        Instantiate(hockey, spawnPos[0].position, Quaternion.identity);
    }

}
