using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleHumanManager : MonoBehaviour
{

    public GameObject[] walkHuman; 


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        int RandomNum = Random.Range(0,2);
        if(RandomNum == 0)
        {
            Instantiate(walkHuman[Random.Range(0,walkHuman.Length)], new Vector3(3.0f,0.0f, Random.Range(-1.0f, 0.0f)), Quaternion.Euler(0, -90, 0));
        }
        else
        {
            Instantiate(walkHuman[Random.Range(0, walkHuman.Length)], new Vector3(-3.0f, 0.0f, Random.Range(-1.0f, 0.0f)), Quaternion.Euler(0, 90, 0));
        }
        
    }
}
