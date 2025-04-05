
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlaySound : UdonSharpBehaviour
{
    public AudioSource audiosource;
    
    void Start()
    {
        
    }
    
    public override void Interact()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnInteract");
    }

    public void OnInteract()
    {
            audioPlay();
    }

    public void audioPlay()
    {
    audiosource.Play();
    }
}
