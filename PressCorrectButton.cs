using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PressCorrectButton : UdonSharpBehaviour
{
    private Vector3 buttonMoveOffset;
    private Vector3 roofMoveOffset;

   private MoveManager moveManager;
    private AudioSource correctButtonSound;
    private AudioSource incorrectButtonSound;
    private AudioSource roofMoveSound;

    [SerializeField] private bool isCorrectButton = false;

    private Vector3 defaultButtonPosition;
    private Vector3 movedButtonPosition;
    private Vector3 defaultRoofPosition;
    private GameObject Parent;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        moveManager = Parent.GetComponent<MoveManager>();
        buttonMoveOffset = moveManager.GetButtonMoveOffset();
        roofMoveOffset = moveManager.GetRoofMoveOffset();
        
        defaultButtonPosition = transform.position;
        movedButtonPosition = defaultButtonPosition + buttonMoveOffset;
    }

    public override void Interact()
    {
        if (moveManager.GetCorrect() == false)
        {
        if (!Networking.IsOwner(gameObject))
            Networking.SetOwner(Networking.LocalPlayer, gameObject);

        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnInteract");
        }
    }

    public void OnInteract()
    {
        if (isCorrectButton)
        {
            moveManager.GetComponent<MoveManager>().IsCorrect();
        }
        else
        {
            moveManager.EnqueueMove(gameObject, defaultButtonPosition);
            moveManager.GetComponent<MoveManager>().IsIncorrect();
            moveManager.EnqueueMove(gameObject, movedButtonPosition);
            moveManager.EnqueueMove(moveManager.GetRoof(), moveManager.GetmovedRoofPosition());
        }
    }
}
