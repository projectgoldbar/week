using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnim;
    private Quaternion doorRotQ;

    private void Start()
    {
        doorAnim = this.GetComponent<Animator>();
        doorRotQ = this.transform.rotation;
        //OpenDoor();
    }

    public void OpenDoor()
    {
        resetDoorRot();
        doorAnim.SetBool("DoorOpen", true);
    }

    private void resetDoorRot()
    {
        doorAnim.SetBool("DoorOpen", false);
        this.transform.rotation = doorRotQ;
        Debug.Log(this.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckPlayerIn(other))
        {
            CloseDoor();
        }
    }

    private bool CheckPlayerIn(Collider other)
    {
        if (other.tag == "Player")
        {
            return true;
        }
        return false;
    }

    private void CloseDoor()
    {
        doorAnim.SetTrigger("PlayerIn");
    }
}