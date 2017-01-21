using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveGenerator : MonoBehaviour {
    public List<GameObject> counterNum;
    public CubeMap cm;

    int level;
    int max;
    float timer;

    int step;
    public GameObject building;
    public List<GameObject> exceptionCube;
    void cubeCounter(GameObject obj) {
        obj.GetComponent<cubeAuto>().setCount(3);
    }
    public GameObject getCube(int x,int y) {
        return cm.map[x, y].gameObject;
    }

	// Use this for initialization
	void Start () {
        max = 49;
        step = 0;
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
        if (timer >= 3 && step == 0)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 7 && step == 1)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 11 && step == 2)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 15 && step == 3)
        {
            cubeCounter(randomCubeSelector()); 
            step++;
        }
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
