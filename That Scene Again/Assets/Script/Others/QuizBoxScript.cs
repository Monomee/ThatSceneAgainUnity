using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizBoxScript : MonoBehaviour
{
    string[] password = { "..---" , "-----" , "-----" , "....." };
    public Text text;

    [SerializeField] GameObject on;
    [SerializeField] GameObject off;
    bool status = false;
    // Start is called before the first frame update
    void Awake()
    {
        text.text = "";
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetStatus();
        if (status)
        {
            StartCoroutine(passwordDisplay());
        }
    }
    void SetStatus()
    {
        status = !status;
        on.SetActive(status);
        off.SetActive(!status);
    }

    IEnumerator passwordDisplay()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        foreach (string s in password)
        {
            for (int i = 0; i < s.Length; i++)
            {
                text.text += s[i];
                yield return new WaitForSeconds(0.5f);
            }
            text.text = "";
            yield return new WaitForSeconds(1f);
        }
        SetStatus();
        this.GetComponent<BoxCollider2D>().enabled = true;
    }
}
