using System.Collections;
using TMPro;
using UnityEngine;

public class ColisionBolos : MonoBehaviour
{
    private GameObject[] bolos;
    public GameObject bolosPrefab;
    private int caidos = 0;
    private int bolosAcumulados = 0;
    private float treshold = 0.7f;

    private TextMeshProUGUI textoPuntos;
    private int[][] puntuacion = new int[11][]; //puntuacion [juego, tirada]
    private int[] marcador = new int[11]; //puntuacion total
    private int juego = 1;
    private int tirada = 1;
    private bool pleno = false;

    public Transform aparicionBola;
    public GameObject bolaPrefab;
    public GameObject proteccionBolos;





    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        GameObject go = GameObject.Find("PuntosText");
        textoPuntos = go.GetComponent<TextMeshProUGUI>();
        textoPuntos.text = "POINTS: " + "\n";

        puntuacion = new int[11][];
        marcador = new int[11];

        proteccionBolos.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (marcador[10] !=0)
        {
            NewGame();
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(1);
        proteccionBolos.SetActive(true);
        yield return new WaitForSeconds(5);
        if (other != null && other.tag == "Ball")
        {

            var peso = other.GetComponent<BallSoundController>().peso;
            var pesoEnKgs = other.GetComponent<BallSoundController>().pesoEnKgs;

            //Se cuentan los bolos
            cuentaCaidos();

            //Se destruye la bola
            Destroy(other.gameObject);

            //Se actualiza la puntuación
            Puntuacion();

            GeneraBola(bolaPrefab, pesoEnKgs, peso);
        }
        proteccionBolos.SetActive(false);
    }

    private void GeneraBola(GameObject bola, int pesoKgs, float peso)
    {
        var bolaControll = bola.GetComponent<BallSoundController>();
        bolaControll.peso = peso;
        bolaControll.pesoEnKgs = pesoKgs;
        bolaControll.changePeso();

        var miBola = Instantiate(bola, aparicionBola.position, aparicionBola.rotation);
        miBola.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * 1.5f, ForceMode.Impulse);
    }

    private void cuentaCaidos()
    {
        bolos = GameObject.FindGameObjectsWithTag("Bolo");

        foreach (GameObject bolo in bolos)
        {
            if ((bolo.transform.up.y < treshold) || (bolo.transform.position.y < -.5))
            {
                caidos++;
                Destroy(bolo);
            }
        }
    }

    private void Puntuacion()
    {

        if (tirada == 1)
        {

            puntuacion[juego] = new int[] { 0, caidos, 0 };
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
            textoPuntos.text += "|";
            //Reseteamos el juego
            juego++;
            tirada = 1;

            //Eliminamos los bolos
            GameObject bolosCaidos = GameObject.Find("BolosPrefab");
            Destroy(bolosCaidos);
            bolosCaidos = GameObject.Find("BolosPrefab(Clone)");
            Destroy(bolosCaidos);

            //Sacamos los nuevos bolos
            GameObject bolosInstanciados = Instantiate(bolosPrefab, new Vector3(-23.0189991f, 0.270000011f, 5.27600002f), Quaternion.identity);

        }
        else
        {
            tirada++;

            if (pleno)
            {
                pleno = false;

                //Eliminamos los bolos
                GameObject bolosCaidos = GameObject.Find("BolosPrefab");
                Destroy(bolosCaidos);
                bolosCaidos = GameObject.Find("BolosPrefab(Clone)");
                Destroy(bolosCaidos);

                //Sacamos los nuevos bolos
                GameObject bolosInstanciados = Instantiate(bolosPrefab, new Vector3(-23.0189991f, 0.270000011f, 5.27600002f), Quaternion.identity);

            }
        }

    }

    private void CalculaPuntosCuadros()
    {
        if (puntuacion[juego][1] == 10) //si pleno
        {
            pleno = true;

            marcador[juego] = 20 + puntuacion[juego][1];
        }
        else
        {
            if (puntuacion[juego][1] + puntuacion[juego][2] == 10) //Si es semipleno
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

        textoPuntos.text += " " + marcador[juego] + "|";
    }

}
