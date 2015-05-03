using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    SpriteRenderer renderer;
    public Sprite win;
	// Use this for initialization
	void Start () {
        renderer = GameObject.Find("TehSprite").GetComponent<SpriteRenderer>();
        StartCoroutine("Logo");
	}

    IEnumerator Logo()
    {
        yield return new WaitForSeconds(3);
        renderer.sprite = win;
        renderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
