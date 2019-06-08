using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSence : MonoBehaviour
{
    // Start is called before the first frame update

    public Blast blast;

    private float Timer = 0.5f;
    private float CurrentTimer;
    private int Seconds = 3;

    public bool Exflag = false;


    private WaitForSeconds waitSeconds;

    private void Awake()
    {
        waitSeconds = new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Exflag) return;

        CurrentTimer += Time.deltaTime;

        if (CurrentTimer >= Timer)
        {
            var objcollider = Physics.OverlapSphere(blast.transform.position , blast.Distance , LayerMask.GetMask("Player"));
            if (objcollider.Length > 0)
            {
                StartCoroutine(Explosion(Seconds));
            }

            CurrentTimer = 0;
        }
    }

    private IEnumerator Explosion(int n)
    {
        for (int i = n; i >= 0; i--)
        {
            Debug.Log($"폭파 {i}초전");
            yield return waitSeconds;
        }
        blast.gameObject.SetActive(true);

        Exflag = true;

        yield return null;
    }


}
