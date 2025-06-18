
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PressCorrectButton : UdonSharpBehaviour
{
    [SerializeField] private bool IsCorrectButton = false;
    [SerializeField] private GameObject SolutionObject;
    [SerializeField] private GameObject MovableRoof;
    [SerializeField] private float Yposition = 2f;
    private float MoveDuration = 2f;
    private float timeElapsed = 0f;
    private Vector3 MovedPosition;
    private Vector3 DefaultPosition;
    private Vector3 UnterschiedPosition;
    void Start()
    {
        UnterschiedPosition = new Vector3(0, Yposition, 0);
        DefaultPosition = gameObject.transform.position;
        MovedPosition = DefaultPosition + UnterschiedPosition;
    }

    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnInteract");
    }
    
    public void OnInteract()
    {
        if(IsCorrectButton)
        {
        SolutionObject.SetActive(true);
        }
        else
        {
            timeElapsed = 0f;
            DefaultPosition = MovableRoof.transform.position;
            MovedPosition = DefaultPosition - UnterschiedPosition;
            MoveStep();
        }
    }
    
    public void MoveStep()
    {
        var step =  MoveDuration * Time.deltaTime;
        if (Vector3.Distance(MovableRoof.transform.position, MovedPosition) > 0.001f)
        {
            MovableRoof.transform.position = Vector3.MoveTowards( MovableRoof.transform.position,MovedPosition, step);
            SendCustomEventDelayedFrames(nameof(MoveStep), 1);
        }
        else
        {
            MovableRoof.transform.position = MovedPosition;     
        }
    }
            
}
