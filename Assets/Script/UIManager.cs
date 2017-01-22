using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public List<GameObject> page = new List<GameObject>();
    public int index = 0;
    private Canvas m_Canvas;
    public Button btn_Start;
    public Button btn_Team;
    public Button btn_Menu;
	void Start ()
    {
        m_Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        page.Add(m_Canvas.transform.FindChild("Main").gameObject);
        page.Add(m_Canvas.transform.FindChild("Team").gameObject);
        page[1].SetActive(false);      
        //button add Listener
        btn_Start.onClick.AddListener(delegate() { StartGame("EarthQuakeProject"); });
        btn_Team.onClick.AddListener(delegate () { changePage(1); });
        btn_Menu.onClick.AddListener(delegate () { changePage(0); });

    }
    public void changePage(int next)
    {
        page[index].SetActive(false);
        page[next].SetActive(true);
        index = next;
    }
	
    public void StartGame(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }


    IEnumerator Close(Button btn)
    {
        Image ig = btn.image;
        while (ig.color.a > 0)
        {
            ig.color = new Color(ig.color.r, ig.color.g, ig.color.b,Mathf.Clamp(ig.color.a - 0.1f,0f,1f) );
            Debug.Log(ig.color.a);
            yield return new WaitForSeconds(0.03f);
        }
        btn.enabled = false;
    }
}
