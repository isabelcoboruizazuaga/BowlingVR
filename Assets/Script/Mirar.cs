using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirar : MonoBehaviour
{
 public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
