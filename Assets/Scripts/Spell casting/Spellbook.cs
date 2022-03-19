using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public Spell[ ] spells;

    public Spell GetSpell (List<Element> selectedElements) {
        if (selectedElements.Count == 0) return null;

        for (int i = 0; i < spells.Length; i++) {
            bool match = selectedElements.Count == spells[i].combo.Length;

            for (int j = 0; j < spells[i].combo.Length; j++) {
                bool elementSelected = selectedElements.Contains(spells[i].combo[j]);

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