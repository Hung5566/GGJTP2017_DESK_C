using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour {
    public int Max;
    public int Now;
    private Image home;
    private Image crack;
    void Start()
    {
        home = this.GetComponent<Image>();
        crack = this.transform.FindChild("Crack").GetComponent<Image>();
        Now = Max;
    }
    void Update()
    {
        crack.fillAmount = Mathf.Lerp(crack.fillAmount, 1 - ((float)Now / Max), Time.deltaTime * 10f);
        if (Now < 20)
            home.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1f));
        else
            home.color = Color.white;
    }
    public void SetDamage(int damge)
    {
        Now = Mathf.Clamp(Now - damge, 0, Max);
    }
    
}
