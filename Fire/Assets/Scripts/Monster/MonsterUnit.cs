using System.Collections;
using UnityEngine;

public class MonsterUnit : MonoBehaviour
{
    #region 변수들

    public float hp = 10;

    public float atkPoint = 1;

    // [System.NonSerialized]
    public float Distance = 0;

    // [System.NonSerialized]
    public bool Check = false;

    // [System.NonSerialized]
    public bool AttackCheck = false;

    // [System.NonSerialized]
    public Transform Target = null;

    #endregion 변수들

    private void Start()
    {
        StartCoroutine(DistanceCheck());
    }

    private IEnumerator DistanceCheck()
    {
        while (true)
        {
            Vector3 dir = (Ref.Instance.playerTr.position - transform.position);
            Distance = dir.magnitude;

            if (Distance < 10000)
            {
                Check = true;
                Target = Ref.Instance.playerTr;
            }
            if (Check)
            {
                if (Distance <= 2) AttackCheck = true;
                else AttackCheck = false;
            }

            yield return null;
        }
    }
}