using UnityEngine;

public class RandomBoxManager : MonoBehaviour
{
    public static RandomBoxManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float GoldBoxOpen(int Lv, int combo = 0)
    {
        //파티클이펙트 실행
        return (combo + 1 * 3) * Random.Range(Lv * 1000, Lv * 3000);
    }

    public int SkinBoxOpen(int lastCount)
    {
        //파티클이펙트 실행
        return Random.Range(1, lastCount);
    }
}