using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

    public bool isTouchingL;
    public bool isTouchingR;

    //Kijk of de speler het raam aan raakt door te kijken of een van de handen in de colider komen.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "HandLeft")
        {
            isTouchingL = true;

        }
        if (other.tag == "HandRight")
        {
            isTouchingR = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.tag == "HandLeft")
        {
            isTouchingL = false;

        }
        if (other.tag == "HandRight")
        {
            isTouchingR = false;

        }

    }
}
