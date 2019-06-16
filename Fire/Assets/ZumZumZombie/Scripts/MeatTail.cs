using System.Collections;
using UnityEngine;

public class MeatTail : MonoBehaviour
{
    public void SetPlayer(PlayerData a)
    {
        playerData = a;
    }

    public bool tailOn;

    public PlayerData playerData;

    //private float DefaultDistance = 7.0f;

    //public float senceDistance;

    private WaitForSeconds Second;

    //private Coroutine MeatProcess;

    public void Awake()
    {
        Second = new WaitForSeconds(2f);
        var x =
        StartCoroutine(CoolDown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tailOn)
        {
            StartCoroutine(PullMeat(other));
        }
    }

    private IEnumerator PullMeat(Collider other)
    {
        for (int i = 0; i < 45; i++)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, playerData.transform.position, Time.deltaTime * 30);
            yield return null;
        }
        tailOn = false;
        yield break;
    }

    private IEnumerator CoolDown()
    {
        while (true)
        {
            if (!tailOn)
            {
                tailOn = true;
            }
            yield return Second;
        }
    }

    //public IEnumerator Meatsense()
    //{
    //    while (tailOn)
    //    {
    //        yield return new WaitForSeconds(2f);
    //        senceDistance = playerData.evolveLvData[11] * DefaultDistance;
    //        var meats = Physics.OverlapSphere(playerData.transform.position, senceDistance, LayerMask.GetMask("Meat"));

    //        if (meats.Length <= 0) continue;

    //        meats[0].transform.position = Vector3.Lerp(meats[0].transform.position, playerData.transform.position, Time.deltaTime * 10.0f);
    //    }
    //}
}