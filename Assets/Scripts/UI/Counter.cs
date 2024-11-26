using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Sprite _redDollar;
    [SerializeField] private Sprite _greenDollar;
    [SerializeField] private Image _imageDollar;
    [SerializeField] private TextMeshProUGUI _amountCollect;
    [SerializeField] private Collector _collector;
    [SerializeField] private Color _red;
    [SerializeField] private Color _green;

    private int _amount;

    private void Awake()
    {
        gameObject.SetActive(false);
        _collector.OnCollectItem += ChangeCounter;
    }

    private void OnDestroy()
    {
        _collector.OnCollectItem -= ChangeCounter;
    }

    private void OnDisable()
    {
        _amount = 0;
    }

    private void ChangeCounter(bool isMoney)
    {
        gameObject.SetActive(true);
        _amount += isMoney ? Collector.AmountPointForMoney : Collector.AmountPointForAlcohol;
        _imageDollar.sprite = _amount > 0 ? _greenDollar : _redDollar;
        _amountCollect.color = _amount > 0 ? _green : _red;
        _amountCollect.text = _amount > 0 ? "+" + _amount.ToString() : _amount.ToString(); 
    }

    public void DisableGameObject() => gameObject.SetActive(false); 
}
