using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

    [SerializeField]
    private LineCtrl EarthWave;
    [SerializeField]
    private LineCtrl PlayerWave;
    [SerializeField]
    private LineCtrl SumWave;
    PlayerCtrl m_PlayerCtrl;
    private Wave NextWave;
    private float m_t;
    [SerializeField]
    private float m_Time = 0;
    // Use this for initialization
    void Awake()
    {
        Init();

    }

    void Start () {
	
	}

    private void Init()
    {
        m_PlayerCtrl =GetComponent<PlayerCtrl>();
        WaveIn();
        EarthWave.InitLine();
        PlayerWave.InitLine();
        SumWave.InitLine();
    }

    public void WaveIn()
    {

        NextWave = new Wave(Random.Range(1, 10), Random.Range(100, 10));
        m_t =0;

    }


    void Next()
    {
        if (m_t >= 1)
        {
            WaveIn();
        }
        m_t += NextWave.GetHz();
        EarthWave.NextTime(NextWave.GetPos(m_t));
        PlayerWave.NextTime(m_PlayerCtrl.GetNowDifference());
        ///2波合成
        SumWave.NextTime(EarthWave.GetPosAt(49) + m_PlayerCtrl.GetNowDifference());


    }


// Update is called once per frame
void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= 0.1f)
        {
            m_Time = 0;
            Next();
        }

    }
}
