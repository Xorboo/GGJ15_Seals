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
    public float nextWaveTime = 2f;
    public float timeBetwenSpawns = 1f;
    public float initWaveTime;

    public GameObject meleeObj, 
                      rangeObj, 
                      bossObj;
    public Vector3 vec1PositionEnemy,
                   vec2PositionEnemy,
                   vec3PositionEnemy,
                   vec4PositionEnemy;
    private float maxX = 2, minX = 4,
                  maxY = 1, minY = 3;
	// Use this for initialization
	void Start () {

        StartCoroutine("StartWaves");
        
	}

    IEnumerator StartWaves()
    {
        yield return new WaitForSeconds(initWaveTime);
        //while (true)
        for(int i=0; i<lstWave.Count; i++)
        {
            int melee = lstWave[i].melee;
            int range = lstWave[i].range;
            int boss = lstWave[i].boss;
            for (int j = 0; j < melee; j++)
            {
                //Vector3 pos = transform.position;
                /*pos.x += attack.position.x * transform.localScale.x;
                pos.y += attack.position.y * transform.localScale.y;
                pos.z += attack.position.z * transform.localScale.z;*/
                //var obj = Instantiate(attack.bullet, pos, Quaternion.identity) as GameObject;
                //obj.transform.localScale = transform.localScale;
                Instantiate(meleeObj, vec1PositionEnemy, Quaternion.identity);
                yield return new WaitForSeconds(timeBetwenSpawns);
            }
            for (int j = 0; j < range; j++)
            {
                Instantiate(rangeObj, vec2PositionEnemy, Quaternion.identity);
                yield return new WaitForSeconds(timeBetwenSpawns);
            }
            for (int j = 0; j < boss; j++)
            {
                Instantiate(bossObj, vec3PositionEnemy, Quaternion.identity);
                yield return new WaitForSeconds(timeBetwenSpawns);
            }
            yield return new WaitForSeconds(nextWaveTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
	
	}
}
