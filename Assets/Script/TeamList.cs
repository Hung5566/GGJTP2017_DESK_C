using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamList : MonoBehaviour
{
    public string title;
    public List<Card> team ;
    public GameObject[] cards;
    void Start()
    {
        //card = Resources.Load<GameObject>("Card");
        transform.FindChild("Title").GetComponent<Text>().text = title;
        for (int i = 0; i < team.Count; i++)
        {
            cards[i].transform.FindChild("Picture").GetComponent<Image>().sprite = team[i].picture;
            cards[i].transform.FindChild("Name").GetComponent<Text>().text = team[i].name;
        }
    }
}
[System.Serializable]
public class Card
{
    public string name;
    public Sprite picture;
    
    public Card(string name)
    {
        this.name = name;   
    }
}
