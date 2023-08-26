using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private VRPlayerController _player;
    private PlayerStats _playerStats;
    private PlayerTotalStats _playerTotalStats;

    private StorePrices _storePrices;
    private PetPickup _pet;

    [HideInInspector] public GameObject dropParent;

    public enum ItemType {health, gold, arcaneEnergy, key, potion, soul }
    public ItemType itemType;

    [SerializeField] private ItemStatDisplay _statDisplay;

    [SerializeField] private bool _shopItem, _petHolding, _petCanPickup;

    private int _itemPrice, _valueOfItem;

    private void Awake()
    {
        _gameManager = LocalGameManager.Instance;
        _player = _gameManager.player;
        _playerStats = _gameManager.GetPlayerStats();
        _playerTotalStats = _gameManager.GetTotalStats();
    }

    public void Start()
    {
        if (!_petHolding)
        {
            switch (itemType)
            {
                case ItemType.health:
                    _valueOfItem = HealthValue();
                    _statDisplay.UpdateStateDisplay("+ " + _valueOfItem + " Health");
                    break;

                case ItemType.gold:
                    _valueOfItem = GoldValue();
                    _statDisplay.UpdateStateDisplay("+ " + _valueOfItem + " Gold");
                    break;

                case ItemType.arcaneEnergy:
                    if (_playerStats.GetLuck() > 0) { _valueOfItem = ArcaneValue(); }
                    else _valueOfItem = 1;
                    _statDisplay.UpdateStateDisplay("+ " + _valueOfItem + " Bomb");
                    break;

                case ItemType.key:
                    if (_playerStats.GetLuck() > 0) { _valueOfItem = KeyValue(); }
                    else _valueOfItem = 1;
                    _statDisplay.UpdateStateDisplay("+ " + _valueOfItem + " Key");
                    break;

                case ItemType.potion:
                    break;

                case ItemType.soul:
                    break;
            }

            if (_valueOfItem == 0)
            {
                Debug.Log("Bad Luck");
                _statDisplay.UpdateStateDisplay("Bad Luck");
            }
            else if (_valueOfItem < 0)
            {
                Debug.Log("Cursed Luck");
                // if you die with cursed luck (-1 luck next run)
                _statDisplay.UpdateStateDisplay("Cursed Luck");
            }
        }
    }

    public void SetStoreSettings(StorePrices storePrices, int itemPrice)
    {
        _storePrices = storePrices;
        _itemPrice = itemPrice;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (!_petHolding)
        {
            if (!_shopItem)
            {
                VRPlayerController player;
                VRPlayerHand hand;
                if (other.gameObject.TryGetComponent<VRPlayerController>(out player) || other.gameObject.TryGetComponent<VRPlayerHand>(out hand))
                {
                    ChangeStat();

                    if (dropParent != null) { Destroy(dropParent); }
                    else Destroy(gameObject);
                }
            }
            
            else
            {
                WalletItem wallet;
                if (other.gameObject.TryGetComponent<WalletItem>(out wallet) && _playerStats.GetCurrentGold() >= _itemPrice)
                {
                    ChangeStat();

                    _playerStats.AdjustGoldAmount(-_itemPrice);

                    if (_storePrices.CheckStoreRefill()) { _storePrices.StoreRefill(); }

                    switch (_gameManager.currentGameMode)
                    {
                        case LocalGameManager.GameMode.normal | LocalGameManager.GameMode.master:
                            _playerTotalStats.AdjustStats(PlayerTotalStats.StatType.itemsBought);
                            break;
                    }
                }
            }
        }
        else
        {
            VRPlayerHand hand;
            if (other.gameObject.TryGetComponent<VRPlayerHand>(out hand))
            {
                ChangeStat();
                _pet.isHoldingItem = false;
                Destroy(gameObject);
            }
        }
    }

    public bool PetCanPickup() { return _petCanPickup; }

    public void PetPickingUp(PetPickup petPickingUp)
    {
        _pet = petPickingUp;

        switch (itemType)
        {
            case ItemType.health:
                PetHoldingItemSettings(3);
                break;

            case ItemType.gold:
                PetHoldingItemSettings(0);
                break;

            case ItemType.arcaneEnergy:
                PetHoldingItemSettings(2);
                break;

            case ItemType.key:
                PetHoldingItemSettings(1);
                break;
        }

        if (dropParent != null) { Destroy(dropParent); }
    }

    private void PetHoldingItemSettings(int whichItem)
    {
        GameObject newItem = Instantiate(_pet.petHoldingItems[whichItem], _pet.spawnLocation);

        _pet.isHoldingItem = true;
        _pet.ResetTransform(newItem);

        newItem.GetComponent<Item>()._pet = _pet;
        Item itemPetHolding = newItem.GetComponent<Item>();
        itemPetHolding._pet = _pet;
        itemPetHolding._valueOfItem = _valueOfItem;
    }

    public void ChangeStat()
    {
        switch (itemType)
        {
            case ItemType.health:
                _playerStats.AdjustHealth(_valueOfItem, "Healing Item");
                break;

            case ItemType.gold:
                _playerStats.AdjustGoldAmount(_valueOfItem);

                switch (_gameManager.currentGameMode)
                {
                    case LocalGameManager.GameMode.normal | LocalGameManager.GameMode.master:
                        _playerTotalStats.AdjustStats(PlayerTotalStats.StatType.totalGold, _valueOfItem);
                        break;
                }

                break;

            case ItemType.arcaneEnergy:
                _playerStats.AdjustArcaneCrystalAmount(_valueOfItem);
                break;

            case ItemType.key:
                _playerStats.AdjustKeyAmount(_valueOfItem);
                break;

            case ItemType.potion:
                _storePrices.BoughtPotion();
                break;

            case ItemType.soul:
                _playerStats.AdjustSoulAmount(1);
                _playerTotalStats.AdjustStats(PlayerTotalStats.StatType.totalSouls);
                break;
        }
    }

    public int HealthValue()
    {
        int healthValue = LocalGameManager.Instance.currentGameMode == LocalGameManager.GameMode.master ?
            Random.Range(25, 35) : Random.Range(20, 25);

        return Mathf.RoundToInt(healthValue + (_playerStats.GetLuck() * 0.5f));
    }

    public int GoldValue()
    {
        int goldValue = (Random.Range(3, 10) + Mathf.RoundToInt(_playerStats.GetLuck() * 3));
        return goldValue;
    }

    public int ArcaneValue()
    {
        return Random.Range(1, (Mathf.RoundToInt(_playerStats.GetLuck())));
    }

    public int KeyValue()
    {
        return Random.Range(1, (Mathf.RoundToInt(_playerStats.GetLuck())));
    }
}

