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

    public AudioClip winSound;
    public AudioClip loseSound;

    void Start ()
    {
        newspaper = GameObject.Find("Ending").GetComponent<Image>();
        newspaper.color = new Color(1, 1, 1, 0);
        newspaper.enabled = false;
        anim_newspaper = newspaper.GetComponent<Animator>();
    }
    public void IsGameOver(bool win)
    {
        if (win)
        {
            newspaper.sprite = _newspapers[1];
            GetComponent<AudioSource>().clip = winSound;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            newspaper.sprite = _newspapers[0];
            GetComponent<AudioSource>().clip = loseSound;
            GetComponent<AudioSource>().Play();
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
