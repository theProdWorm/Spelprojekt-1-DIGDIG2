using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public Spell[ ] spells;

    public Spell GetSpell (List<Element> selectedElements) {
        Element[ ] combo = selectedElements.OrderByDescending(e => e).Reverse( ).ToArray( );

        Spell _spell = spells.First(s => Enumerable.SequenceEqual(s.combo, combo));
        
        return _spell;
    }
}