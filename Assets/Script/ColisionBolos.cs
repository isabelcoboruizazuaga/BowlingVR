using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class ColisionBolos : MonoBehaviour
{
    private GameObject[] bolos;
    public GameObject bolosPrefab;
    private int caidos=0;
    private int bolosAcumulados = 0;
    private float treshold = 0.7f;

    private TextMeshProUGUI textoPuntos;
    private int[][] puntuacion=new int[11][]; //puntuacion [juego, tirada]
    private int[] marcador=new int[11]; //puntuacion total
    private int juego = 1;
    private int tirada = 1;
    private bool pleno = false;

    public Transform aparicionBola;
    public GameObject bolaPrefab;





    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("PuntosText");
        textoPuntos = go.GetComponent<TextMeshProUGUI>();

        puntuacion = new int[11][]; 
        marcador = new int[11];
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
            var peso= other.GetComponent<BallSoundController>().peso;
            var pesoEnKgs = other.GetComponent<BallSoundController>().pesoEnKgs;

            //Se cuentan los bolos
            cuentaCaidos();

            //Se destruye la bola
            Destroy(other.gameObject);

            //Se actualiza la puntuación
            Puntuacion();

            GeneraBola(bolaPrefab, pesoEnKgs,peso);
        }
    }

    private void GeneraBola(GameObject bola,int pesoKgs, float peso)
    {
        var bolaControll = bola.GetComponent<BallSoundController>();
        bolaControll.peso = peso;
        bolaControll.pesoEnKgs = pesoKgs;
        bolaControll.changePeso();

        var miBola= Instantiate(bola, aparicionBola.position,aparicionBola.rotation);
        miBola.GetComponent<Rigidbody>().AddForce(new Vector3(1,0,0) *1.5f, ForceMode.Impulse);
    }

    private void cuentaCaidos()
    {
        bolos = GameObject.FindGameObjectsWithTag("Bolo");

        foreach (GameObject bolo in bolos) {
            if ((bolo.transform.up.y < treshold)||(bolo.transform.position.y<-.5))
            {
                caidos++;
                Destroy(bolo);
            }
        }
    }

    private void Puntuacion()
    {
        /*10 juegos de 2 tiradas cada uno
        si pleno-> 1 tirada si no-> 2
        pleno(10 bolos primer lanzamiento): 10 puntos más siguientes dos lanzamientos
        semipleno(derriba todos los restantes en el 2o): 10 puntos más puntos lanzamiento 
        ultima tirada-> pleno-> 2 tiradas extras, semipleno 1 tirada extra (3 tiradas en ambos casos);
         */

        /*
         10 juegos 
        si jugada 1 y caidos 10 ( pleno){
            puntuacion jugada= 10 (+2 jugadas siguientes)
        }
        si no{
             si caidos +guardados== 10(semipleno){
                puntuacion jugada= 10 (+ jugada siguiente)
                jugada=1;
                juego++;
            }
            si no{
                puntuacion jugada+=caidos
            
                if(jugada ==1) jugada++
                else {
                    jugada =1
                    juego++;
                }
            
        
        }
        }
         */

        /* if(tirada==1 && caidos == 10) //Es un pleno
         {
             puntuacion[juego][tirada] = caidos; //se tiene que sumar luego las dos siguientes

             //Se pasa al siguiente juego
            //pleno = true;
             juego++;
         }
         else
         {
             if (caidos+bolosAcumulados==10) //semipleno
             {
                 puntuacion[juego][tirada] = caidos; //se suma luego la siguiente jugada

                 //se pasa al siguiente juego
                 //semipleno = true;
                 juego++;
             }
             else
             {
                 puntuacion[juego][tirada]= caidos;
             }
         }*/

        if (tirada == 1)
        {

            puntuacion[juego] = new int[] {0, caidos, 0 };
        }
        else
        {
            puntuacion[juego] = new int[] { 0, puntuacion[juego][1], caidos };
        }

        //Reseteamos los caidos
        caidos = 0;

        //Se actualiza el marcador
        CalculaPuntosCuadros();


        if (tirada > 1)
        {
            //Reseteamos el juego
            juego++;
            tirada = 1;

            //Eliminamos los bolos
            GameObject bolosCaidos = GameObject.Find("BolosPrefab");
            Destroy(bolosCaidos);
            bolosCaidos = GameObject.Find("BolosPrefab(Clone)");
            Destroy(bolosCaidos);

            //Sacamos los nuevos bolos
            GameObject bolosInstanciados = Instantiate(bolosPrefab,  new Vector3(-23.0189991f, 0.270000011f, 5.27600002f),Quaternion.identity);

           
        }
        else
        {
            tirada++;
        }

    }

    private void CalculaPuntosCuadros()
    {
        if (puntuacion[juego][1]==10) //si pleno
        {
            pleno = true;
            
            marcador[juego] = 20 + puntuacion[juego][1];
        }
        else
        {
            if(puntuacion[juego][1] + puntuacion[juego][2] == 10) //Si es semipleno
            {
                //Se suma la siguiente tirada
                marcador[juego] = 10 + puntuacion[juego][1];
            }
            else
            {
                //Se calcula normal simplemente sumando las dos puntuaciones
                marcador[juego] = puntuacion[juego][1] + puntuacion[juego][2];
            }
        }

        textoPuntos.text += " " + marcador[juego] +"|";
    }

}
