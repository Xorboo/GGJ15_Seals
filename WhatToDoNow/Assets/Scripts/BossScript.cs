using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("PlaySounds");
	}

    IEnumerator PlaySounds()
    {
        while (true)
        {
            GameObject.Find("Fatty_" + Random.RandomRange(1, 5)).GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(Random.value * 3 + 3);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
