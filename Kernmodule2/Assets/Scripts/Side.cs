using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour {
    public bool isNearWindow;

    //Kijk of de schim bij het raam is
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Man")
        {
            isNearWindow = true;

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Man")
        {
            isNearWindow = false;

        }

    }
}
