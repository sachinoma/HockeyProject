using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    [SerializeField] GameManagerKou gameManager;

    public void Finish()
    {
        gameManager.Finish();
    }
}
