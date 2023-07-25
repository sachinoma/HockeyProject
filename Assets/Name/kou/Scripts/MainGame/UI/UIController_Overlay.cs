using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_Overlay : MonoBehaviour
{
    public Transform targetTfm;

    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 1.5f, -1.0f);

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
    }

    void Update()
    {
        myRectTfm.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
    }
}
