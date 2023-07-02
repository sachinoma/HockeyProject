using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPLayers = 2;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
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

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if(playerConfigs.Count == MaxPLayers && playerConfigs.All(p => p.IsReady == true))
        {
            ////Player�^�O������GameObject��z��Ŏ擾�����X�g�֕ϊ�
            //List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("Player").ToList();
            ////�擾����GameObject���폜
            //gameObjects.ForEach(gameObj => Destroy(gameObj));

            SceneManager.LoadScene("MainGame");
        }
    }

    public void ListEmpty() 
    {
        playerConfigs.RemoveAll(p => p != null);
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex) && playerConfigs.Count < MaxPLayers)
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

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
}
