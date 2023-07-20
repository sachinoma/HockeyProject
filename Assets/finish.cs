using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish : MonoBehaviour
{
    [SerializeField] GameManagerKou gameManager;

    AudioSource audioSource;

    [SerializeField] private AudioClip finishSE;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Finish()
    {
        gameManager.Finish();
    }

    public void PlayFinishSE()
    {
        audioSource.PlayOneShot(finishSE, 1.0f);
    }
}
