using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LineCtrl : MonoBehaviour
{
    private LineRenderer m_LineRender;
    private int LinePos;
    [SerializeField]
    private int Amount;
    private List<float> m_PosList = new List<float>();


    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// 到下一時間
    /// </summary>
    public void NextTime(float Pos)
    {

        m_PosList.RemoveAt(0);
        m_PosList.Add(Pos);
        SetPoint();
    }




    /// <summary>
    /// 初始化
    /// </summary>
   public  void InitLine()
    {
        m_LineRender = GetComponent<LineRenderer>();
        m_LineRender.SetVertexCount(Amount);
        for (int i = 0; i < Amount; i++)
        {
            m_LineRender.SetPosition(i, new Vector3(i, 0, transform.position.z));
            m_PosList.Add(0);
        }
      //  WaveIn(new Wave(Random.Range(1, 10), 0.2f));
    }
    public float GetPosAt(int i)
    {
        return m_PosList[i];

    }

    private void SetPoint()
    {
        for (int i = 0; i < Amount; i++)
        {
            m_LineRender.SetPosition(i, new Vector3(i, m_PosList[i], this.transform.position.z));
        }

    }

}
