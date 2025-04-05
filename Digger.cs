using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


public class Digger : UdonSharpBehaviour
{
    public GameObject kiste;
    public GameObject DigSpot;

    public AudioSource DigSound;
    public AudioSource kisteFUMP;

    public ParticleSystem dreck;

    public int dauer = 5;

    public bool aufhebbar = true;

    float DigLenght { get{return Mathf.Abs((kiste.transform.localPosition.y-DigSpot.transform.localPosition.y)/dauer);} }

    Vector3 kistePos;

    bool istDrin;


    void Start()
    {
        istDrin = true;
        kistePos = new Vector3(DigSpot.transform.localPosition.x, kiste.transform.localPosition.y, DigSpot.transform.localPosition.z);

        kiste.transform.localPosition = kistePos;
        kiste.GetComponent<VRC_Pickup>().pickupable = false;
        Debug.Log("Grabtiefe ist:"+DigLenght + "DigSpot:"+DigSpot.transform.position.y+"kiste:"+kiste.transform.position.y+"name von diesem objekt:"+ this.gameObject.name);
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Grabtiefe ist:"+DigLenght);
        if (DigSpot.transform.position.y >= kiste.transform.position.y && istDrin == true && other.gameObject.Equals(DigSpot))
        {
            Debug.Log("er ist drin!");
            kiste.transform.localPosition = new Vector3(kiste.transform.localPosition.x, kiste.transform.localPosition.y + DigLenght, kiste.transform.localPosition.z);
            istDrin = false;
            if (DigSound != null)
            {
                DigSound.Play();
            }
            if (dreck != null)
            {
                dreck.Play();
            } 
            
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.Equals(DigSpot))
        {
        istDrin = true;
            if(aufhebbar && DigLenght < 0.02f)
            {
                if (kisteFUMP != null)
                {
                    kisteFUMP.Play();
                }
                kiste.GetComponent<VRC_Pickup>().pickupable = true;
            }
        }
    }
        
    

}
