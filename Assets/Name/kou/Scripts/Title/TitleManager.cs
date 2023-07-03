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
            //playerConfigurationManagerを取得
            playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        }       
    }

    //Buttonのイベントで呼ぶ
    public void ToPlayerSetup()
    {
        SceneManager.LoadScene("PlayerSetup");
    }
    //Buttonのイベントで呼ぶ
    public void ToCredit()
    {
        //SceneManager.LoadScene("Credit");
    }
}
