using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class PlayerCardContnroller : MonoSingleton<PlayerCardContnroller>
{
    [SerializeField] 
    private GameObject[] _playerCards;

    private GameObject _currentPlayerCard;

    // Sorcerer Only
    public delegate void SorcererSelected(Sorcerer sorcererController);
    public static event SorcererSelected newSorcerer;

    public void ChangePlayerCard(MagicController.ClassType newClass)
    {
        Destroy(_currentPlayerCard);

        switch (newClass)
        {
            case MagicController.ClassType.Wizard:
                _currentPlayerCard = Instantiate(_playerCards[0]);
                break;

            case MagicController.ClassType.Conjurer:
                _currentPlayerCard = Instantiate(_playerCards[1]);
                break;

            case MagicController.ClassType.Sorcerer:
                _currentPlayerCard = Instantiate(_playerCards[2]);
                newSorcerer(_currentPlayerCard.GetComponent<Sorcerer>());
                break;

            case MagicController.ClassType.Mage:
                _currentPlayerCard = Instantiate(_playerCards[3]);
                break;

            case MagicController.ClassType.Enchanter:
                _currentPlayerCard = Instantiate(_playerCards[4]);
                break;

            case MagicController.ClassType.Warlock:
                _currentPlayerCard = Instantiate(_playerCards[5]);
                break;

            case MagicController.ClassType.Witch:
                _currentPlayerCard = Instantiate(_playerCards[6]);
                break;

            case MagicController.ClassType.Tarot:
                _currentPlayerCard = Instantiate(_playerCards[7]);
                break;
        }

        _currentPlayerCard.transform.SetParent(LocalGameManager.Instance.player.transform);
    }
}
