using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private PlayerConfigurationManager playerConfigurationManager;

    void Start()
    {
        if(playerConfigurationManager != null)
        {
            playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        }       
    }

    public void ToPlayerSetup()
    {
        SceneManager.LoadScene("PlayerSetup");
    }

    public void ToCredit()
    {
        //SceneManager.LoadScene("Credit");
    }
}
