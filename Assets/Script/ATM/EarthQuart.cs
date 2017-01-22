using UnityEngine;
using System.Collections;

public class EarthQuart : MonoBehaviour
{

    private int CenterX;
    private int CenterY;
    private int Time;
    private float Power;
    private float NowHight;
    private CubeMap m_CubeMap;
    private int m_who;

    /* public EarthQuart(int x, int y, int _Power)
     {

     }*/
    public void Init(float _Power, int _who)
    {
        Time = 0;
        NowHight = 0;
        Power = _Power;
        m_CubeMap = CubeMap.Instance;
        m_CubeMap.NextEvent += Spread;
        m_who = _who;

    }

    void Start()
    {
        //Init(0.2f);

    }
    void OnTriggerEnter(Collider other)
    {
        CubePoint _CubePoint = other.gameObject.GetComponent<CubePoint>();
        if (_CubePoint != null)
        {
            _CubePoint.SendPow(Power, m_who);
        }

    }

    /// <summary>
    /// 擴散
    /// </summary>
    public void Spread()
    {
        transform.localScale += Vector3.one;
        Power--;
        if (Power <= 0)
        {
            m_CubeMap.NextEvent -= Spread;
            Destroy(this.gameObject);
        }

    }

    public float GetPow()
    {

        return Power;
    }

}
