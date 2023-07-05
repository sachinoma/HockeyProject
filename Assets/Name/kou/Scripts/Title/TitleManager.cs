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
            //playerConfigurationManager���擾
            playerConfigurationManager = GameObject.Find("PlayerConfigurationManager").GetComponent<PlayerConfigurationManager>();
        }       
    }

    //Button�̃C�x���g�ŌĂ�
    public void ToPlayerSetup()
    {
        SceneManager.LoadScene("PlayerSetup");
    }
    //Button�̃C�x���g�ŌĂ�
    public void ToCredit()
    {
        SceneManager.LoadScene("Credit");
    }
}
