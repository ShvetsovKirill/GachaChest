using TMPro;
using UnityEngine;

namespace Gacha_Test
{
  public class Counter : MonoBehaviour
  {
    [SerializeField] private ChestOpener _chestOpener;
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private ResType _resType;
    
    private void OnEnable()
      => _chestOpener.ChestOpened += UpdateCounterText;

    private void Start()
      => UpdateCounterText();

    private void OnDisable()
      => _chestOpener.ChestOpened += UpdateCounterText;

    private void UpdateCounterText()
    {
      _counterText.text = _resType switch
      {
        ResType.Chest => $"{_chestOpener.ChestsCount}",
        ResType.Coins => $"{_chestOpener.CoinsCount}",
        _ => _counterText.text
      };
    }
  }
}