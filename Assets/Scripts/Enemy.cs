using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    #region STATS
    public float speed;
    public float damage;
    public float defense;
    public float maxHealth;

    public float waterResistance;
    public float earthResistance;
    public float fireResistance;
    public float airResistance;
    #endregion

    private float currentHealth;

    private readonly Dictionary<Element, float> inflictedElements = new Dictionary<Element, float>( );
    private readonly List<Effect> inflictedEffects = new List<Effect>( );

    private void Start ( ) {
        currentHealth = maxHealth;
    }

    private void Update ( ) {

    }

    public void Inflict (Element element, float magnitude) {
        inflictedElements[element] += magnitude;

        float resistance = element switch {
            Element.water => waterResistance,
            Element.earth => earthResistance,
            Element.fire => fireResistance,
            Element.air => airResistance,
            _ => 0
        };

        if (inflictedElements[element] > resistance) {
            inflictedEffects.Add(element switch {
                _ => Effect.burning
            });
        }
    }
}