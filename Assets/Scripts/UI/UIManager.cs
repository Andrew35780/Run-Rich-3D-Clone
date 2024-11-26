using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _numberLevel;
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private Image _sliderStatus;
    [SerializeField] private Collector _collector;
    [SerializeField] private StatusInfo[] _statusInfo;

    [SerializeField] private PlayerController _playerScript;
    [SerializeField] private TextMeshProUGUI _amountMoney;
    [SerializeField] private GameObject _settingButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private TextMeshProUGUI _getButtonText;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;

    private GameObject _activeSkin;

    public StatusInfo[] StatusInfo => _statusInfo;

    private void Awake()
    {
        _playerScript.enabled = false;
        _activeSkin = _statusInfo[0].skin;
        _numberLevel.text = "Óðîâåíü " + (PlayerPrefs.GetInt("LevelCompleted", 0) + 1);
        _amountMoney.text = PlayerPrefs.GetInt("Money", 0).ToString();
        ChangeStatus(true);
    }

    private void Update()
    {
        if(!_playerScript.enabled && Input.GetMouseButtonDown(0))
        {
            _playerScript.enabled = true;
            _score.gameObject.SetActive(true);
            _numberLevel.gameObject.SetActive(true);
            _settingButton.SetActive(false);
            _exitButton.SetActive(true);
            _tutorial.SetActive(false);
            _statusText.transform.parent.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        _collector.OnCollectItem += ChangeScore;
        _collector.OnCollectItem += ChangeStatus;
    }

    private void OnDisable()
    {
        _collector.OnCollectItem -= ChangeScore;
        _collector.OnCollectItem -= ChangeStatus;
    }

    public void OpenWinWindow(int money)
    {
        _playerScript.enabled = false;
        _numberLevel.text = "Óðîâåíü " + PlayerPrefs.GetInt("LevelCompleted", 0) + "\n ÇÀÂÅÐØ¨Í";
        _winWindow.SetActive(true);
        _getButtonText.text = money.ToString();
        _exitButton.SetActive(false);
        _audioSource.PlayOneShot(_audioClips[0]);
    }

    public void OpenLoseWindow()
    {
        _playerScript.enabled = false;
        _loseWindow.SetActive(true);
        _numberLevel.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        _audioSource.PlayOneShot(_audioClips[1]);
    }

    public void OpenNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeScore(bool isMoney)
    {
        _score.text = _collector.Score.ToString();
        if (_collector.Score <= 0)
            OpenLoseWindow();
    }

    private void ChangeStatus(bool isMoney)
    {
        for(int i = _statusInfo.Length - 1 ; i >= 0; i--)
        {
            if (_collector.Score >= _statusInfo[i].value)
            {
                _activeSkin.SetActive(false);
                _statusInfo[i].skin.SetActive(true);
                _activeSkin = _statusInfo[i].skin;
                _statusText.text = _statusInfo[i].name;
                _statusText.color = _statusInfo[i].colorText;
                _sliderStatus.fillAmount =  (float)_collector.Score / Collector.MaxScore;
                _sliderStatus.color = _statusInfo[i].colorText;
                return;
            }
        }
    }
}
