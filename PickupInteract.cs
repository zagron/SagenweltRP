
using UdonSharp;
using UnityEngine;
using VRC.SDK3.ClientSim;
using VRC.SDKBase;
using VRC.Udon;

public class PickupInteract : UdonSharpBehaviour
{
    public bool isGlobal = true;

    public Animator[] animator;


    public AudioSource sound;


    
    

    [UdonSynced] 
    bool isAn = false;



    void Start()
    {

    }
    public override void OnDeserialization()
    {
        if (isGlobal)
        {
        RequestSerialization();
        OnInteract(); // Late Joiner sehen den aktuellen Wert
        }
    }
    
   
    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

         isAn = !isAn;

        if (isGlobal)
        {
        RequestSerialization(); // Synchronisation auslösen
        }

        OnInteract();
        OnPickupUseUp();
    }
    public override void OnPickupUseUp()
    {
        playSound();
    }
    public void OnInteract()
    {
        
       
    }

    public void playSound()
    {
        sound.Play();
    }
    

 
}

