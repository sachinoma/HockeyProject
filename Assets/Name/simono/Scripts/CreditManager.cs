using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
