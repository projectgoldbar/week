using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int[,] ByteMap; // 2차원 맵의 좌표값
    public GameObject[] BuildingPreset; // 찍어낼 건물의 프리셋
    public int MapSizeX = 400;// 최대 맵 사이즈x
    public int MapSizeY = 400;// 최대 맵 사이즈y
    public int NumOfBuilding; // 찍어낼 건물의 최대 기대값


    private void Awake()
    {
        ByteMap = new int[MapSizeX, MapSizeY]; 
        GenBuilding(NumOfBuilding);
    }

    /// <summary>
    /// 빌딩 찍어내는 함수
    /// </summary>
    /// <param name="NumofBuild">찍어낼 빌딩 개수</param>
    public void GenBuilding(int NumofBuild)// 빌딩을 찍어냄
    {

        for (int i = 0; i < NumofBuild; i++)
        {
            int vectorX = Random.Range(0, 255);
            int vectorY = Random.Range(0, 255);
            Vector3 instantVector3 = new Vector3(vectorX, 0, vectorY);
            var building = BuildingPreset[Random.Range(0, BuildingPreset.Length)];
            var buildingSize = building.GetComponent<BoxCollider>().size;

            if (BuildingChk((int)buildingSize.x, (int)buildingSize.z, vectorX, vectorY))
            {
                GameObject.Instantiate(building, instantVector3, Quaternion.identity);
            }
            
        }
    }


    bool BuildingChk(int sizeX, int sizeY, int startX, int startY)
    {
        bool result = true;

        for (int q = 0; q < sizeX; q++)
        {
            for (int w = 0; w < sizeY; w++)
            {
                if (ByteMap[startX + q, startY + w] != 1)
                {
                    ByteMap[startX + q, startY + w] = 1;
                }
                else
                {
                    result = false;
                    break;
                }
            }
        }
        return result;
    }
}


   

