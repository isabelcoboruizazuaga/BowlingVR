using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallSoundController : MonoBehaviour
{
    public float peso=0.7f;
    public int pesoEnKgs=7;
    private bool hasPlayed = false;

    public TextMeshProUGUI textoPeso;

    private void Start()
    {
       changePeso();
    }

    public void changePeso()
    {
        this.GetComponent<Rigidbody>().mass = peso;
        textoPeso.text = pesoEnKgs.ToString() + "KG";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bolo") && hasPlayed==false)
        {
            hasPlayed = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
