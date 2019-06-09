using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Field
{
    public Transform up;
    public Transform left;
}

public class CoinSpwaner : MonoBehaviour
{
    public List<GameObject> coinPool;
    public List<GameObject> meatPool;
    private SectorManager sectorManager;

    public List<Field> field;
    public GameObject coin;
    public GameObject meat;
    private WaitForSeconds twoSecond;

    private void Awake()
    {
        sectorManager = FindObjectOfType<SectorManager>();
        twoSecond = new WaitForSeconds(2f);
        for (int i = 0; i < 100; i++)
        {
            coinPool.Add(Instantiate(coin, this.transform.position, Quaternion.identity));
            meatPool.Add(Instantiate(coin, this.transform.position, Quaternion.identity));
        }
        //for (int i = 0; i < 100; i++)
        //{
        //    Spwan();
        //}
        //for (int i = 0; i < 100; i++)
        //{
        //    MeatSpwan();
        //}

        // StartCoroutine(CoinSpwan(300));
    }

    private IEnumerator CoinSpwan(int x)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < 5f; j++)
            {
                Spwan();
                MeatSpwan();
            }
            yield return twoSecond;
        }
    }

    private void Spwan()
    {
        var u = Random.Range(0, field.Count);
        float x = field[u].up.position.x;
        float x2 = field[u].left.position.x;
        float y = field[u].up.position.z;
        float y2 = field[u].left.position.z;
        float a = Random.Range(x, x2);
        float b = Random.Range(y, y2);

        for (int i = 0; i < coinPool.Count; i++)
        {
            if (!coinPool[i].activeSelf)
            {
                coinPool[i].transform.position = new Vector3(a, 1.7f, b);
                coinPool[i].SetActive(true);
                return;
            }
        }
        var tt = Instantiate(coin, transform.position, Quaternion.identity);
        tt.SetActive(false);
        coinPool.Add(tt);
        tt.transform.position = new Vector3(a, 1.7f, b);
        tt.SetActive(true);
        return;
    }

    private void MeatSpwan()
    {
        var u = Random.Range(0, field.Count);
        float x = field[u].up.position.x;
        float x2 = field[u].left.position.x;
        float y = field[u].up.position.z;
        float y2 = field[u].left.position.z;
        float a = Random.Range(x, x2);
        float b = Random.Range(y, y2);

        for (int i = 0; i < meatPool.Count; i++)
        {
            if (!meatPool[i].activeSelf)
            {
                meatPool[i].transform.position = new Vector3(a, 1.7f, b);
                meatPool[i].SetActive(true);
                return;
            }
        }
        var tt = Instantiate(meat, transform.position, Quaternion.identity);
        tt.SetActive(false);
        meatPool.Add(tt);
        tt.transform.position = new Vector3(a, 1.7f, b);
        tt.SetActive(true);
        return;
    }

    //public void SpwanCoin(int[] spwanSectorIdx)
    //{
    //    for (int i = 0; i < spwanSectorIdx.Length; i++)
    //    {
    //        if (sectorManager.sectors[spwanSectorIdx[i]].currentCoin < sectorManager.sectors[spwanSectorIdx[i]].maxCoin)
    //        {
    //            var coin = GetCoin();
    //            var point = FindPoint(spwanSectorIdx[i]);
    //            coin.GetComponent<Coin>().coinSection = spwanSectorIdx[i];
    //            sectorManager.sectors[spwanSectorIdx[i]].currentCoin++;
    //            coin.transform.position = point;
    //            coin.SetActive(true);
    //        }
    //    }
    //}

    //private GameObject GetCoin()
    //{
    //    for (int i = 0; i < coinPool.Count; i++)
    //    {
    //        if (!coinPool[i].activeSelf)
    //        {
    //            return coinPool[i];
    //        }
    //    }
    //    var newCoin = Instantiate(coin, transform.position, Quaternion.identity);
    //    newCoin.SetActive(false);
    //    coinPool.Add(newCoin);
    //    return newCoin;
    //}

    //private Vector3 FindPoint(int sectorIdx)
    //{
    //    var min = sectorManager.bounds[sectorIdx].min;
    //    var max = sectorManager.bounds[sectorIdx].max;
    //    var x = Random.Range(min.x, max.x);
    //    var z = Random.Range(min.z, max.z);
    //    Vector3 targetVector = new Vector3(x, 1.7f, z);

    //    for (int i = 0; i < 50; i++)
    //    {
    //        if (!SomethingOnPlace(targetVector))
    //        {
    //            //var coin = GetCoin();
    //            //coin.transform.position = new Vector3(x, 1.7f, z);
    //            //coin.SetActive(true);
    //            return targetVector;
    //        }
    //    }
    //    return new Vector3(1000f, 1.7f, 1000f);
    //}

    //private bool SomethingOnPlace(Vector3 point)
    //{
    //    Vector3 rayStartPoint = new Vector3(point.x, point.y + 80f, point.z);
    //    Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
    //    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 200f, 1 << 11))
    //    {
    //        rayStartPoint.y = point.y;
    //        rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Left, 1f);
    //        Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
    //        if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
    //        {
    //            rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Right, 1f);
    //            Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
    //            if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
    //            {
    //                rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Back, 1f);
    //                Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
    //                if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
    //                {
    //                    rayStartPoint = PivotPointSet(rayStartPoint, point, Direction.Foward, 1f);
    //                    Debug.DrawRay(rayStartPoint, point - rayStartPoint, Color.red, 100f);
    //                    if (!Physics.Raycast(rayStartPoint, point - rayStartPoint, 2f, 1 << 11))
    //                    {
    //                        return false;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return true;
    //}

    ///// <summary>
    ///// 피벗기준으로 rotation방향으로 distance만큼 떨어진 위치를 반환
    ///// </summary>
    ///// <param name="pivot"></param>
    ///// <param name="rotation"></param>
    //private Vector3 PivotPointSet(Vector3 pivot, Vector3 origin, Direction direction, float distance)
    //{
    //    switch (direction)
    //    {
    //        case Direction.Left:
    //            pivot = origin;
    //            pivot.x -= distance;
    //            break;

    //        case Direction.Right:
    //            pivot = origin;
    //            pivot.x += distance;
    //            break;

    //        case Direction.Foward:
    //            pivot = origin;
    //            pivot.z += distance;

    //            break;

    //        case Direction.Back:
    //            pivot = origin;
    //            pivot.z -= distance;

    //            break;

    //        default:
    //            break;
    //    }
    //    return pivot;
    //}
}