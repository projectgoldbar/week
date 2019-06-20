using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnim;

    private void Start()
    {
        doorAnim = this.GetComponent<Animator>();
        OpenDoor();
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

    private void OpenDoor()
    {
        doorAnim.SetTrigger("start");
    }
}