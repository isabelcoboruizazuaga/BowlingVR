using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirar : MonoBehaviour
{
 public Canvas canvas;
    public Camera camaraAMirar;
    // Start is called before the first frame update
    void Start()
    {
        camaraAMirar = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        canvas.transform.rotation=Quaternion.LookRotation(camaraAMirar.transform.forward);
    }

    public void Mira()
    {
        canvas.GetComponent<Canvas>().enabled = true;
    }

    public void Para()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }
}
