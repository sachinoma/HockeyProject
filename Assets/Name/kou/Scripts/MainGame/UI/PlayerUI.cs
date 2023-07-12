using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image[] skillUI;

    public void UpdateUI(int num,float fillNum)
    {
        skillUI[num].fillAmount = fillNum;
    }
}
