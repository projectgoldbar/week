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
    public List<Field> field;
    public GameObject coin;
    public GameObject meat;
    private WaitForSeconds twoSecond;

    private void Awake()
    {
        twoSecond = new WaitForSeconds(2f);
        for (int i = 0; i < 50; i++)
        {
            coinPool.Add(Instantiate(coin, this.transform.position, Quaternion.identity));
            meatPool.Add(Instantiate(coin, this.transform.position, Quaternion.identity));
        }
        for (int i = 0; i < 100; i++)
        {
            Spwan();
        }
        for (int i = 0; i < 100; i++)
        {
            MeatSpwan();
        }

        StartCoroutine(CoinSpwan(10));
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
}