using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionBolos : MonoBehaviour
{
    private GameObject[] bolos;
    private int caidos;
    private float treshold=90f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(3);
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
            if (bolo.transform.rotation.x > -91f  || bolo.transform.rotation.x < -89f )
            {
                caidos++;
                Destroy(bolo);
            }
        }
        Debug.Log(caidos);
    }
}
