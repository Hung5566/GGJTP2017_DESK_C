using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubeAuto : MonoBehaviour {
    public Text testText;

    public waveGenerator wg;
    public GameObject hintIcon;
    public GameObject indicator;

    public GameObject myCounterNum;

    public float timer;
    public bool startCount;
    public int duration;
    public int shownNum;
    private Color myColor;

    public void setCount(int Duration) {
        //hintIcon.transform.position = transform.position + new Vector3(0,2,0);
        indicator = Instantiate(GameObject.Find("indicator"), transform.position + new Vector3(0, 1f, 0), GameObject.Find("indicator").transform.rotation) as GameObject;
        indicator.transform.localScale = new Vector3(10,10,5);

        duration = Duration;
        timer = duration + 1;

        startCount = true;
    }
    public void cubeCounting()
    {// 倒數
        if (startCount)
        {
            timer -= Time.deltaTime;

            showNumImg((int)timer);
            if ((int)timer <= 0) {
                startCount = false;
                timer = 0;
                Destroy(indicator);
                waveGen();
            }
        }
    }
    void waveGen() {

        wg.GetComponent<CubeMap>().CreatEarthQuake(transform.position,-1);
    }
    public void showNumImg(int num) {
        myCounterNum.GetComponent<SpriteRenderer>().sprite = wg.counterNum[num].GetComponent<SpriteRenderer>().sprite;
        if (num == 0)
            myCounterNum.GetComponent<SpriteRenderer>().sprite = null;
    }
	// Use this for initialization
	void Start () {
        wg = GameObject.Find("GameSystem").GetComponent<waveGenerator>();
        //indicator = new GameObject();
        //indicator = Instantiate(GameObject.Find("indicator"), transform.position + new Vector3(0, 2, 0), GameObject.Find("indicator").transform.rotation) as GameObject;
       
        myCounterNum = new GameObject();
        myCounterNum.transform.parent = transform;
        myCounterNum.AddComponent<SpriteRenderer>();
        myCounterNum.transform.position = transform.position + new Vector3(0,7,0);
        //myCounterNum.GetComponent<SpriteRenderer>().sprite = wg.counterNum[1].GetComponent<SpriteRenderer>().sprite;
        myCounterNum.transform.LookAt(myCounterNum.transform.position - (Camera.main.transform.position - myCounterNum.transform.position));
        timer = 0;
        startCount = false;
	}
	
	// Update is called once per frame
	void Update () {
        cubeCounting();
	}
}
