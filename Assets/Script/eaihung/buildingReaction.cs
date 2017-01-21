using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingReaction : MonoBehaviour {
    public float hp ;
    public List<GameObject> locationStr;
    public float power = 1;

    public Vector3 originalPos;
	// Use this for initialization
	void Start () {
        hp = 100;
        originalPos = transform.position;
	}

    void causeDmg() {
        float[] str = new float[12];


        float w_strength=0;
        for (int i = 0; i < 12; i++)
        {
            str[i] = Mathf.Abs(locationStr[i].transform.GetChild(0).position.y - locationStr[24 - i].transform.GetChild(0).position.y) * 0.1f;
            w_strength += str[i];
        }
        transform.position = originalPos +new Vector3(Random.Range(-w_strength, w_strength), Random.Range(-w_strength, w_strength), Random.Range(-w_strength, w_strength));

        hp -= w_strength * 5;


            //print("yo");
            //GetComponent<Rigidbody>().AddForce(Random.Range(-5, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)*100);

        //    hp -= (str[0] + str[1] + str[2] + str[3]) * Time.deltaTime * power;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            causeDmg();
        }
        causeDmg();
	}
}
