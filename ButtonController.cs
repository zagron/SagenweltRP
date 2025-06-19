
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class ButtonController : UdonSharpBehaviour
{
    [SerializeField]public float Duration = 2f;
    [SerializeField] public float Zposition = 0.1f;
    [SerializeField] public float Xposition = 0f;
    [SerializeField] public float Yposition = 0f;
    [SerializeField] public GameObject[] Buttons;
    [SerializeField] public GameObject[] RiddleSolvedObjects;
    [SerializeField] public AudioSource RiddleSolvedSound;
    [UdonSynced]private bool isAn;
    [UdonSynced]private int CurrentButton;
    private float timeElapsed = 0f;
    void Start()
    {
    }

    public override void OnDeserialization()
    {
        if(isAn)
            foreach (GameObject RiddleSolvedObject in RiddleSolvedObjects)
            {
                RiddleSolvedObject.SetActive(!RiddleSolvedObject.activeSelf);
            }
    }

    public void IsButtonCorrect()
    {
        if (Buttons[CurrentButton].GetComponent<ButtonMove>().GetActive() == true)
        {
            Buttons[CurrentButton].GetComponent<ButtonMove>().SetTimeElapsed();
            Buttons[CurrentButton].GetComponent<ButtonMove>().MoveStep();
            
            CurrentButton++;
        }
        else if(Buttons[CurrentButton].GetComponent<ButtonMove>().GetActive() == false)
        {
            CurrentButton = 0;
            
            Reset();
        }
        if (CurrentButton == Buttons.Length)
        {
            isAn = true;     
            foreach (GameObject RiddleSolvedObject in RiddleSolvedObjects)
            {
                RiddleSolvedObject.SetActive(!RiddleSolvedObject.activeSelf);
            }
            RiddleSolvedSound.Play();
        }
        if (!Networking.IsOwner(gameObject))
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        RequestSerialization();

    }

    public void Reset()
    {
        foreach (GameObject button in Buttons)
        {
            button.GetComponent<ButtonMove>().Reset();
        }
    }

    public float GetDuration()
    {
        return Duration;
    }
    public float GetZposition()
    {
        return Zposition;
    }
    public float GetXposition()
    {
        return Xposition;
    }
    public float GetYposition()
    {
        return Yposition;
    }
    
}
