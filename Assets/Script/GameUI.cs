using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    [SerializeField]
    private Sprite[] _newspapers = new Sprite[2];
    private Image newspaper;
    private Animator anim_newspaper;
    public enum Result { Success , Lose };
    
    void Start ()
    {
        newspaper = GameObject.Find("newspaper").GetComponent<Image>();
        newspaper.color = new Color(1, 1, 1, 0);
        newspaper.enabled = false;
        anim_newspaper = newspaper.GetComponent<Animator>();
    }
    public void IsGameOver(Result result)
    {
        
        switch (result)
        {
            case Result.Success:
                newspaper.sprite = _newspapers[1];
                break;
            case Result.Lose:
                newspaper.sprite = _newspapers[0];
                break;
            default:
                break;
        }
        newspaper.enabled = true;
        anim_newspaper.SetBool("IsEnding", true);
        StartCoroutine(DisplayImage(newspaper));


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
