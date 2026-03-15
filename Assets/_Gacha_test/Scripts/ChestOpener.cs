using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gacha_Test
{
  public class ChestOpener : MonoBehaviour
  {
    [SerializeField] private Animator _chestAnimator;

    [Header("Ui элементы")]
    [SerializeField] private GameObject _uiElements;
    [SerializeField] private Button _openChestButton;
    [SerializeField] private GameObject _clickCatcher;
    
    [Header("Эффекты")]
    [SerializeField] private EffectsActivator _effectsActivator;

    [Header("Количество сундуков и монет")] 
    [SerializeField] private int _chestsCount = 1;
    [SerializeField] private int _coinsCount = 0;

    [Header("Настройки открытия сундука")]
    [SerializeField] private int _chestsToOpen = 1;
    [SerializeField] private int _coinsFromOneChest = 100;
    [SerializeField] private float _openDelay = 2f;

    public int ChestsCount => _chestsCount;
    public int CoinsCount => _coinsCount;

    public event Action ChestOpened;
    
    private Coroutine _openChestCoroutine;

    private void OnEnable()
      => _openChestButton.onClick.AddListener(OpenChest);

    private void Start()
    {
      _clickCatcher.SetActive(false);
      ShowUiElements(ChestsCount > 0);
    }

    private void OnDisable()
      => _openChestButton.onClick.RemoveListener(OpenChest);

    private void OpenChest()
    {
      if (ChestsCount < _chestsToOpen || _openChestCoroutine != null)
      {
        Debug.Log("Нельзя открыть сундук!");
        return;
      }
      
      _openChestCoroutine = StartCoroutine(OpeningChest());
    }

    private IEnumerator OpeningChest()
    {
      _clickCatcher.SetActive(true);
      _chestAnimator.SetTrigger("Open");
      _effectsActivator.Activate(true);
      ShowUiElements(false);
      
      yield return new WaitForSeconds(_openDelay);

      _chestsCount -= _chestsToOpen;
      _coinsCount += _coinsFromOneChest * _chestsToOpen;
      ChestOpened?.Invoke();
      Debug.Log("Сундук открыт");
      //ShowUiElements(ChestsCount > 0);
      //_clickCatcher.SetActive(false);
      //_openChestCoroutine = null;
    }

    private void ShowUiElements(bool isVisible)
    {
      _uiElements.gameObject.SetActive(isVisible);
    }
  }
}