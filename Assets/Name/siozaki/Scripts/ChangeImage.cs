using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private int[] spriteNum;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeSpriteNum(int sprNum, int prefabNum)
    {
        this.spriteNum[sprNum] = prefabNum;
    }
    public void StartChangeImage()
    {
        int num = 0;
        foreach (var image in images)
        {
            image.sprite = sprites[spriteNum[num]];
            num++;
        }
    }
}
