using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAI : MonoBehaviour {

    public bool reached;
    public bool sideMove;
    public bool pickUp;
    public bool tapping;
    public bool toWindow;
    public bool randomDesination;

    public GameObject manDestination;
    public GameObject nearWindowArea;
    public GameObject ExitArea;

    private ManIK manIK;
    private Side side;
    private Exit exit;
    private Animator animator;
    private Vector3 currentPos;
    private Vector3 manDestinationPos;

    void Start ()
    {
        //Vraag componenten op om te gebruiken
        animator = GetComponent<Animator>();
        manIK = GetComponent<ManIK>();
        side = nearWindowArea.GetComponent<Side>();
        exit = ExitArea.GetComponent<Exit>();
        
        //Zet voor de eerste keer dat de schim de scene inloop aan dat hij gaat tikken op het raam
        tapping = true;
    }

    void Update ()
    {
        
        //Zet uit/aan of de schim naar links en naar rechts kan lopen
        if (Input.GetKeyDown("s"))
        {
            if(sideMove == false)
            {
                sideMove = true;
            }
            else
            {
                sideMove = false;

            }
        }
        //Zet uit/aan of de schim naar het raam toe loopt of er juist weg gaat
        if (Input.GetKeyDown("b"))
        {
            if (toWindow == false)
            {
                toWindow = true;
            }
            else
            {
                toWindow = false;

            }
        }
        //Speel de pickup animatie af
        if (Input.GetKeyDown("p"))
        {
            if (pickUp == false)
            {
                pickUp = true;
            }
            else
            {
                pickUp = false;

            }
        }
        //Speel te op de ruit tick animatie af
        if (Input.GetKeyDown("t"))
        {
            if (tapping == false)
            {
                tapping = true;
            }
            else
            {
                tapping = false;

            }
        }
        //Geef de schim een random punt waar hij naar toe gaat lopen op de x-as
        if (Input.GetKeyDown("r"))
        {
            
            if (randomDesination == false)
            {
                randomDesination = true;
                manDestinationPos.Set(Random.Range(-10, -3), -1.54f, -1.57f);
                manDestination.transform.position = manDestinationPos;

            }
            else
            {
                randomDesination = false;

            }
        }
      
        if (toWindow == false)
        {

            WalkOut();
            //Zet de IK aan in het ManIK script zodat hij de speler aankijkt.
            manIK.ikActive = true;

        }
        if (toWindow == true)
        {
            
            WalkIn();
            //Zet de IK aan in het ManIK script zodat hij de speler aankijkt.
            manIK.ikActive = true;

        }
        if (sideMove == true)
        {
            SideMoving();
            //Zet de IK aan in het ManIK script zodat hij de speler aankijkt.
            manIK.ikActive = true;

        }

        if (pickUp == true)
        {
            StartCoroutine(PickUp());
            //Zet de IK uit in het ManIK script zodat hij de speler niet meer aankijkt en dus de animatie niet raar wordt
            manIK.ikActive = false;

        }

        if (tapping == true)
        {
            StartCoroutine(Tapping());
            //Zet de IK uit in het ManIK script zodat hij de speler niet meer aankijkt en dus de animatie niet raar wordt
            manIK.ikActive = false;
        }


        else if (reached == true || side.isNearWindow == false & sideMove == true)
        {
            //Set values in the animator controller to make the man in idle animation
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        //check if the man has reached the destination
        if (other.tag == "Destination")
        {
            reached = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Destination isn't reached if it is moved
        if (other.tag == "Destination")
        {
            reached = false;

        }

    }
    void SideMoving()
    {
        if (manDestination.transform.position.x > transform.position.x & reached == false)
        {
            //Set values in the animator controller to move to the right
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);

        }

        else if (manDestination.transform.position.x < transform.position.x & reached == false)
        {
            //Set values in the animator controller to move to the left
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }
        else if (reached == true || side.isNearWindow == false)
        {
            //Set values in the animator controller to make the man in idle animation
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
    }
    void WalkIn()
    {
        //check if the man is in the area in which he needs to walk to the left or right (near the window)
        if (side.isNearWindow == false)
        {
            animator.SetBool("Back", false);
            animator.SetBool("Forwards", true);
        }
        //check if the man is in the area in which he needs to walk to the left or right (near the window)
        if (side.isNearWindow == true)
        {
            animator.SetBool("Back", false);
            animator.SetBool("Forwards", false);

        }
    }
    void WalkOut()
    {
        //check if the man is in the area in which he needs to walk to the left or right (near the window)
        if (exit.isInExit == false)
        {
            animator.SetBool("Back", true);
            animator.SetBool("Forwards", false);
        }
        //check if the man is in the area in which he needs to walk to the left or right (near the window)
        if (exit.isInExit == true)
        {
            animator.SetBool("Back", false);
            animator.SetBool("Forwards", false);

        }
    }
    IEnumerator PickUp()
    {
        animator.SetBool("PickUp", true);
        //Wacht tot de animatie is afgespeeld
        yield return new WaitForSeconds(3);
        animator.SetBool("PickUp", false);
        //Zet de boolean uit zodat hij weer aangezet kan worden later
        pickUp = false;
    }
    IEnumerator Tapping()
    {
        animator.SetBool("Tap", true);
        //Wacht tot de animatie is afgespeeld
        yield return new WaitForSeconds(5);
        animator.SetBool("Tap", false);
        //Zet de boolean uit zodat hij weer aangezet kan worden later
        tapping = false;
        sideMove = true;
    }
    
}
