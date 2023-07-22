using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DataManager : MonoBehaviour
{
    public bool status;
    public string[] errors;
    public string message;
    public int code;
    public Result result;

  
}
[System.Serializable]
public class Result
{
    public bool match;
    public int game_id;
    public Grid[] grid;
}
[System.Serializable]
public class Grid
{
    public string block;
    public string card;
    public string image;

}
