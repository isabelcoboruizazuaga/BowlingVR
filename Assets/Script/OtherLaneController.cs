using System.Collections;
using TMPro;
using UnityEngine;

public class OtherLaneController : MonoBehaviour
{
    public Transform aparicionBola;
    public GameObject bolaPrefab;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(5);
        if (other.tag == "Ball")
        {
            var peso = other.GetComponent<BallSoundController>().peso;
            var pesoEnKgs = other.GetComponent<BallSoundController>().pesoEnKgs;


            //Se destruye la bola
            Destroy(other.gameObject);


            GeneraBola(bolaPrefab, pesoEnKgs, peso);
        }
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

}
