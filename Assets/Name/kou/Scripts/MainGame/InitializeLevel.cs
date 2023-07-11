using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    //�v���C���[�̃X�|�[���n�_
    [SerializeField]
    private Transform[] playerSpawns;
    //�v���C���[Prefab
    [SerializeField]
    private GameObject[] playerPrefab;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    [SerializeField]
    private PlayerUI[] playerUI;

    void Start()
    {
        //playerConfigs����Ƀv���C���[��z�u
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for(int i = 0; i < playerConfigs.Length; i++)
        {
            Debug.Log(i);
            int prefabNum = playerConfigs[i].PlayerPrefabNum;
            var player = Instantiate(playerPrefab[prefabNum], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            player.GetComponent<Mover>().SetPlayerUI(playerUI[playerConfigs[i].PlayerIndex]);
        }
    }

}
