using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool isInExit;

    //kijk of de schim in de stop zone is zodat hij niet de scene uit loopt.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Man")
        {
            //Zet de boolean op true
            isInExit = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Man")
        {
            isInExit = false;

        }

    }
}
