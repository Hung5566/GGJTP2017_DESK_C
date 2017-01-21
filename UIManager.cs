using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    
    private Canvas m_Canvas;
    public Button btn_Start;
	void Start ()
    {
        m_Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();      
        //button add Listener
        btn_Start.onClick.AddListener(delegate() { StartGame("Game Screne"); });
	}
	
    public void StartGame(string SceneName)
    {
        //SceneManager.LoadScene(SceneName);
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
