using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MoveManager : UdonSharpBehaviour
{
    private const int MaxQueueSize = 20;

    private GameObject[] moveQueueTargets = new GameObject[MaxQueueSize];
    private Vector3[] moveQueuePositions = new Vector3[MaxQueueSize];
    private int queueStart = 0;
    private int queueEnd = 0;
    private Vector3 defaultRoofPosition;
    private Vector3 movedRoofPosition;
    private bool isCorrect = false;
    
    [Header("Button & Roof Movement")]
    [SerializeField] private GameObject[] solutionObjects;
    [SerializeField] private GameObject movableRoof;
    
    
    [Header("Position Offsets")]
    [SerializeField] private Vector3 buttonMoveOffset;
    [SerializeField] private Vector3 roofMoveOffset;
    
    [Header("Audio")]
    [SerializeField] private AudioSource correctButtonSound;
    [SerializeField] private AudioSource incorrectButtonSound;
    [SerializeField] private AudioSource roofMoveSound;

    private GameObject currentTarget;
    private Vector3 currentPosition;
    private bool isMoving = false;

    [SerializeField] private float moveDuration = 2f;

    public void Start()
    {
        defaultRoofPosition = movableRoof.transform.position;
        movedRoofPosition = defaultRoofPosition + roofMoveOffset;
    }

    public void EnqueueMove(GameObject target, Vector3 destination)
    {
        if ((queueEnd + 1) % MaxQueueSize == queueStart)
        {
            Debug.LogWarning("MoveManager: Queue is full!");
            return;
        }

        moveQueueTargets[queueEnd] = target;
        moveQueuePositions[queueEnd] = destination;
        queueEnd = (queueEnd + 1) % MaxQueueSize;

        if (!isMoving)
        {
            StartNextMove();
        }
    }

    public void StartNextMove()
    {
        if (queueStart == queueEnd)
        {
            isMoving = false;
            return;
        }

        isMoving = true;
        currentTarget = moveQueueTargets[queueStart];
        currentPosition = moveQueuePositions[queueStart];
        queueStart = (queueStart + 1) % MaxQueueSize;

        MoveStep();
    }

    public void MoveStep()
    {
        float step = moveDuration * Time.deltaTime;

        if (Vector3.Distance(currentTarget.transform.position, currentPosition) > 0.01f)
        {
            currentTarget.transform.position = Vector3.MoveTowards(currentTarget.transform.position, currentPosition, step);
            SendCustomEventDelayedFrames(nameof(MoveStep), 1);
        }
        else
        {
            currentTarget.transform.position = currentPosition;
            SendCustomEventDelayedFrames(nameof(StartNextMove), 1);
        }
    }

    public void IsCorrect()
    {
        isCorrect = true;
        foreach (GameObject obj in solutionObjects)
            obj.SetActive(!obj.activeSelf);

        if (correctButtonSound != null)
            correctButtonSound.Play();
    }

    public void IsIncorrect()
    {
        if (incorrectButtonSound != null)
            incorrectButtonSound.Play();
        if (roofMoveSound != null)
            roofMoveSound.Play();

        movedRoofPosition = movableRoof.transform.position + roofMoveOffset;
    }

    public Vector3 GetButtonMoveOffset()
    {
        return buttonMoveOffset;
    }
    
    public Vector3 GetRoofMoveOffset()
    {
        return roofMoveOffset;
    }

    public Vector3 GetmovedRoofPosition()
    {
        return movedRoofPosition;
    }

    public AudioSource GetCorrectButtonSound()
    {
        return correctButtonSound;
    }

    public AudioSource GetIncorrectButtonSound()
    {
        return incorrectButtonSound;
    }

    public AudioSource GetRoofMoveSound()
    {
        return roofMoveSound;
    }

    public GameObject GetRoof()
    {
        return movableRoof;
    }

    public bool GetCorrect()
    {
        return isCorrect;
    }
    
    
}