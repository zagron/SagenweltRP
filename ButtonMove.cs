
using UdonSharp;
using UnityEngine;
using UnityEngine.UIElements;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class ButtonMove : UdonSharpBehaviour
{
    private bool isActive;
    
    private float ZPosition;
    private float Xposition;
    private float Yposition;
    private Vector3 MovedPosition;
    private Vector3 DefaultPosition;
    private Vector3 UnterschiedPosition;
    
    private ButtonController Controller;
    private GameObject Parent;
    
    
    private float timeElapsed = 0f;
    private float Duration;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        Controller = Parent.GetComponent<ButtonController>();
        Duration = Controller.GetDuration();
        ZPosition = Controller.GetZposition();
        Yposition = Controller.GetYposition();
        Xposition = Controller.GetXposition();
        
        UnterschiedPosition = new Vector3(Xposition, Yposition, ZPosition);
        isActive = false;
        DefaultPosition = gameObject.transform.position;
        MovedPosition = DefaultPosition + UnterschiedPosition;
    }
    public override void Interact()
    {
        if (!Networking.IsOwner(gameObject))
            Networking.SetOwner(Networking.LocalPlayer, gameObject);

        SendCustomNetworkEvent(NetworkEventTarget.All, "OnInteract");
    }

    public void OnInteract()
    {
        if (isActive == false)
        {
            isActive = true;
            Controller.IsButtonCorrect();
        }
    }

    public void SetTimeElapsed()
    {
        timeElapsed = 0f;
    }
    public void MoveStep()
    {
        var step =  Duration * Time.deltaTime;
        if (Vector3.Distance(gameObject.transform.position, MovedPosition) > 0.001f)
        {
            gameObject.transform.position = Vector3.MoveTowards( gameObject.transform.position,MovedPosition, step);
            SendCustomEventDelayedFrames(nameof(MoveStep), 1);
        }
        else
        {
            gameObject.transform.position = MovedPosition;     
        }
    }
    
    public void Reset()
    {
        var step =  Duration * Time.deltaTime;
        if (Vector3.Distance(gameObject.transform.position, DefaultPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards( gameObject.transform.position,DefaultPosition, step);
            SendCustomEventDelayedFrames(nameof(Reset), 1);
        }
        else
        {
            gameObject.transform.position = DefaultPosition;    
        }
    }

    public bool GetActive()
    {
        return isActive;
    }
}
