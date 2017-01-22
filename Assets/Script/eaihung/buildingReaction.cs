using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingReaction : MonoBehaviour {
    bool dead;

    float gap;

    public float hp ;
    public List<GameObject> locationStr;
    public float power = 1;
    public float m_EarthQuakeT=0;
    public Vector3 originalPos;

    public float dmgCount;
    float tmpDmg;
    float w_str;
	// Use this for initialization
	void Start () {
        gap = 0.2f;

        dead = false;
        w_str = 0;
        dmgCount = 0;
        hp = 100;
        originalPos = transform.position;
        tmpDmg = 0;
	}
    void OnTriggerEnter(Collider other)
    {

        if (!dead)
            if (other.CompareTag("EarthQuake"))
                causeDmg(other.GetComponent<EarthQuart>().GetPow());

    }
    void causeDmg(float _Dmg)
    {
        tmpDmg = _Dmg;
        if (dmgCount == 0)
        {
            dmgCount++;
        }
        else if (dmgCount == 1)
        {
            if (Time.time - m_EarthQuakeT < gap)
            {
                //Debug.LogError("場場");
                dmgCount = 0;
                GetComponent<Animator>().SetTrigger("defense");
            }
        }

        m_EarthQuakeT = Time.time;

       /* float[] str = new float[12];


        float w_strength=0;
        for (int i = 0; i < 12; i++)
        {
            str[i] = Mathf.Abs(locationStr[i].transform.GetChild(0).position.y - locationStr[24 - i].transform.GetChild(0).position.y) * 0.1f;
            w_strength += str[i];
        }
        Debug.Log(locationStr[12].transform.GetChild(0).position.y.ToString());
        transform.position = originalPos +new Vector3(Random.Range(-w_strength, w_strength), Random.Range(-w_strength, w_strength), Random.Range(-w_strength, w_strength));

        hp -= w_strength;*/


            //print("yo");
            //GetComponent<Rigidbody>().AddForce(Random.Range(-5, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)*100);

        //    hp -= (str[0] + str[1] + str[2] + str[3]) * Time.deltaTime * power;
    }
	// Update is called once per frame
	void Update () {
        if (dead)
            return;

        if (Time.time - m_EarthQuakeT > gap)
        {
            if (dmgCount == 1)
            {
                hp -= tmpDmg*0.5f;
                if(hp<=0)
                {
                    explosion();
                    dead = true;
                }
                w_str = tmpDmg * 0.01f;
                dmgCount = 0;
            }
        }
        shaking();
      //  causeDmg();
	}

    void explosion() {
        
        for(int i=0;i<transform.childCount;i++){
            if (transform.GetChild(i).name == "New Text")
                continue;
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
        }
        GetComponent<Rigidbody>().AddExplosionForce(100,transform.position + new Vector3(3,5,3),5);
        Destroy(GetComponent<Rigidbody>());
    }

    void shaking() {
        transform.position = originalPos + new Vector3(Random.Range(-w_str, w_str), Random.Range(-w_str, w_str), Random.Range(-w_str, w_str));
        if (w_str > 0)
            w_str -= Time.deltaTime;
        else
            w_str = 0;
    }

}
