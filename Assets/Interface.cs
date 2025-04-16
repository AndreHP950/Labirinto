using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    public static Interface Instance;

    [Header("Lobby (somente InitScene)")]
    public GameObject playButton;

    [Header("Sistema de som")]
    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip playButtonSFX;

    [Header("Pontuação")]
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private bool playPressed = false;
    private bool jaComecouJogo = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        HandleScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleScene(scene.name);
    }

    void HandleScene(string sceneName)
    {
        if (sceneName == "InitiScene")
        {
            if (jaComecouJogo)
            {
                if (playButton != null)
                    playButton.SetActive(false);

                Time.timeScale = 1f;
            }
            else
            {
                if (playButton != null)
                    playButton.SetActive(true);

                Time.timeScale = 0f;

                if (audioSource != null && backgroundMusic != null && !audioSource.isPlaying)
                {
                    audioSource.clip = backgroundMusic;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
        }
        else
        {
            jaComecouJogo = true;

            if (playButton != null)
                playButton.SetActive(false);

            Time.timeScale = 1f;
        }

        if (scoreText == null)
            scoreText = FindAnyObjectByType<TextMeshProUGUI>();

        UpdateScoreText();
    }

    public void RegistrarTextoPontuacao(TextMeshProUGUI texto)
    {
        scoreText = texto;
        UpdateScoreText();
    }

    public void OnPlayButtonPressed()
    {
        if (playPressed) return;
        playPressed = true;
        jaComecouJogo = true;

        if (audioSource != null && playButtonSFX != null)
            audioSource.PlayOneShot(playButtonSFX);

        if (playButton != null)
            playButton.SetActive(false);

        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Inicia o jogo normalmente
        Time.timeScale = 1f;
    }

    public bool JogoJaComecou()
    {
        return jaComecouJogo;
    }

    public int GetPontos()
    {
        return score;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();

        if (audioSource != null && playButtonSFX != null)
            audioSource.PlayOneShot(playButtonSFX);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Pontos: {score}";
    }
}
