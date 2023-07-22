using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CardController : MonoBehaviour
{
    private Sprite back;
    private Sprite face;
    bool isBack;
    [HideInInspector]
    public string block, card;
   
    public void click()
    {
       StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().selectCard(this.gameObject));
    }
    public void createCard(Grid cardData)
    {
        isBack = true;
        back = GetComponent<Image>().sprite;
        StartCoroutine(DownloadImage(cardData.image));
        block = cardData.block;
        card = cardData.card;
    }
    public void hide()
    {
        GetComponent<Button>().enabled = false;
        GetComponent<Image>().color= new Color(0,0,0,0);
    }
    public void flipCard()
    {
        if (isBack)
        {
            GetComponent<Image>().sprite=face;
        }
        else
        {
            GetComponent<Image>().sprite = back;
        }
        isBack = !isBack;
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        WWW www = new WWW(MediaUrl);
        yield return www;
        face = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
