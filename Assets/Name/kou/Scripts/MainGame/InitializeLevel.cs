using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    //プレイヤーのスポーン地点
    [SerializeField]
    private Transform[] playerSpawns;
    //プレイヤーPrefab
    [SerializeField]
    private GameObject[] playerPrefab;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    [SerializeField]
    private PlayerUI[] playerUI;

    [SerializeField]
    private ChangeImage changeImage;

    void Start()
    {
        //playerConfigsを基にプレイヤーを配置
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for(int i = 0; i < playerConfigs.Length; i++)
        {
            Debug.Log(i);
            int prefabNum = playerConfigs[i].PlayerPrefabNum;
            var player = Instantiate(playerPrefab[prefabNum], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            player.GetComponent<Mover>().SetPlayerType(playerConfigs[i].PlayerPrefabNum);
            player.GetComponent<Mover>().SetPlayerUI(playerUI[playerConfigs[i].PlayerIndex]);
            changeImage.ChangeSpriteNum(i, playerConfigs[i].PlayerPrefabNum);          
        }
        changeImage.StartChangeImage();
    }

}
