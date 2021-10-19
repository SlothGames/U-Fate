using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TituloJuego : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown || Input.GetButtonUp("A") || Input.GetButtonUp("B") || Input.GetButtonUp("X") || Input.GetButtonUp("Y") || Input.GetButtonUp("Start") || Input.GetButtonUp("Select") || Input.GetButtonUp("L1") || Input.GetButtonUp("R1"))
        {
            SceneManager.LoadScene("Portada");
        }
    }
}
