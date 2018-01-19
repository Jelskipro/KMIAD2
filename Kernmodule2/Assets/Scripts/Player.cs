using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject BodySourceView;
    public GameObject manDestination;
    public GameObject headObject;
    public GameObject handLObject;
    public GameObject handRObject;
    public Vector3 playerPos;

    private ManAI manAI;
    private BodySourceView bodySourceView;
    private GameObject body;
    private GameObject spineBase;
    private GameObject head;
    private GameObject handLeft;
    private GameObject handRight;
    private Vector3 manDestinationPos;

    // Use this for initialization
    void Start()
    {
        bodySourceView = BodySourceView.GetComponent<BodySourceView>();
        manDestinationPos.Set(0, -1.54f, -1.57f);
        manAI = GameObject.FindGameObjectWithTag("Man").GetComponent<ManAI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Zoek naar het kinect lichaam zodat we de lichaamsdelen kunnen gebruiken
        body = GameObject.FindGameObjectWithTag("Body");

        //Restart de scene
        if (Input.GetKeyDown("u"))
        {
            SceneManager.LoadScene("MAin2");

        }

        //Kijk of hij een lichaam heeft gevonden
        if (bodySourceView.tracked == true)
        {
            //Pak de lichaamsdelen
            spineBase = body.transform.Find("SpineBase").gameObject;
            head = body.transform.Find("Head").gameObject;
            handLeft = body.transform.Find("HandLeft").gameObject;
            handRight = body.transform.Find("HandRight").gameObject;

            //Zet de objecten op de positie van de lichaams delen.
            headObject.transform.position = head.transform.position;
            handLObject.transform.position = handLeft.transform.position;
            handRObject.transform.position = handRight.transform.position;

            if (spineBase != null)
            {
                //De speler zn positie is de positie van de spine
                playerPos = spineBase.transform.position;

                //Kijk of de speler opzij loopt, maar wel met een marge zodat de waypoint voor de schim niet schokkend wordt
                if (Mathf.Abs(playerPos.x - manDestinationPos.x) > 2f)
                {
                    
                    if (manAI.randomDesination == false)
                    {
                        //Zet de vector3 voor de waypoint gameobject.
                        manDestinationPos.Set(playerPos.x, -1.54f, -1.57f);
                        manDestination.transform.position = manDestinationPos;

                    }
                }


            }
            else Debug.Log("No child with the name 'HandLeft' attached to the player");

        }



    }
}
