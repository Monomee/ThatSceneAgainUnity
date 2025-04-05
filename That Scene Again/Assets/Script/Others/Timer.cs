using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime;
    public float countdownTime;

    PlayerCollide player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollide>();
        remainingTime = countdownTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        if (remainingTime > 10)
        {
            timerText.color = Color.green;
        } else if (remainingTime > 0)
        {
            timerText.color = Color.red;
        } else if (remainingTime < 0)
        {
            remainingTime = countdownTime;
            player.Dead();
        }
        int minute = Mathf.FloorToInt(remainingTime / 60);
        int second = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
}
