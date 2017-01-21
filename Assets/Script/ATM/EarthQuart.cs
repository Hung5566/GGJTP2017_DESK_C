using UnityEngine;
using System.Collections;

public class EarthQuart : MonoBehaviour { 

    private int CenterX;
    private int CenterY;
    private int Time;
    private float Power;
    private float NowHight;
    private CubeMap m_CubeMap;

    /* public EarthQuart(int x, int y, int _Power)
     {

     }*/
    public void Init(float _Power)
    {
         Time = 0;
         NowHight = 0;
         Power = _Power;
         m_CubeMap = CubeMap.Instance;
        m_CubeMap.NextEvent += Spread;

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
            _CubePoint.SendPow(Power);
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
        
        /*
        ///痕的
        for (int i = CenterX - Time; i <= CenterX + Time; i++)
        {


            CubePoint _CutePoint = m_CubeMap.GetPoint(i, CenterY - Time);

            if (_CutePoint != null)
                _CutePoint.SendPow(NowHight);

            _CutePoint = m_CubeMap.GetPoint(i, CenterY + Time);

            if (_CutePoint != null)
                _CutePoint.SendPow(NowHight);
        }
        ///值得
        for (int i = CenterY - Time + 1; i < CenterY + Time; i++)
        {
            CubePoint _CutePoint = m_CubeMap.GetPoint(CenterX - Time, i);

            if (_CutePoint != null)
                _CutePoint.SendPow(NowHight);

            _CutePoint = m_CubeMap.GetPoint(CenterX + Time, i);

            if (_CutePoint != null)
                _CutePoint.SendPow(NowHight);
        }
        NowHight = NowHight+Vec* 0.5f;
        if (NowHight >= Power)
        {
            Vec = -1;



        }
        Time++;

        if (NowHight <= 0)
        {
            
            m_CubeMap.DestoryEarthQuart(this);

        }

        */

    }

}
