using JetBrains.Annotations;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorFeatures : CoreFeatures
{
    [Header("Door Configuration")]
    [SerializeField]
    private Transform doorPivot;
    [SerializeField] 
    private float maxAngle = 90.0f;
    [SerializeField]
    private bool reverseAngleDirection = false; //Flips direction
    [SerializeField]
    private float doorSpeed = 2.0f;
    [SerializeField]
    private bool open = false;
    [SerializeField]
    private bool MakeKinematicOnOpen = false;


    [Header("Interactions Configuration")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        //When key gets close to the socket, add a listener
        //s = shorthand, selectEnterEvents
        socketInteractor?.selectEntered.AddListener((s) => //ABSTRACTION - hiding complexity
        {
            OpenDoor();
        });
        socketInteractor?.selectExited.AddListener((s) => 
        {
            PlayOnEnd();
            socketInteractor.socketActive = featureUsage == FeatureUsage.Once ? false : true; //Reusability
        });

        //Doors with Simple Interactors may not require a "key". also good for cabinets, drawers, etc...
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });
    }
    public void OpenDoor()
    {
        //If the door is not open, Play the OnStart Sound
        if (!open)
        {
            PlayOnStart();
            open = true;
            StartCoroutine(ProcessMotion());
        }
    }
    private IEnumerator ProcessMotion()
    {
        //Keep looking for whether door is open or not
        while (open)
        {
            var angle = doorPivot.localEulerAngles.y < 180 ? doorPivot.localEulerAngles.y : doorPivot.localEulerAngles.y - 360;
            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;
            if (angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1 : 1)); 
            }
            else
            {
                //when done with opening, turn off the rigidbody
                open = false;
                var featureRigidBody = GetComponent<Rigidbody>();
                if (featureRigidBody != null && MakeKinematicOnOpen) featureRigidBody.isKinematic = true;
                {

                }
            }
                yield return null;
        }

    }
}
