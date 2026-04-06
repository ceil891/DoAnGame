using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class UnityAdManager : MonoBehaviour
{
    // ID Quảng cáo của bạn
    #if UNITY_ANDROID
        
        string bannerId = "ca-app-pub-3940256099942544/6300978111"; 
        string appOpenId = "ca-app-pub-3940256099942544/9257395921";
    #else
        string bannerId = "unused";
        string appOpenId = "unused";
    #endif

    private BannerView bannerView;
    private AppOpenAd appOpenAd;
    private DateTime loadTime;

    private void Awake()
    {
        // Đảm bảo Script này tồn tại xuyên suốt game, không bị hủy khi đổi màn chơi
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 1. Khởi tạo SDK
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob SDK Initialized.");
            // 2. Load quảng cáo ngay khi khởi tạo xong
            LoadBanner();
            LoadAppOpenAd();
        });

        // 3. Lắng nghe sự kiện để hiện quảng cáo khi người dùng mở lại game từ đa nhiệm
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    private void OnDestroy()
    {
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    // ================= PHẦN 1: BANNER =================
    public void LoadBanner()
    {
        if (bannerView != null) bannerView.Destroy();

        // Tạo Banner ở dưới cùng màn hình
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest();
        bannerView.LoadAd(request);
    }

    // ================= PHẦN 2: APP OPEN AD =================
    public void LoadAppOpenAd()
    {
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        AdRequest request = new AdRequest();
        AppOpenAd.Load(appOpenId, request, (AppOpenAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("App Open Ad failed to load: " + error);
                return;
            }
            
            appOpenAd = ad;
            loadTime = DateTime.Now;

            // Hiển thị ngay lần đầu tiên mở game (tuỳ chọn)
            ShowAppOpenAd(); 
        });
    }

    public void ShowAppOpenAd()
    {
        // App Open Ad có hạn sử dụng 4 tiếng, nếu quá giờ phải load lại
        if (appOpenAd != null && appOpenAd.CanShowAd() && (DateTime.Now - loadTime).TotalHours < 4)
        {
            appOpenAd.Show();
        }
        else
        {
            LoadAppOpenAd();
        }
    }

    // Hàm này tự động chạy khi người dùng ẩn app và mở lại
    private void OnAppStateChanged(AppState state)
    {
        if (state == AppState.Foreground)
        {
            ShowAppOpenAd();
        }
    }
}