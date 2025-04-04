using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    const string STR_IS_WALK = "isWalk";
    const string STR_IS_JUMP = "isJump";
    public Animator animatorController;

    [SerializeField] MovementController moveCtrl;
    int y;
    // Update is called once per frame
    void Update()
    {
        MoveAnimationActive();
        JumpAnimationActive();
    }

    void MoveAnimationActive()
    {
        //animation
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Walk");
            animatorController.SetBool(STR_IS_WALK, true);
        }
        else
        {
            animatorController.SetBool(STR_IS_WALK, false);
        }

        //direction
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            y = (!GameManager.Instance.reverse) ? 0 : 180;  
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            y = (!GameManager.Instance.reverse) ? 180 : 0;
        }
        this.transform.rotation = new Quaternion(0, y, 0, 0);
    }
    public void JumpAnimationActive()
    {
        if (Input.GetKeyDown(KeyCode.Space) && moveCtrl.isOnGround)
        {
            //Debug.Log("Jump");
            animatorController.SetTrigger(STR_IS_JUMP);
        }
    }

}
