using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeMap : MonoBehaviour
{
    buildingReaction BuildingReaction;
    public GameObject grassCube;

    public delegate void VoidDelegate();
    public delegate void NextDelegate(int t);
    public VoidDelegate NextEvent;
    public VoidDelegate ShowEvent;
    public static CubeMap Instance;
    [SerializeField]
    private int m_SizeX = 10;
    [SerializeField]
    private int m_SizeY = 10;
    public CubePoint[,] map;
    GameObject m_cube;
    GameObject m_cube2;

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
        m_cube2 = Resources.Load("Cube (1)") as GameObject;
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
                GameObject tmp = m_cube;
                if (Random.Range(0, 2) == 0)
                    tmp = m_cube2;
                GameObject _Obj = (Instantiate(tmp, new Vector3(i, 0, j), tmp.transform.rotation) as GameObject);

                _Obj.transform.parent = transform;
                map[i, j] = _Obj.GetComponent<CubePoint>();
                map[i, j].Init(i, j);
                _Obj.name = "Cube_"+i+"_"+j;
                //eaihung
                _Obj.AddComponent<cubeAuto>();
                if (i >= m_SizeX/2 - 2 && i <= m_SizeX/2 + 2 && j >= m_SizeX/2 - 2 && j <= m_SizeX/2 + 2)
                {
                    BuildingReaction.locationStr.Add(_Obj);
                }
            }
        }
        //for (int i = m_SizeX * 2; i > -m_SizeX; i--)
        //{
        //    for (int j = -1; j > -m_SizeY/4; j--)
        //    {
        //        GameObject _Obj = (Instantiate(grassCube, new Vector3(i, -0.5f, j), grassCube.transform.rotation) as GameObject);
        //    }
        //}

        //for (int i = m_SizeX * 2; i > -m_SizeX; i--)
        //{
        //    for (int j = m_SizeY; j < 1.25f * m_SizeY; j++)
        //    {
        //        GameObject _Obj = (Instantiate(grassCube, new Vector3(i, -0.5f, j), grassCube.transform.rotation) as GameObject);
        //    }
        //}
        //for (int i = -1; i > -m_SizeX; i--)
        //{
        //    for (int j = 0; j < m_SizeY; j++)
        //    {
        //        GameObject _Obj = (Instantiate(grassCube, new Vector3(i, -0.5f, j), grassCube.transform.rotation) as GameObject);
        //    }
        //}

        //for (int i = m_SizeX; i < 2*m_SizeX; i++)
        //{
        //    for (int j = 0; j < m_SizeY; j++)
        //    {
        //        GameObject _Obj = (Instantiate(grassCube, new Vector3(i, -0.5f, j), grassCube.transform.rotation) as GameObject);
        //    }
        //}


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


    public void CreatEarthQuake(Vector3 _Pos ,int _who)
    {
        GameObject _EQ= Instantiate(Earth, _Pos, Quaternion.identity) as GameObject;
        //m_EarthQuake.Add(new EarthQuart(x, y, 5));
        _EQ.GetComponent<EarthQuart>().Init(120,_who);
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
