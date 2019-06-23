using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private float rotTime = 2.0f;
    private Quaternion doorRotQ;

    private void Start()
    {
        doorRotQ = transform.rotation;
    }

    public void OpenDoorTween(float EnterDuration)
    {
        rotTime = EnterDuration;
        transform.rotation = doorRotQ;
        LeanTween.rotate(gameObject, Vector3.up * 85f, rotTime).setEase(LeanTweenType.easeOutBack);
    }
}