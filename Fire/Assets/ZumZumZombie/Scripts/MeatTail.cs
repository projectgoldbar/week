using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatTail : MonoBehaviour
{
    public void GetMeat(PlayerData a )
    {
        playerData = a;
    }


    public PlayerData playerData;

    private float DefaultDistance = 7.0f;

    public float senceDistance;

    private WaitForSeconds Second;

    private Coroutine MeatProcess;

    public void Awake()
    {
        Second = new WaitForSeconds(0.01f);
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
       
        while (true)
        {
            yield return null;
            senceDistance = playerData.evolveLvData[11] * DefaultDistance;
            var meats = Physics.OverlapSphere(playerData.transform.position, senceDistance, LayerMask.GetMask("Meat"));

            if (meats.Length <= 0) continue;

               
            meats[0].transform.position = Vector3.Lerp(meats[0].transform.position, playerData.transform.position, Time.deltaTime * 10.0f);
        }
    }
}