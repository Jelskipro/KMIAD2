using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    public bool fade;
    public float time = 0.0f;
    public float speed = 1.0f;
    public Color color1;
    public Color color2;
    public Color lerpedColor = Color.white;
    public Renderer rend;

    void Start()
    {
        //Zoek de material Render van de plane die de schim verbergt
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown("v"))
        {
            fade = true;
        }
        if (fade == true)
        {
            //start het fadein effect
            StartCoroutine(FadeIn());
        }
        


    }
    IEnumerator FadeIn()
    {
        lerpedColor = Color.Lerp(color1, color2, Mathf.Lerp(0, 1, time));
        rend.material.SetColor("_TintColor", lerpedColor);
        time += Time.deltaTime * speed;
        //Wacht zodat de lerp afgemaakt kan worden
        yield return new WaitForSeconds(2);
        //Zet de tijd op zn eindwaarde vast zodat de color2 blijft. 
        time = 2;
    }
}
