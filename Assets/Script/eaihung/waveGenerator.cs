using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveGenerator : MonoBehaviour {
    public List<GameObject> counterNum;
    public CubeMap cm;

    public int level;
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
        level = 0;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (level == 1)
        {
            level_1();
        }
        else if (level == 2) {
            level_2();
        }
	}
    void level_1() {
        timer += Time.deltaTime;
        if (timer >= 3 && step == 0)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 8 && step == 1)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 13 && step == 2)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 18 && step == 3)
        {
            cubeCounter(randomCubeSelector()); 
            step++;
        }

        if (timer >= 23 && step == 4)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
    }

    void level_2()
    {
        timer += Time.deltaTime;
        if (timer >= 1 && step == 0)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 3 && step == 1)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 5 && step == 2)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 7 && step == 3)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }

        if (timer >= 9 && step == 4)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }

        if (timer >= 11 && step == 5)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 13 && step == 6)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 15 && step == 7)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 17 && step == 8)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
        if (timer >= 19 && step == 9)
        {
            cubeCounter(randomCubeSelector());
            step++;
        }
    }
    GameObject randomCubeSelector() {
        int x = Random.Range(0, max);
        int z = Random.Range(0, max);
        GameObject obj = getCube(x, z);
        for (int i = 0; i < exceptionCube.Count; i++) {
            

            if (exceptionCube[i] == obj || x>=22 && x<27 || z>=14 && z<=35) {
                i = 0;
                x = Random.Range(0, max);
                z = Random.Range(0, max);
                obj = getCube(x, z);
            }
        }
        return obj;
    }
}
