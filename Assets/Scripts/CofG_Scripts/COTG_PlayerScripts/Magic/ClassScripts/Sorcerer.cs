using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : MonoBehaviour
{
    public static Sorcerer instance;

    [SerializeField] private SpellCasting _spellCasting;

    private void Start()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
    }

    public SpellCasting GetSpellCasting() { return _spellCasting; }

    private void OnDestroy()
    {
        if (instance == this) { instance = null; }
    }
}
