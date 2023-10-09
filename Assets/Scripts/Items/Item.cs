using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    StorePrices _storePrices;
    PetPickup _pet;

    public GameObject dropParent { get; set; }

    public enum ItemType 
    {
        health, 
        gold, 
        arcaneEnergy, 
        key, 
        potion, 
        soul 
    }

    public ItemType itemType;

    [SerializeField] 
    ItemStatDisplay _statDisplay;

    [SerializeField] 
    bool 
        _shopItem, 
        _petHolding, 
        _petCanPickup;

    int 
        _itemPrice, 
        _valueOfItem;

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
                    if (PlayerStats.Instance.data.luck > 0)
                        _valueOfItem = ArcaneValue();

                    else
                        _valueOfItem = 1;

                    _statDisplay.UpdateStateDisplay("+ " + _valueOfItem + " Bomb");
                    break;

                case ItemType.key:
                    if (PlayerStats.Instance.data.luck > 0)
                        _valueOfItem = KeyValue();

                    else
                        _valueOfItem = 1;

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

    void OnTriggerEnter (Collider other)
    {
        if (!_petHolding)
        {
            if (!_shopItem)
            {
                VRPlayer player;
                VRHand hand;

                if (other.gameObject.TryGetComponent<VRPlayer>(out player) || other.gameObject.TryGetComponent<VRHand>(out hand))
                {
                    ChangeStat();

                    if (dropParent != null)
                        Destroy(dropParent);

                    else
                        Destroy(gameObject);
                }
            }
            
            else
            {
                PlayerItemGrabbable grabbableItem;
                if (other.gameObject.TryGetComponent<PlayerItemGrabbable>(out grabbableItem))
                {
                    if (grabbableItem.grabbableItem == ItemPoolManager.GrabbableItem.wallet && PlayerStats.Instance.data.currentGold >= _itemPrice)
                    {
                        ChangeStat();

                        PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.gold, -_itemPrice);

                        if (_storePrices.CheckStoreRefill())
                            _storePrices.StoreRefill();

                        switch (LocalGameManager.Instance.currentGameMode)
                        {
                            case LocalGameManager.GameMode.normal | LocalGameManager.GameMode.master:
                                PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.itemsBought);
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            VRHand hand;

            if (other.gameObject.TryGetComponent<VRHand>(out hand))
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

    void PetHoldingItemSettings(int whichItem)
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
                PlayerStats.Instance.Damage(_valueOfItem);
                break;

            case ItemType.gold:
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.gold, _valueOfItem);

                switch (LocalGameManager.Instance.currentGameMode)
                {
                    case LocalGameManager.GameMode.normal | LocalGameManager.GameMode.master:
                        PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.totalGold, _valueOfItem);
                        break;
                }

                break;

            case ItemType.arcaneEnergy:
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.arcaneCrystals, _valueOfItem);
                break;

            case ItemType.key:
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.keys, _valueOfItem);
                break;

            case ItemType.potion:
                _storePrices.BoughtPotion();
                break;

            case ItemType.soul:
                PlayerStats.Instance.data.currentSouls++;
                PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.totalSouls);
                break;
        }
    }

    public int HealthValue()
    {
        int healthValue = LocalGameManager.Instance.currentGameMode == LocalGameManager.GameMode.master ?
            Random.Range(25, 35) : Random.Range(20, 25);

        return Mathf.RoundToInt(healthValue + (PlayerStats.Instance.data.luck * 0.5f));
    }

    public int GoldValue()
    {
        int goldValue = (Random.Range(3, 10) + Mathf.RoundToInt(PlayerStats.Instance.data.luck * 3));
        return goldValue;
    }

    public int ArcaneValue()
    {
        return Random.Range(1, (Mathf.RoundToInt(PlayerStats.Instance.data.luck)));
    }

    public int KeyValue()
    {
        return Random.Range(1, (Mathf.RoundToInt(PlayerStats.Instance.data.luck)));
    }
}

