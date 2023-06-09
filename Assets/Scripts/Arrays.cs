using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour
{
    public GameObject[] enemies = new GameObject[10];    
    void Start()
    {
    }

    // Update is called once per frame

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Respawn");
       
    }

}
