using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeMap : MonoBehaviour
{
    buildingReaction BuildingReaction;


    public delegate void VoidDelegate();
    public delegate void NextDelegate(int t);
    public VoidDelegate NextEvent;
    public VoidDelegate ShowEvent;
    public static CubeMap Instance;
    [SerializeField]
    private int m_SizeX = 10;
    [SerializeField]
    private int m_SizeY = 10;
    CubePoint[,] map;
    GameObject m_cube;
    [SerializeField]
    GameObject Earth;
    private float m_Clock;
    private int m_Time;
    private List<EarthQuart> m_EarthQuake = new List<EarthQuart>();

    void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        m_Time = 0;
        m_cube = Resources.Load("Cube") as GameObject;
        InitMap();
    }

    public void DestoryEarthQuart(EarthQuart _E)
    {
        m_EarthQuake.Remove(_E);



    }
    public void InitMap()
    {
        BuildingReaction = GameObject.Find("building").GetComponent<buildingReaction>();
        BuildingReaction.locationStr = new List<GameObject>();

        map = new CubePoint[m_SizeX, m_SizeY];
        for (int i = 0; i < m_SizeX; i++)
        {
            for (int j = 0; j < m_SizeY; j++)
            {
                GameObject _Obj = (Instantiate(m_cube, new Vector3(i, 0, j), m_cube.transform.rotation) as GameObject);
                _Obj.transform.parent = transform;
                map[i, j] = _Obj.GetComponent<CubePoint>();
                map[i, j].Init(i, j);

                if (i >= m_SizeX/2 - 2 && i <= m_SizeX/2 + 2 && j >= m_SizeX/2 - 2 && j <= m_SizeX/2 + 2)
                {
                    BuildingReaction.locationStr.Add(_Obj);
                }
            }
        }
    }

    /// <summary>
    /// =抓到那個點
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public CubePoint GetPoint(int x, int y)
    {

        if (m_SizeX > x && x >= 0 && m_SizeY > y && y >= 0)

            return map[x, y];
        else
            return null;

    }


    public void CreatEarthQuake(Vector3 _Pos)
    {
        GameObject _EQ= Instantiate(Earth, _Pos, Quaternion.identity) as GameObject;
        //m_EarthQuake.Add(new EarthQuart(x, y, 5));
        _EQ.GetComponent<EarthQuart>().Init(120);
    }
    // Update is called once per frame
    void Update()
    {

        m_Clock += Time.deltaTime;

        if (m_Clock >= 0.01)
        {
            m_Clock = 0;
            /*   for (int i = m_EarthQuake.Count- 1; i >= 0; i--)
               {
                   m_EarthQuake[i].Spread();
               }*/
            if (NextEvent != null)
                NextEvent();
            ShowEvent();

        }
    }
}
