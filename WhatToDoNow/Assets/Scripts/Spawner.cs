using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Wave
{
    public int melee;
    public int range;
    public int boss;
}


public class Spawner : MonoBehaviour {

    public List<Wave> lstWave = new List<Wave>();
    public float moveUpdateTime=20f;
	// Use this for initialization
	void Start () {

        StartCoroutine("StartWaves");
        
	}

    IEnumerator StartWaves()
    {
        //while (true)
        for(int i=0; i<lstWave.Count; i++)
        {
            StartWave(i);
            yield return new WaitForSeconds(moveUpdateTime);
        }
    }
    void StartWave(int num)
    {
        //lstWave[num];
        int melee = lstWave[num].melee;
        int range = lstWave[num].range;
        int boss = lstWave[num].boss;
        for (int i=0; i < melee; i++)
        {
            //Instantiate(melee);
        }
        for (int i = 0; i < range; i++)
        {
            //Instantiate(range);
        }
        for (int i = 0; i < boss; i++)
        {
            //Instantiate(boss);
        }
    }
    // Update is called once per frame
    void Update()
    {
	
	}
}
