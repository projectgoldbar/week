using UnityEngine;
using UnityEngine.Advertisements; 
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Ads : MonoBehaviour
{
    private string RewardVideo_id = "rewardedVideo";

    public UnityEvent Finished;
    public UnityEvent Skipped;
    public UnityEvent Failed;


    public void ShowAD()
    {
        if (Advertisement.IsReady(RewardVideo_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(RewardVideo_id, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");

                    // to do ...
                    // 광고 시청이 완료되었을 때 처리
                    Finished?.Invoke();
                    break;
                }
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped before reaching the end.");

                    // to do ...
                    // 광고가 스킵되었을 때 처리
                    Skipped?.Invoke();
                    break;
                }
            case ShowResult.Failed:
                {
                    Debug.LogError("The ad failed to be shown.");

                    // to do ...
                    // 광고 시청에 실패했을 때 처리
                    Failed?.Invoke();
                    break;
                }
        }
    }
}