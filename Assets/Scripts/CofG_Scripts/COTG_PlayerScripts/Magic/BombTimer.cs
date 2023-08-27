using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    [SerializeField]
    private GameObject _dentonatingEffect, _explosionEffect;

    public async void StartBombTimer()
    {
        _dentonatingEffect.SetActive(true);

        await Task.Delay(3000);

        DetonateBomb();
    }

    private void DetonateBomb()
    {
        GameObject newExplosion = Instantiate(_explosionEffect, transform.position, transform.rotation);
        newExplosion.transform.SetParent(null);
        newExplosion.transform.localScale = new Vector3(1, 1, 1);
        newExplosion.GetComponentInChildren<EnemyHealthModifier>().player = LocalGameManager.Instance.player;

        Destroy(gameObject);
    }
}
