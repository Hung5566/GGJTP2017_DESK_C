using UnityEngine;
using System.Collections;

public class Wave  {

    /// <summary>
    /// 振福
    /// </summary>
    private float m_Amplitude = 0;
    /// <summary>
    /// 頻率
    /// </summary>
    private float m_Hz = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Amp"></param>震幅
    /// <param name="_Wavelength"></param>波長
    public Wave(float _Amp,float _Wavelength)
    {
        m_Amplitude = _Amp;
        m_Hz =1f/_Wavelength;
    }

    public float GetHz()
    {
        return m_Hz;
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
