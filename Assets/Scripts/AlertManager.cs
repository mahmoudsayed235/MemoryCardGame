using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class AlertManager : MonoBehaviour
{
    public static AlertManager Instance { get; private set; }
    public GameObject alert;
    public GameObject loader;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void show(string msg)
    {
        alert.SetActive(true);
        alert.transform.GetChild(0).GetComponent<TMP_Text>().text = msg;
        Invoke("hide", 2f);
    }
    void hide()
    {

        alert.SetActive(false);
        alert.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }
    public void showLoader()
    {
        loader.SetActive(true);
       
    }
    public void hideLoader()
    {

        loader.SetActive(false);
       
    }
    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
