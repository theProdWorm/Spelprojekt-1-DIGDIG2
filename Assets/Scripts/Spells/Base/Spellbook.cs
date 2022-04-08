using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public GameObject[ ] spells;

    public GameObject GetSpell (List<Element> selectedElements) {
        if (selectedElements.Count == 0) return null;

        for (int i = 0; i < spells.Length; i++) {
            var spell = spells[i].GetComponent<Spell>( );

            bool match = selectedElements.Count == spell.combo.Length;

            for (int j = 0; j < spell.combo.Length; j++) {
                bool elementSelected = selectedElements.Contains(spell.combo[j]);

                if (!elementSelected) {
                    match = false;
                    break;
                }
            }

            if (match) {
                return spells[i];
            }
        }
        
        return null;
    }
}