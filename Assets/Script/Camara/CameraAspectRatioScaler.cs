using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatioScaler : MonoBehaviour
{
    public GameObject cuadroIzq, cuadroDer;



    void Start()
    {
        EstableceBarras();
    }



    public void EstableceBarras()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;
        
        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            cuadroIzq.SetActive(false);
            cuadroDer.SetActive(false);
        }
        else if (scaleheight < 1.01f && scaleheight > 1.0f)
        {
            cuadroIzq.SetActive(false);
            cuadroDer.SetActive(false);
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            scalewidth = 1.0f - scalewidth;
            cuadroIzq.transform.localScale = new Vector3(scalewidth, 1, 1);
            cuadroDer.transform.localScale = new Vector3(scalewidth, 1, 1);
        }
    }
}
