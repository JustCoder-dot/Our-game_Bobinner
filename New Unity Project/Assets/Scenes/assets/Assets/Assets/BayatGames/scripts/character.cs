using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;


public class character : MonoBehaviour {


    [SerializeField]
    private int lives = 5;// personaj joni
    [SerializeField]
    private float speed = 3.0f; // personaj xarakatlanish tezligi o'yinda 5.0f
    [SerializeField]
    private float jumpForce = 15.0f; // personaj sakrash kuchi o'yinda 20.0f
    int lastlevel = 10; // o'yinning oxirgi bosqichi shu bosqichni o'tsa o'yinning 0- bosqichiga o'tip qoladi.
    int coin3 = 0; // faol bosqichda yig'ilgan tangalar soni. 
    bool dead1 = true; // personaj trikligi 
    static  int Adcount = 0;
    //private const string GameOver = "ca-app-pub-5527213689499353/7367062707";
    //private const string banner = "ca-app-pub-5527213689499353/6168589152";
    //private InterstitialAd ad;

    private bool isGrounded = true; // personaj yer uctidaligi.


    private CharState State
    {

        
        get
        {
            return (CharState)animator.GetInteger("State");
        }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        //BannerView bannerV = new BannerView(banner, AdSize.Banner, AdPosition.Bottom);
        //AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("301E1505CF44E4F1").Build();

        //AdRequest request = new AdRequest.Builder().Build();
        //bannerV.LoadAd(request);


    }
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()

    {
        if (Advertisement.isSupported)
            Advertisement.Initialize("3505241");


        if (isGrounded && dead1) {  // pesonaj yerda xamda tirik bo'lsa bajaraladigan ishlar.
            State = CharState.Idl; // pesonaj tinch xolatdagi animatsiya
                                   // State = CharState.Blink;

        }
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
    }
    void OnTriggerEnter2D(Collider2D Player)
    {

        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log("Aktiv scena bu ' " + scene.buildIndex + " ' .");
        int ActiveScene = scene.buildIndex;                 // faol senani (bosqichni) raqamini oladi.
        if ((Player.gameObject.tag == "Respawn") && dead1) { // personajni "Respawn" tegiga kirsa bajariladigan ishlar
            dead1 = false;                                  // personajni o'lgan deymiz
            rigidbody.drag = 100000.0f;                     //personajning havodagi qarshiligini belgiledi.
            Adcount++;
            State = CharState.dead;                                      // personajni olim animatsiyasini qo'yamiz
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - coin3); // personaj shu senada yig'gan tangalarni ayriymiz.
                                                                            // coin2.text = PlayerPrefs.GetInt("coin").ToString();

            Invoke("RestarLevel", 2); // RestartLevel degan funksiyani 2 sekuntdan keyin chaqiramiz chunki animatsiya bajarilishi uchun
            //ad = new InterstitialAd(GameOver);
            //AdRequest request = new AdRequest.Builder().Build();
            //AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("301E1505CF44E4F1").Build();


           // ad.LoadAd(request);
            //ad.IsLoaded();
            //ad.OnAdLoaded += OnAdLoaded;
            //ad.Show();


        }
        else if (Player.gameObject.tag == "coin") // tangaga tegsa bo'ladigan xosilar
        {
            Destroy(Player.gameObject); // tegilgan obyeqtni o'yindan o'chiriladi.(tangani)
            coin3++; // bosqichda yig'iladigan tangalar soni
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 1); //Umumiy tangalar soniga 1 qo'shiladi.
                                                                        // coin2.text = PlayerPrefs.GetInt("coin").ToString();

        }

        else if (Player.tag == "Finish") { // finish tegiga tegsa bo'ladigan xodisalar
            dead1 = false; //personajni olgan deb belgilemiz
            State = CharState.Blink;// ko'zini yumib ochadigan animatsiya yoqamiz
            Invoke("FinishLevel", 3);// keyingi bosqichga o'tadigan funksiyani chaqiramiz

        }
    }
    void RestarLevel() {      // shu bosqichni qayta boshledigan funksiya
        if (Advertisement.IsReady() /*&&  (Adcount % 2 == 0)*/ )
        {
            Advertisement.Show();
                }

        Scene scene = SceneManager.GetActiveScene(); // activ senani "scene" ga teng ledi.
        int ActiveScene = scene.buildIndex; // activ senani int ga o'tkizadi.(shartmas)
        SceneManager.LoadScene(ActiveScene); // aactiv senani yuklaydi
    }

    void FinishLevel() { // finish funksiyasi
        Scene scene = SceneManager.GetActiveScene();//scene ni faol sena bilan tengledi
        int ActiveScene = scene.buildIndex; // scenani raqamini aniqledi

        if (scene.buildIndex + 1 != lastlevel) {//senaing 1 qo'shilgani agar oxirgi senaga teng bo'lmasa shu funksiya bajariladi 
            SceneManager.LoadScene(scene.buildIndex + 1);// keyingi bosqichni yukledi
        }
        if (scene.buildIndex + 1 == lastlevel) { // agar o'yinchi oxirgi bosqichni tugatsa u menuga jo'natiladi.
            SceneManager.LoadScene(0); // 0-sena bu menu
        }


    }

    /////////////////////////////////////////////////////////////////
    private void Run()
    {
        if (dead1) {
            Vector3 direction = transform.right * Input.GetAxis("Horizontal");

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

            sprite.flipX = direction.x <= 0.0f;// Personaj spritini x o'qi bilan ko'zgulashadi.
            if (isGrounded) // agar yerda bo'lsa yugurish animatsiyasi bajariladi
                State = CharState.Run;
        }
    }
    private void Jump() //sakirash funksiyasi
    {
        if (dead1) {// agar trik bo'lsagina sakray oladi 
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void CheckGround()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = colliders.Length > 1;

        if (!isGrounded && dead1) // yerda bo'lmasa xamda xayot bo'lsa sakirash animatsiyasini bajaradi.
            State = CharState.Jump;// ya'ni yiqilsa xam shu animatsiya bajariladi
    }
    
    public void OnAdLoaded(object sender, System.EventArgs args)
    {
        if (!dead1)
        {
      //      ad.Show();
        }
    }

}
public enum CharState
{
	Idl,
	Run,
	Jump,
	dead,
	Blink
}

