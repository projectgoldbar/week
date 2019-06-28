using UnityEngine;

public class Gate : MonoBehaviour
{
    private bool playerin = false;
    public bool isClear = false;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerData>().arrow.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!playerin)
        {
            playerin = true;
            other.GetComponent<PlayerData>().gate = this;
        }
        else if (isClear)
        {
            //other.GetComponent<PlayerData>().clearBox[other.GetComponent<PlayerData>().boxBuffer]++;
        }
    }
}