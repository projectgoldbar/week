using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatTail : MonoBehaviour
{
    private PlayerData playerData;

    private float DefaultDistance = 7.0f;

    private float senceDistance;

    private WaitForSeconds Second;

    private Coroutine MeatProcess;

    public void Awake()
    {
        Second = new WaitForSeconds(0.01f);
        playerData = FindObjectOfType<PlayerData>();

        senceDistance = playerData.evolveLvData[11] * DefaultDistance;
    }

    private void OnEnable()
    {
        MeatProcess = StartCoroutine(Meatsense());
    }

    private void OnDisable()
    {
        if (MeatProcess != null)
            StopCoroutine(MeatProcess);
    }

    public IEnumerator Meatsense()
    {
        while (senceDistance != 0 && playerData != null)
        {
            var meats = Physics.OverlapSphere(playerData.transform.position, senceDistance, LayerMask.GetMask("Meat"));

            if (meats.Length <= 0) continue;

            for (int i = 0; i < meats.Length; i++)
            {
                meats[i].transform.position = Vector3.Lerp(meats[i].transform.position, playerData.transform.position, Time.deltaTime * 10.0f);
                //Debug.Log(meats[i].name+i);
            }

            yield return Second;
        }
    }
}