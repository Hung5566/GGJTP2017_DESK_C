using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour {
    public GameObject building;
    public int Max;
    //public int Now;
    private Image home;
    private Image crack;
    void Start()
    {
        building = GameObject.Find("building");

        home = this.GetComponent<Image>();
        crack = this.transform.FindChild("Crack").GetComponent<Image>();
        building.GetComponent<buildingReaction>().hp = Max;
    }
    void Update()
    {
        crack.fillAmount = Mathf.Lerp(crack.fillAmount, 1 - ((float)building.GetComponent<buildingReaction>().hp / Max), Time.deltaTime * 10f);
        if (building.GetComponent<buildingReaction>().hp < 20)
            home.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1f));
        else
            home.color = Color.white;
    }
    public void SetDamage(int damge)
    {
        building.GetComponent<buildingReaction>().hp = Mathf.Clamp(building.GetComponent<buildingReaction>().hp - damge, 0, Max);
    }
    
}
