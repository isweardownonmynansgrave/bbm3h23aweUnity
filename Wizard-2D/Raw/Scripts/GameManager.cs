using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int score;
    public string state;
    string previousState;
    float timerSessionLength;
    
    //Buttons Benutzeroberfläche - MainMenu & Theater
    Button playButton;
    Button newGameButton;
    Button quitButton;
    Button exitButton;
    GameObject pauseContainer;
    GameObject theaterHud;
    void Awake()
    {
      //Score sicherheitshalber mit 0 initialisieren
      score = 0;
      //Blank State initialisieren
      state = "";

      //Szenenwechsel Notifier "abonnieren"(alternativ in OnEnable Mono Methode) - evtl in OnDisable wieder "deabonnieren"
      SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    
    // Start is called before the first frame update
    void Start()
    {
      //GameManager als Singleton deklarieren und in "DontDestroyOnLoad" Szene laden
      if (gm == null) {
        DontDestroyOnLoad(gameObject);
        gm = this;
      } else {
        Destroy(gameObject);
      }

        ///Buttons
        //initialisieren
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        //Eventlistener hinzufügen
        playButton.onClick.AddListener(StartGame);
        newGameButton.onClick.AddListener(NewGame);
        quitButton.onClick.AddListener(QuitGame);

        //Timer
        timerSessionLength = 180f;
    }
  
    // Update is called once per frame
    void Update()
    {
      //Timer
      if(this.state == "game") {
        timerSessionLength -= Time.deltaTime;
      }
      if (timerSessionLength <= 0) {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Timer auf 0 - Spielende");
        //Score & State reset nachdem der Timer abläuft
        this.state = "";
        score = 0;
      }
      if(Input.GetKeyDown(KeyCode.Escape)) {
        this.previousState = this.state;
        this.state = "pause";
      }
    }
    void OnActiveSceneChanged(Scene p, Scene n)
    {
      Debug.Log("OnActiveSceneChanged");
      if (n.name == "Theater") {
        Debug.Log("OnActiveSceneChanged - n = Theater true");
        //"Zurück zum Hauptmenü" Button angelegt und Eventlistener hinzugefügt
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(QuitToMainMenu);
      } else {
        ///Buttons
        //initialisieren
        playButton = GameObject.Find("playButton").GetComponent<Button>();
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        //Eventlistener hinzufügen
        playButton.onClick.AddListener(StartGame);
        newGameButton.onClick.AddListener(NewGame);
        quitButton.onClick.AddListener(QuitGame);
      }
    }
    //Methoden für Buttons
    public void StartGame()
    {
      SceneManager.LoadScene("Theater"); 
      state = "game";
    }
    public void NewGame()
    {
      //Score & Timer zurücksetzen
      score = 0;
      this.timerSessionLength = 180;

      StartGame();
    }
    public void QuitGame()
    {
      //Spielanwendung beenden
      Debug.Log("QUIT");
      //Application.Quit();
    }
    void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        this.state = "";
    }
    public void PauseMenu()
    {
      pauseContainer = GameObject.Find("PauseMenuContainer");
      theaterHud = GameObject.Find("Hud");
      if (!pauseContainer.activeSelf) {
        pauseContainer.SetActive(true);
        theaterHud.SetActive(false);
        Time.timeScale = 0f;
      } else {
        pauseContainer.SetActive(false);
        theaterHud.SetActive(true);
        Time.timeScale = 1f;
        this.state = this.previousState;
      }
    }

    //Getter
    public int getScore() {
      return score;
    }
    public float getTimerSessionLength() {
      return this.timerSessionLength;
    }

    //Setter
    public void setState(string s)
    {
      this.state = s;
    }
    public void addScore(int a) {
      score += a;
    }
}
