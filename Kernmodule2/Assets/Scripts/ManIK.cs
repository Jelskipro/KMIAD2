using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class ManIK : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;

    public GameObject touchArea;

    public Transform leftHandObj = null;
    public Transform rightHandObj = null;
    public Transform lookObj = null;

    private Touch touch;

    void Start()
    {
        animator = GetComponent<Animator>();
        touch = touchArea.GetComponent<Touch>();

    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if(touch.isTouchingL == true)
                {
                    if (leftHandObj != null)
                    {

                        Quaternion handRotation = Quaternion.LookRotation(leftHandObj.position - transform.position);
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0F);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0F);
                        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftHand, handRotation);
                    }
                }
                if (touch.isTouchingR == true)
                {
                    if (rightHandObj != null)
                    {

                        Quaternion handRotation = Quaternion.LookRotation(rightHandObj.position - transform.position);
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0F);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0F);
                        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
                    }
                }


            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}