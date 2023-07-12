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
    

    // Start is called before the first frame update
    void Start()
    {
        foreach (var image in images)
        {
            image.sprite = sprites[0];
        }
    }
}
