using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyPadScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    string numberStr;
    const string ANSWER = "2005";
    [SerializeField] GameObject blank;
    private void Awake()
    {
        input.text = string.Empty;
    }
    public void inputText()
    {
        input.text += numberStr;
        if(input.text.Length > 20)
        {
            input.text = "Too long!";
        }
    }
    public void setNumberStr(string numberStr)
    {
        this.numberStr = numberStr.Trim();
    }
    public void Clear()
    {
        input.text = string.Empty;
    }
    public void Enter()
    {
        if (input.text.Equals(ANSWER))
        {
            input.text = "Correct answer!";
            blank.SetActive(false);
        }
        else
        {
            input.text = "Wrong answer!";
        }
    }
}
