using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingReaction : MonoBehaviour {
    public float hp ;
    public List<GameObject> locationStr;
    public float power = 1;
	// Use this for initialization
	void Start () {
        hp = 100;
	}
    void causeDmg() {
        float[] str = new float[4];
        str[0] = 0; //abs(0-8)
        str[1] = 0; //abs(3-5)
        str[2] = 0; //abs(2-6)
        str[3] = 0; //abs(1-7)

        hp -= ( str[0] + str[1] + str[2] + str[3] ) * Time.deltaTime * power;
    }
	// Update is called once per frame
	void Update () {
        causeDmg();
	}
}
