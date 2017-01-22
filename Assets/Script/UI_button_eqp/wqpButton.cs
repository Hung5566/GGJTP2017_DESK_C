using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wqpButton : MonoBehaviour {
    public int level;


    public void setLevel() {
        GameObject.Find("GameSystem").GetComponent<waveGenerator>().level = level;
        GameObject.Find("Happy").SetActive(false);
        GameObject.Find("Crazy").SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
