using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public Vector3 vectorOrigin = new Vector3(0, 0.5f, 0);
    public Transform player;
    private Queue xq = new Queue();
    private Queue yq = new Queue();
    public GameObject[,] xyMap = new GameObject[6, 4];

    private enum Way
    { T, B, R, L };

    private void Awake()
    {
        TileGen();
        StartCoroutine(CheckPlayerPosAndTileChange());
    }

    private void TileGen()
    {
        int i = 0;

        for (int y = 125; y > -126; y -= 50)
        {
            int j = 0;
            for (int x = -75; x < 76; x += 50)
            {
                xyMap[i, j] = GameObject.Instantiate(tiles[Random.Range(0, tiles.Length)], new Vector3(x, 0, y), Quaternion.identity);
                j++;
            }
            i++;
        }
    }

    private IEnumerator CheckPlayerPosAndTileChange()
    {
        for (; ; )
        {
            var playerPos = player.position - vectorOrigin;

            if (playerPos.x >= 50f)
            {
                TileChanger(Way.R);
            }
            else if (playerPos.x <= -50f)
            {
                TileChanger(Way.L);
            }
            if (playerPos.z >= 50f)
            {
                TileChanger(Way.T);
                Debug.Log("위쪽 1행 생성");
            }
            else if (playerPos.z <= -50f)
            {
                TileChanger(Way.B);
                Debug.Log("아래쪽 1행 생성");
            }
            Debug.Log(playerPos);
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// 플레이어위치에따라서 타일의 위치를 변경해줌
    /// </summary>
    /// <param name="way"></param>
    private void TileChanger(Way way)
    {
        var lastYIdx = xyMap.GetLength(0) - 1;
        var lastXIdx = xyMap.GetLength(1) - 1;
        switch (way)
        {
            case Way.T:
                for (int i = 0; i < lastXIdx + 1; i++)
                {
                    xyMap[lastYIdx, i].transform.position = new Vector3(xyMap[lastYIdx, i].transform.position.x, 0, xyMap[lastYIdx, i].transform.position.z + 300);
                }
                SwapSort(way);
                break;

            case Way.B:
                for (int i = 0; i < lastXIdx + 1; i++)
                {
                    xyMap[0, i].transform.position = new Vector3(xyMap[0, i].transform.position.x, 0, xyMap[0, i].transform.position.z - 300);
                }
                SwapSort(way);
                break;

            case Way.R:
                for (int i = 0; i < lastYIdx + 1; i++)
                {
                    xyMap[i, 0].transform.position = new Vector3(xyMap[i, 0].transform.position.x + 200, 0, xyMap[i, 0].transform.position.z);
                }
                SwapSort(way);
                break;

            case Way.L:
                for (int i = 0; i < lastYIdx + 1; i++)
                {
                    xyMap[i, lastXIdx].transform.position = new Vector3(xyMap[i, lastXIdx].transform.position.x - 200, 0, xyMap[i, lastXIdx].transform.position.z);
                }
                SwapSort(way);
                break;

            default:
                break;
        }
        vectorOrigin = player.position;
    }

    /// <summary>
    /// 2차원배열에서 스왑하고 정렬하는 함수
    /// </summary>
    /// <param name="way"></param>
    private void SwapSort(Way way)
    {
        GameObject a = null;
        switch (way)
        {
            case Way.T:
                for (int i = 5; i >= 1; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        a = xyMap[i, j];
                        xyMap[i, j] = xyMap[i - 1, j];
                        xyMap[i - 1, j] = a;
                    }
                }
                break;

            case Way.B:
                for (int i = 0; i < 6 - 1; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        a = xyMap[i, j];
                        xyMap[i, j] = xyMap[i + 1, j];
                        xyMap[i + 1, j] = a;
                    }
                }
                break;

            case Way.R:
                for (int i = 0; i < 4 - 1; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        a = xyMap[j, i];
                        xyMap[j, i] = xyMap[j, i + 1];
                        xyMap[j, i + 1] = a;
                    }
                }
                break;

            case Way.L:
                for (int i = 3; i >= 1; i--)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        a = xyMap[j, i];
                        xyMap[j, i] = xyMap[j, i - 1];
                        xyMap[j, i - 1] = a;
                    }
                }
                break;

            default:
                break;
        }
    }
}