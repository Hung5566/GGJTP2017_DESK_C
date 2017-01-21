using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubeAuto : MonoBehaviour {
    public Text testText;

    public float timer;
    public bool startCount;
    public int duration;
    public int shownNum;
    private Color myColor;

    public void setCount(int Duration) { 
        
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
                waveGen();
            }
        }
    }
    void waveGen() { 
        
    }
    public void showNumImg(int num) {
        testText.text = "" + num;
        if (num == 0)
            testText.text = "";
    }
	// Use this for initialization
	void Start () {
        timer = 0;
        startCount = false;
	}
	
	// Update is called once per frame
	void Update () {
        cubeCounting();
        if (Input.GetKeyDown(KeyCode.A)) {
            setCount(3);
        }
	}
}
