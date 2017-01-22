using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField]
    private Sprite[] newspaper = new Sprite[2];
    // Use this for initialization
    void Start ()
    {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator DisplayImage(Image ig)
    {
        while (ig.color.a < 1)
        {
            ig.color = new Color(ig.color.r, ig.color.g, ig.color.b, Mathf.Clamp(ig.color.a + 0.1f, 0f, 1f));
            yield return new WaitForSeconds(0.03f);
        }

    }
}
