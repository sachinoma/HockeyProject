using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField] private GameObject bgmPlayer;

    //最大プレイヤー数
    [SerializeField]
    private int MaxPlayers = 2;

    [SerializeField]
    private int WinnerPlayers;
    private int WinnerPrefabNum;

    [SerializeField]
    private GameObject PressAUi;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        bgmPlayer = GameObject.Find("BGM");
        if(Instance != null)
        {
            Debug.Log("trying to creat another singleton!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColor(int index,Material color)
    {
        playerConfigs[index].PlayerMaterial = color;
    }

    public void SetPlayerPrefab(int index, int prefabNum)
    {
        playerConfigs[index].PlayerPrefabNum = prefabNum;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if(playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            GameStart();
        }
    }

    private void GameStart()
    {
        Destroy(bgmPlayer);
        LoadScene();
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    //playerConfigsを格納しているリストを空にする
    public void ListEmpty() 
    {
        playerConfigs.RemoveAll(p => p != null);
    }
    //プレイヤーがJoinした時呼び出せる、もしプレイヤーがの数がMaxPlayer以下ならplayerConfigsを作る。
    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex) && playerConfigs.Count < MaxPlayers)
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
            PressAUi.SetActive(false);
        }
    }

    //MaxPlayerを返す
    public int GetMaxPlayer()
    {
        return MaxPlayers;
    }

    public void SetWinner(int num)
    {
        WinnerPlayers = num;
        WinnerPrefabNum = playerConfigs[num].PlayerPrefabNum;
    }

    public int GetWinner()
    {
        return WinnerPlayers;
    }

    public int GetWinnerPrefabNum()
    {
        return WinnerPrefabNum;
    }
}

//playerConfigsクラス
public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMaterial { get; set; }

    public int PlayerPrefabNum { get; set; }
}
