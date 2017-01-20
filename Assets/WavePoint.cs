using UnityEngine;
using System.Collections;

public class WavePoint : MonoBehaviour {

    /// <summary>
    /// 振福
    /// </summary>
    [SerializeField]
    private float m_Amplitude = 0;



    public WavePoint(float _Amp)
    {
        m_Amplitude = _Amp;


    }

    public float GetPos(float t)
    {
        return Mathf.Sin((float)(t)) * m_Amplitude;
    }



}
