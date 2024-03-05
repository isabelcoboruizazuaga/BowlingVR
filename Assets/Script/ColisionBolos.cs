using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColisionBolos : MonoBehaviour
{
    private GameObject[] bolos;
    private int caidos=0;
    private float treshold = 0.7f;
    private TextMeshProUGUI textoPuntos;



    // Start is called before the first frame update
    void Start()
    {
        //ROTACION <-0.019/0.014 -0.002
        GameObject go = GameObject.Find("PuntosText");
        textoPuntos = go.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(5);
        if (other.tag == "Ball")
        {
            //Se cuentan los bolos
            cuentaCaidos();

            //Se destruye la bola
            Destroy(other.gameObject);

            //Se actualiza la puntuación
            textoPuntos.text = "Bolos caidos: " + caidos;
        }
    }

    private void cuentaCaidos()
    {
        bolos = GameObject.FindGameObjectsWithTag("Bolo");

        foreach (GameObject bolo in bolos) {
            if ((bolo.transform.up.y < treshold)||(bolo.transform.position.y<-1.5))
            {
                caidos++;
                Destroy(bolo);
            }
        }
    }
}
