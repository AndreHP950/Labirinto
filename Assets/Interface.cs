using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    public static Interface Instance;

    [Header("Lobby (somente InitScene)")]
    public GameObject playButton;
    public Transform cameraTargetPosition;
    public float cameraZoomSpeed = 2f;

    [Header("Sistema de som")]
    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip playButtonSFX;

    [Header("Pontuação")]
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private bool playPressed = false;
    private bool jaComecouJogo = false;
    private bool isZooming = false;

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

    void Update()
    {
        if (isZooming && cameraTargetPosition != null)
        {
            Transform cam = Camera.main.transform;

            cam.position = Vector3.MoveTowards(
                cam.position,
                cameraTargetPosition.position,
                Time.unscaledDeltaTime * cameraZoomSpeed
            );

            cam.rotation = Quaternion.RotateTowards(
                cam.rotation,
                cameraTargetPosition.rotation,
                Time.unscaledDeltaTime * cameraZoomSpeed * 100f
            );

            float dist = Vector3.Distance(cam.position, cameraTargetPosition.position);
            float rotDiff = Quaternion.Angle(cam.rotation, cameraTargetPosition.rotation);

            if (dist < 0.05f && rotDiff < 1f)
            {
                cam.position = cameraTargetPosition.position;
                cam.rotation = cameraTargetPosition.rotation;

                isZooming = false;
                Time.timeScale = 1f;
            }
        }
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

        // Começa a animação de zoom
        isZooming = true;
        Time.timeScale = 0f;
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
