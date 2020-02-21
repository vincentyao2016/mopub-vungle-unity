using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    string adUnitId = Constant.AD_UNIT_ID;
    string interstitialAdUnitId = Constant.INTERSTITIAL_UNIT_ID;
    string rewardAdUnitId = Constant.REWARD_UNIT_ID;
    string bannerAdUnitId = Constant.BANNER_UNIT_ID;
    string mrecAdUnitId = Constant.MREC_UNIT_ID;

    void OnGUI()
    {
        GUI.backgroundColor = Color.green;
        GUILayoutOption[] option = new GUILayoutOption[]
        {
            GUILayout.Height(80)
        };
        Texture2D texture = new Texture2D(128, 128);

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.background = texture;
        Destroy(texture);
        style.normal.textColor = Color.yellow;
        style.fontSize = 48;

        style.active.textColor = Color.blue;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();

        GUILayout.Space(20);
        GUIStyle lableStyle = new GUIStyle();
        lableStyle.fontSize = 56;
        lableStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Mopub + Vungle", lableStyle);

        if (GUILayout.Button("Init", style, option))
        {
            initMediation();
        }

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Load Interstitial", style, option))
        {
            MoPub.RequestInterstitialAd(interstitialAdUnitId);
        }
        if (GUILayout.Button("Play Interstitial", style, option))
        {
            MoPub.ShowInterstitialAd(interstitialAdUnitId);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Reward", style, option))
        {
            MoPub.RequestRewardedVideo(rewardAdUnitId);
        }
        if (GUILayout.Button("Play Reward", style, option))
        {
            MoPub.ShowRewardedVideo(rewardAdUnitId);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Banner", style, option))
        {
            loadBanner();
        }
        if (GUILayout.Button("Play Banner", style, option))
        {
            playBanner();
        }
        if (GUILayout.Button("Destroy Banner", style, option))
        {
            MoPub.DestroyBanner(bannerAdUnitId);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load MREC", style, option))
        {
            loadMREC();
        }
        if (GUILayout.Button("Play MREC", style, option))
        {
            playMREC();
        }
        if (GUILayout.Button("Destroy MREC", style, option))
        {
            MoPub.DestroyBanner(mrecAdUnitId);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void initMediation()
    {
        var mopConfig = new MoPub.SdkConfiguration();

        mopConfig.LogLevel = MoPub.LogLevel.Debug;
        mopConfig.AdUnitId = adUnitId;
        MoPub.InitializeSdk(mopConfig);

        string[] interstitials = { interstitialAdUnitId };
        MoPub.LoadInterstitialPluginsForAdUnits(interstitials);

        string[] rewards = { rewardAdUnitId };
        MoPub.LoadRewardedVideoPluginsForAdUnits(rewards);

        string[] banners = { bannerAdUnitId, mrecAdUnitId };
        MoPub.LoadBannerPluginsForAdUnits(banners);

        MoPubManager.OnSdkInitializedEvent += OnSdkInitializedEvent;
    }

    void loadBanner()
    {
        MoPub.RequestBanner(bannerAdUnitId, MoPub.AdPosition.TopCenter, MoPub.MaxAdSize.Width320Height50);
    }

    void playBanner()
    {
        MoPub.ShowBanner(bannerAdUnitId, true);
    }

    void loadMREC()
    {
        MoPub.RequestBanner(mrecAdUnitId, MoPub.AdPosition.BottomCenter, MoPub.MaxAdSize.Width300Height250);
    }

    void playMREC()
    {
        MoPub.ShowBanner(mrecAdUnitId, true);
    }

    #region MoPub event handler
    void OnSdkInitializedEvent(string adUnitId)
    {
        // The SDK is initialized here. Ready to make ad requests.
        Debug.Log("OnSdkInitializedEvent" + adUnitId);
    }

    void OnInterstitialFailedEvent(string adUnitId, string errorCode)
    {

        Debug.Log("OnInterstitialFailedEvent" + adUnitId + ", error code:" + errorCode);
    }
    #endregion
}
