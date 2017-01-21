using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveGenerator : MonoBehaviour {
    int level;
    int max;
    float timer;
    public GameObject building;
    public List<GameObject> exceptionCube;
    void cubeCounter(GameObject obj) {
        obj.GetComponent<cubeAuto>().setCount(3);
    }
    public GameObject getCube(int x,int y) {
        return new GameObject();
    }

	// Use this for initialization
	void Start () {
        level = 1;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (level == 1)
        {
            level_1();
        }
	}
    void level_1() {
        timer += Time.deltaTime;
        if(timer == 3)
            cubeCounter(randomCubeSelector());
        if (timer == 7)
            cubeCounter(randomCubeSelector());
        if (timer == 11)
            cubeCounter(randomCubeSelector());
        if (timer == 15)
            cubeCounter(randomCubeSelector());
    }
    GameObject randomCubeSelector() {
        GameObject obj = getCube(Random.Range(0, max), Random.Range(0, max));
        for (int i = 0; i < exceptionCube.Count; i++) {
            if (exceptionCube[i] == obj) {
                i = 0;
                obj = getCube(Random.Range(0, max), Random.Range(0, max));
            }
        }
        return obj;
    }
}
