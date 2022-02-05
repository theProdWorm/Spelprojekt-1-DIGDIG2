using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public float speed;
    public float damage;
    public float maxHealth;
    [Tooltip("Maximum amount of time (in seconds) that this entity can stay affected by an element without triggering an effect.")]
    public float elementalResistance;

    private float currentHealth;

    private readonly Dictionary<Element, bool> affectedBy = new Dictionary<Element, bool>( );
    private readonly List<Effect> effects = new List<Effect>( );

    private void Start ( ) {
        currentHealth = maxHealth;
    }

    private void Update ( ) {

    }

    public void Inflict (Element element) => affectedBy[element] = true;
}