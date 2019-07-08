using System.Collections;
using UnityEngine;

public class TestCoinSpwan : MonoBehaviour
{
    public Transform player;
    public CoinPool coinPool;

    public IEnumerator coroutine;
    public IEnumerator meatCoroutine;

    private void Start()
    {
        coroutine = SpwanCoroutine();
        meatCoroutine = SpwanMeatCoroutine();
        StartCoroutine(coroutine);
        StartCoroutine(SpwanMeatCoroutine());
    }

    public void SpwanGold()
    {
        StartCoroutine(coroutine);
    }

    public void StopSpwan()
    {
        StopCoroutine(coroutine);
    }

    public void SpwanMeatMethod()
    {
        StartCoroutine(meatCoroutine);
    }

    public void StopMeatMethod()
    {
        StopCoroutine(meatCoroutine);
    }

    private IEnumerator SpwanMeatCoroutine()
    {
        WaitForSeconds second = new WaitForSeconds(10f);
        while (true)
        {
            yield return second;
            for (int j = 0; j < 3; j++)
            {
                SpwanMeat();
            }
            yield return null;
        }
    }

    private IEnumerator SpwanCoroutine()
    {
        WaitForSeconds second = new WaitForSeconds(10f);
        while (true)
        {
            yield return second;
            for (int j = 0; j < 10; j++)
            {
                Spwan();
            }
            yield return null;
        }
    }

    private void SpwanMeat()
    {
        GameObject meat = null;
        for (int i = 0; i < coinPool.meatPool.Count; i++)
        {
            if (!coinPool.meatPool[i].activeSelf)
            {
                meat = coinPool.meatPool[i];
                break;
            }
        }
        if (meat == null)
        {
            return;
        }

        var point = FindFarPoint(player.position, 40f, 80f);

        //coin.GetComponent<Coin>().coinSection = sectorNumber;
        //currentCoin++;
        meat.transform.position = point;
        meat.SetActive(true);
    }

    private void Spwan()
    {
        GameObject coin = null;
        for (int i = 0; i < coinPool.coinPool.Count; i++)
        {
            if (!coinPool.coinPool[i].activeSelf)
            {
                coin = coinPool.coinPool[i];
                break;
            }
        }

        if (coin == null)
        {
            return;
        }

        var point = FindFarPoint(player.position, 20f, 50f);

        //coin.GetComponent<Coin>().coinSection = sectorNumber;
        //currentCoin++;
        coin.transform.position = point;
        coin.SetActive(true);
    }

    public Vector3 FindFarPoint(Vector3 pivot, float minDistance = 90f, float maxDistance = 110f)
    {
        for (int i = 0; i < 50; i++)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(0f, 360f);
            float radian = angle * Mathf.Deg2Rad;

            var point = pivot + (new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * distance);
            if (!SomethingOnPlace(pivot))
            {
                return point;
            }
        }
        return new Vector3(1000f, 1.7f, 1000f);
    }

    public Vector3 FindPoint()
    {
        var bounds = GetComponent<BoxCollider>().bounds;
        var min = bounds.min;
        var max = bounds.max;

        for (int i = 0; i < 50; i++)
        {
            var x = Random.Range(min.x, max.x);
            var z = Random.Range(min.z, max.z);
            Vector3 targetVector = new Vector3(x, 1.7f, z);
            if (!SomethingOnPlace(targetVector))
            {
                return targetVector;
            }
        }
        return new Vector3(1000f, 1.7f, 1000f);
    }

    private bool SomethingOnPlace(Vector3 point)
    {
        Vector3 rayStartPoint = new Vector3(point.x, point.y + 80f, point.z);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// 피벗기준으로 rotation방향으로 distance만큼 떨어진 위치를 반환
    /// </summary>
    /// <param name="pivot"></param>
    /// <param name="rotation"></param>
    private Vector3 PivotPointSet(Vector3 pivot, Vector3 origin, Direction direction, float distance)
    {
        switch (direction)
        {
            case Direction.Left:
                pivot = origin;
                pivot.x -= distance;
                break;

            case Direction.Right:
                pivot = origin;
                pivot.x += distance;
                break;

            case Direction.Foward:
                pivot = origin;
                pivot.z += distance;

                break;

            case Direction.Back:
                pivot = origin;
                pivot.z -= distance;

                break;

            default:
                break;
        }
        return pivot;
    }
}