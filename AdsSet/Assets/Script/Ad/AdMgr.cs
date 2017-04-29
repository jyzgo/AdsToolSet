

using GoogleMobileAds.Api;
using MTUnity;
using System;
using UnityEngine;

public static class AdMgr  {
    #region Admob-------------------------

    const string IOS_INTERSTITIAL_ID = "";
    const string AND_INTERSTITIAL_ID = "";

    const string IOS_BANNER_ID = "";
    const string AND_BANNER_ID = "";

    public static void ShowAdmobInterstitial()
    {
        _interstitial.Show();
    }

    public static void PreloadAdmobInterstitial()
    {

#if UNITY_ANDROID
        string adUnitId = AND_INTERSTITIAL_ID;

#elif UNITY_IOS
        string adUnitId = IOS_INTERSTITIAL_ID;
        //MAGIC_IOS_INTERSTITIAL_ID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        _interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.OnAdLoaded += HandleOnLoaded;
        _interstitial.OnAdClosed += HandleOnClosed;
        _interstitial.OnAdOpening += HandleOnOpening;
        _interstitial.OnAdFailedToLoad += HandleOnFailedToLoad;
        // Load the interstitial with the request.
        _interstitial.LoadAd(request);

    }

    static void HandleOnOpening(object sender, EventArgs args)
    {
        TrackAdMob("0");
    }

    static void HandleOnClosed(object sender, EventArgs args)
    {
        TrackAdMob("1");
    }

    private static void HandleOnFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        TrackAdMob("2");
    }

    static void HandleOnLoaded(object sender, EventArgs args)
    {
        TrackAdMob("3");
    }

    


    static InterstitialAd _interstitial;

    public static void TrackAdMob(string idStr)
    {
        //TrackAd(idStr, "admobMedi");
    }





		public static int bannerCount = 0;
    public static void ShowAdmobBanner()
    {
        if (_bannerView != null)
        {
			bannerCount++;
            _bannerView.Show();
        }
    }

    public static void HideAdmobBanner()
    {
        if (_bannerView != null)
        {
			bannerCount--;
			if (bannerCount <= 0) 
			{
				_bannerView.Hide();
				bannerCount = 0;
			
			}
           
        }
    }


    public static int downBannerCount = 0;
    public static void ShowDownAdmobBanner()
    {
        if (_downBannerView != null)
        {
            downBannerCount++;
            _downBannerView.Show();
        }
    }

    public static void HideDownAdmobBanner()
    {
        if (_downBannerView != null)
        {
            downBannerCount--;
            if (downBannerCount <= 0)
            {
                _downBannerView.Hide();
                downBannerCount = 0;

            }

        }
    }





    static void  InitBanner()
    {
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
#if UNITY_ANDROID
        string adUnitId = //MAGIC_IOS_BANNER_ID;
            AND_BANNER_ID;
#elif UNITY_IOS
        string adUnitId = //MAGIC_IOS_BANNER_ID;
        IOS_BANNER_ID;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        _bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        _bannerView.LoadAd(request);

        _bannerView.Hide();

        AdRequest downRequest = new AdRequest.Builder().Build();
        _downBannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        _downBannerView.LoadAd(downRequest);
        _downBannerView.Hide();





    }

    static BannerView _bannerView;
    static BannerView _downBannerView;


    public static bool IsAdmobInterstitialReady()
    {
		if (_interstitial == null) 
		{
			return false;
		}
        return _interstitial.IsLoaded();
    }





#endregion Admob



}
