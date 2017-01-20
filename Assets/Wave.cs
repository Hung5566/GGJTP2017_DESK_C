using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

    /// <summary>
    /// 振福
    /// </summary>
    [SerializeField]
    private float m_Amplitude = 0;


    public Wave(float _Amp)
    {

        m_Amplitude = _Amp;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>介於1至-1
    /// <returns></returns>
    public float GetPos(float t)
    {
        return Mathf.Sin(t * 2 * Mathf.PI) * m_Amplitude;
    }



}
