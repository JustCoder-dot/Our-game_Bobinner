using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Nexlevel : MonoBehaviour {
    // private const string banner = "ca-app-pub-5527213689499353/6168589152";
	public void Next(){
		SceneManager.LoadScene (1);
	}
   // public void Update()
   // {
      // BannerView bannerV = new BannerView(banner, AdSize.Banner, AdPosition.Bottom);
      // AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("301E1505CF44E4F1").Build(); 
            
      // AdRequest request = new AdRequest.Builder().Build();
      // bannerV.LoadAd(request);
   // }

}
