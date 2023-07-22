using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour
{
    public static ApiManager Instance { get; private set; }
    public GameController gameController;
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
    
    public IEnumerator newGame(string playerName,GameObject registration)
    {
        AlertManager.Instance.showLoader();

        WWWForm form = new WWWForm();
        form.AddField("player_name", playerName);

        UnityWebRequest www = UnityWebRequest.Post("https://devtest.meemain.dev/api/MemoryCardGame/newGame", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {

            AlertManager.Instance.show("Server error!");
            Debug.Log(www.error);
        }
        else
        {
            DataManager dataManager = Newtonsoft.Json.JsonConvert.DeserializeObject<DataManager>(www.downloadHandler.text);

            if (dataManager.status)
            {
                registration.SetActive(false);
                gameController.setGridProprietes(dataManager.result);
            }
            else
            {
                AlertManager.Instance.show("Try again later!");
            }
        }

        AlertManager.Instance.hideLoader();
        www.Dispose();
    }
    public IEnumerator logGame(int gameId,string firstCard,string secondCard)
    {
        WWWForm form = new WWWForm();
        form.AddField("game_id", gameId);
        form.AddField("first_card", firstCard);
        form.AddField("second_card", secondCard);

        UnityWebRequest www = UnityWebRequest.Post("https://devtest.meemain.dev/api/MemoryCardGame/logeMove", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            AlertManager.Instance.show("Server error!");
            Debug.Log(www.error);
        }
        else
        {
            DataManager dataManager = Newtonsoft.Json.JsonConvert.DeserializeObject<DataManager>(www.downloadHandler.text);

            if (dataManager.status)
            {
                gameController.updateGame(dataManager.result.match);
            }
            else
            {
                AlertManager.Instance.show("Try again later!");
            }
        }
        www.Dispose();
    }
}
