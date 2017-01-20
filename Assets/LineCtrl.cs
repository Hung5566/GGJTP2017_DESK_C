using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LineCtrl : MonoBehaviour
{
    private LineRenderer m_LineRender;
    private int LinePos;
    [SerializeField]
    private int Amount;
    [SerializeField]
    private float m_Time = 0;
    private List<float> m_PosList = new List<float>();
    private Wave NextWave;
    private float m_t;
    private float Size;


    void Awake()
    {
        WaveIn(new Wave(5));
        m_LineRender = GetComponent<LineRenderer>();
        m_LineRender.SetVertexCount(Amount);
        for (int i = 0; i < Amount; i++)
        {
            m_LineRender.SetPosition(i, new Vector3(i,0, 0));
            m_PosList.Add(0);
        }
        Size = 1;

    }



    public void WaveIn(Wave _Wav)
    {
        NextWave=_Wav;
        m_t = -1;

    }
    // Use this for initialization
    void Start()
    {

    }

    private void NextTime()
    {
        m_PosList.RemoveAt(0);
        m_PosList.Add(NextWave.GetPos(m_t));
        m_t += 0.1f;
        if(m_t>=1)
        {
            WaveIn(new Wave(Size));
            Size += 0.5f;
        }

    }



    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= 0.1f)
        {
            m_Time = 0;
            NextTime();
        }
        SetPoint();
    }

    private void SetPoint()
    {
        for (int i = 0; i < Amount; i++)
        {
            m_LineRender.SetPosition(i, new Vector3(i, m_PosList[i], 0));
        }


    }

}
