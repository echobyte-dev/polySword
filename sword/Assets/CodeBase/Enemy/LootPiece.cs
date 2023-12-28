using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class LootPiece : MonoBehaviour
  {
    [SerializeField] private GameObject _money;
    [SerializeField] private GameObject _pickupFxPrefab;
    [SerializeField] private GameObject _pickupPopup;
    [SerializeField] private TextMeshPro _lootText;

    private Loot _loot;
    private bool _picked;
    private WorldData _worldData;

    public void Construct(WorldData worldData)
    {
      _worldData = worldData;
    }

    public void Initialize(Loot loot)
    {
      _loot = loot;
    }

    private void OnTriggerEnter(Collider other) => Pickup();

    private void Pickup()
    {
      if (_picked)
        return;

      _picked = true;

      UpdateWorldData();
      HideMoney();
      PlayPickupFx();
      ShowText();

      StartCoroutine(StartDestroyTimer());
    }

    private void UpdateWorldData() =>
      _worldData.LootData.Collect(_loot);

    private void HideMoney() =>
      _money.SetActive(false);

    private void PlayPickupFx() =>
      Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity);

    private void ShowText()
    {
      _lootText.text = $"{_loot.Value}";
      _pickupPopup.SetActive(true);
    }

    private IEnumerator StartDestroyTimer()
    {
      yield return new WaitForSeconds(1.5f);
      Destroy(gameObject);
    }
  }
}