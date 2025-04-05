
using System.Runtime;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AnimTrigger : UdonSharpBehaviour
{
    public bool isGlobal = true;

    public Animator[] animator;

    public string triggerAn;
    public string triggerAus;


     VRCPlayerApi spieler;
    
    

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
    }
    public void OnInteract()
    {
        playAnim();
       
    }

    public void playAnim()
    {
        foreach (var anim in animator)
        {
           anim.SetBool(triggerAn, isAn);
           anim.SetBool(triggerAus, !isAn);
           
        }
    }
    

 
}


