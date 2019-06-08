﻿using UnityEngine;

public class Sector : MonoBehaviour
{
    public CoinPool coinPool;
    public SectorManager sectorManager;
    public int sectorNumber = 0;
    public int maxCoin = 10;
    public int currentCoin = 0;

    public int[] spwanSectorNumber;

    private void Awake()
    {
        coinPool = FindObjectOfType<CoinPool>();
        sectorManager = FindObjectOfType<SectorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < coinPool.coinPool.Count; i++)
        {
            var coin = coinPool.coinPool[i].GetComponent<Coin>();
            var count = 0;
            for (int j = 0; j < spwanSectorNumber.Length; j++)
            {
                if (coin.coinSection == spwanSectorNumber[j] || coin.coinSection == sectorNumber)
                {
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                coinPool.coinPool[i].SetActive(false);
                count = 0;
            }
        }

        for (int i = 0; i < spwanSectorNumber.Length; i++)
        {
            sectorManager.sectors[spwanSectorNumber[i]].SpwanCoin();
        }
    }

    public void SpwanCoin()
    {
        if (currentCoin < maxCoin)
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

            var point = FindPoint();
            coin.GetComponent<Coin>().coinSection = sectorNumber;
            currentCoin++;
            coin.transform.position = point;
            coin.SetActive(true);
        }
    }

    private Vector3 FindPoint()
    {
        var bounds = GetComponent<BoxCollider>().bounds;
        var min = bounds.min;
        var max = bounds.max;
        var x = Random.Range(min.x, max.x);
        var z = Random.Range(min.z, max.z);
        Vector3 targetVector = new Vector3(x, 1.7f, z);

        for (int i = 0; i < 50; i++)
        {
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
        Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
        {
            rayStartPoint.y = point.y;
            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
            Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
            {
                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
                Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                {
                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
                    Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
                    {
                        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
                        Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
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