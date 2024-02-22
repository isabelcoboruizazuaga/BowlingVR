using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionBolos : MonoBehaviour
{
    private GameObject[] bolos;
    private int caidos;
    public float treshold = 0.4f;


    // Start is called before the first frame update
    void Start()
    {
        //ROTACION <-0.019/0.014 -0.002
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(4);
        if (other.tag == "Ball")
        {
            Debug.Log("plano de bolos");
            cuentaCaidos();
        }
    }

    private void cuentaCaidos()
    {
        bolos = GameObject.FindGameObjectsWithTag("Bolo");

        foreach (GameObject bolo in bolos) {
            if (bolo.transform.up.y < treshold)
            {
                caidos++;
                Destroy(bolo);
            }
        }
        Debug.Log(caidos);
    }
}
