using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public Spell[ ] spells;
    public Dictionary<Element[ ], Spell> spellCollection = new Dictionary<Element[], Spell>( );

    private void Start ( ) {
        for (int i = 0; i < spells.Length; i++) {
            Element[ ] combo = spells[i].combo.OrderByDescending(e => e).Reverse( ).ToArray( );

            string elArray = "[";
            for (int j = 0; j < combo.Length; j++) {
                elArray += combo[j];

                if (j < combo.Length - 1)
                    elArray += ", ";
            }
            elArray += "]";
            print(elArray);

            spellCollection[combo] = spells[i];
        }
    }

    public Spell GetSpell (List<Element> selectedElements) {
        Element[ ] combo = selectedElements.OrderByDescending(e => e).Reverse( ).ToArray( );

        string n_combo = "spellCollection.ContainsKey(Element[";
        for (int i = 0; i < combo.Length; i++) {
            n_combo += (int) combo[i];

            if (i < combo.Length - 1)
                n_combo += ", ";
        }
        print(n_combo + "] = " + spellCollection.ContainsKey(combo) + ")");


        if (!spellCollection.ContainsKey(combo)) {
            return null;
        }

        return spellCollection[combo];
    }
}