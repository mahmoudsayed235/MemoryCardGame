using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RegisterController : MonoBehaviour
{
    public void register(TMP_InputField name)
    {
        if (!string.IsNullOrEmpty(name.text))
        {

            StartCoroutine(ApiManager.Instance.newGame(name.text,this.gameObject));

        }
        else
        {
            AlertManager.Instance.show("Your name is required!");
        }
    }
}
