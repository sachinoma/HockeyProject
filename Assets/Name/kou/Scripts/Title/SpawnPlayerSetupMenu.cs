using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject playerSetupMenuPrefab;
    public PlayerInput input;

    private void Awake()
    {
        //MainLayoutを見つける
        var rootMenu = GameObject.Find("MainLayout");
        if(rootMenu != null )
        {
            //PlayerSetupMenuを生成
            var menu = Instantiate(playerSetupMenuPrefab,rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
        }
    }
}
