using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehaviour : MonoBehaviour
{
    [SerializeField] GameObject theEyeClose;
    [SerializeField] GameObject theEyeOpen;
    [SerializeField] GameObject red;
    [SerializeField] GameObject green;

    [SerializeField] Timer timer;
    PlayerCollide player;
    Transform targetEye;
    Vector3 lastPosition;
    bool isMoving;
    [SerializeField] float tolerance = 5f; // Sai số cho phép
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollide>();
        targetEye = GameObject.FindGameObjectWithTag("Player").transform;
        lastPosition = targetEye.transform.position;
        StartCoroutine(EyeRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Nếu khoảng cách giữa 2 vị trí lớn hơn tolerance thì mới tính là di chuyển
        isMoving = Vector3.Distance(targetEye.transform.position, lastPosition) > tolerance;

        // Cập nhật lastPosition nếu có sự thay đổi đáng kể
        if (isMoving)
        {
            lastPosition = targetEye.transform.position;
        }

        if(theEyeOpen.activeSelf && isMoving)
        {
            Debug.Log("Dead");
            player.Dead();
            timer.remainingTime = timer.countdownTime;
        }
    }

    IEnumerator EyeRoutine()
    {
        bool eyeStatus = false;
        while (true)
        {
            eyeStatus = !eyeStatus;
            theEyeClose.SetActive(eyeStatus);
            green.SetActive(eyeStatus);
            theEyeOpen.SetActive(!eyeStatus);
            red.SetActive(!eyeStatus);
            yield return new WaitForSeconds(Random.Range(3,5));
        }
    }
}
