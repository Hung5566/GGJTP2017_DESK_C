using UnityEngine;
using System.Collections;

public class CubePoint: MonoBehaviour
{

    float NowPow = 0;
    int m_x;
    int m_y;

    [SerializeField]
    Transform WatchCube; 

    public void Init(int _x,int  _y)
    {
        m_x = _x;
        m_y = _y;
        NowPow = 0;
    }

    public void SendPow(float _Pow)
    {
        NowPow += _Pow;
    }


    void OnMouseDown()
    {

        CubeMap.Instance.CreatEarthQuake(transform.position);

    }

    public void Show()
    {       
        if (WatchCube.position.y > 0||NowPow>=0)
        {
            WatchCube.position =new Vector3(transform.position.x, (transform.position.y*50+ NowPow)/50f, transform.position.z);
            NowPow-=10;
        }
        else
        {
           WatchCube.position=new Vector3(transform.position.x, 0, transform.position.z);

        }

    }
    private void Start()
    {
        CubeMap.Instance.ShowEvent += Show;
    }

}
