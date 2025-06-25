using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AnimTrigger : UdonSharpBehaviour
{
    [Header("Sync Verhalten")]
    public bool isGlobal = true;

    [Header("Animator Setup")]
    public Animator[] animator;
    public string triggerAn;
    public string triggerAus;

    [UdonSynced] 
    private bool isAn = false;

    public override void OnDeserialization()
    {
        if (isGlobal)
        {
            playAnim(); 
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
            RequestSerialization(); 
        }

        playAnim(); 
    }

    private void playAnim()
    {
        foreach (var anim in animator)
        {
            anim.SetBool(triggerAn, isAn);
            anim.SetBool(triggerAus, !isAn);
        }
    }
}