
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
            // Wenn das Objekt noch nicht die Zielposition erreicht hat, bewege es weiter
            if (timeElapsed < Duration)
            {
                timeElapsed += Time.deltaTime; // Zunahme der Zeit
                float moveFraction = timeElapsed / Duration; // Berechnet den Anteil der Bewegung

                // Bewege das Objekt basierend auf dem Prozentsatz der Gesamtbewegungszeit
                gameObject.transform.position = Vector3.Lerp(DefaultPosition, MovedPosition, moveFraction);

                // Sendet das nächste MoveStep nach einem Frame
                SendCustomEventDelayedFrames(nameof(MoveStep), 1);
            }
            else
            {
                // Am Ziel angekommen, Position exakt setzen und Stoppen
                gameObject.transform.position = MovedPosition;
            }
    }
    

    public void Reset()
    {
        isActive = false;
        if (gameObject.transform.position == MovedPosition)
        {
            if (timeElapsed < Duration)
            {
                timeElapsed += Time.deltaTime; // Zunahme der Zeit
                float moveFraction = timeElapsed / Duration; // Berechnet den Anteil der Bewegung

                // Bewege das Objekt basierend auf dem Prozentsatz der Gesamtbewegungszeit
                gameObject.transform.position = Vector3.Lerp(MovedPosition,DefaultPosition, moveFraction);

                // Sendet das nächste MoveStep nach einem Frame
                SendCustomEventDelayedFrames(nameof(Reset), 1);
            }
            else
            {
                // Am Ziel angekommen, Position exakt setzen und Stoppen
                gameObject.transform.position = DefaultPosition;     
            }
        }
    }

    public bool GetActive()
    {
        return isActive;
    }
}
