
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CoOpStandOnButton : UdonSharpBehaviour
{
    VRCPlayerApi player;
    [SerializeField]GameObject OtherButton;
    [SerializeField]GameObject[] ToggleObjectsOn;
    [SerializeField]GameObject[] ToggleObjectsOff;
    [SerializeField]AudioSource audioOnSucess;
    [SerializeField]AudioSource audioOnStand;
    [SerializeField] private float Yposition = 2f;
    [UdonSynced]private bool isPressed;
    [UdonSynced]private bool isDone;
    
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
        player = Networking.LocalPlayer;
        isPressed = false;
    }

    public override void OnDeserialization()
    {
        RequestSerialization();
        if (isDone)
        {
            foreach (GameObject ToggleObject in ToggleObjectsOn)
            {
                ToggleObject.SetActive(true);
            }
            foreach (GameObject ToggleObject in ToggleObjectsOff)
            {
                ToggleObject.SetActive(false);
            }
        }
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (isDone) return;
        if (isPressed) return;
        timeElapsed = 0f;

        if (!Networking.IsOwner(gameObject))
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        if(audioOnStand !=null)
            audioOnStand.Play();
        MoveStep();
        isPressed = true;
        RequestSerialization();
        if (!OtherButton.GetComponent<CoOpStandOnButton>().CheckPressed()) return;
        isDone = true;
        OtherButton.GetComponent<CoOpStandOnButton>().isDoneTrue();
        objectToggle();
        if(audioOnSucess != null)
            audioOnSucess.Play();
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if(isDone) return;
        if (!isPressed) return;
        timeElapsed = 0f;

        if (!Networking.IsOwner(gameObject))
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        Reset();
        isPressed = false;
        RequestSerialization();
    }

    public void objectToggle()
    {
        foreach (GameObject ToggleObject in ToggleObjectsOn)
        {
            ToggleObject.SetActive(true);
        }
        foreach (GameObject ToggleObject in ToggleObjectsOff)
        {
            ToggleObject.SetActive(false);
        }
    }

    public void MoveStep()
    {
        var step =  MoveDuration * Time.deltaTime;
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
        var step =  MoveDuration * Time.deltaTime;
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
    
    public bool CheckPressed()
    {
        return isPressed;
    }

    public void isDoneTrue()
    {
        isDone = true;
        RequestSerialization();
    }
}
