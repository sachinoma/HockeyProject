using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] private AudioClip countdown;
    [SerializeField] private AudioClip start;

    private GameManagerKou gameManager;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerKou>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCountDown()
    {
        audioSource.PlayOneShot(countdown, 1.0f);
    }

    public void playStart()
    {
        audioSource.PlayOneShot(start, 1.0f);
    }

    public void changeIsStartCountDownFalse()
    {
        gameManager.SetIsStartCountDown(false);
    }

}
