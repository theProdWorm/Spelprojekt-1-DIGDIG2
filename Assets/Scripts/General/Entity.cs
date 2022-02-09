using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    #region INITIAL STATS
    public float initialHealth;
    public float initialSpeed;
    public float initialDamage;
    [Tooltip("Maximum amount of time (in seconds) that this entity can stay affected by an element without triggering an effect.")]
    public float elementalResistance;
    #endregion

    #region CURRENT STATS
    protected float maxHealth;
    protected float currentHealth;
    protected float currentSpeed;
    protected float slowDuration;
    protected float stunDuration;
    #endregion

    private readonly Dictionary<Element, bool> affectedBy = new Dictionary<Element, bool>( );
    private readonly List<Effect> effects = new List<Effect>( );

    private void Start ( ) {
        maxHealth = initialHealth;
        currentHealth = initialHealth;

        currentSpeed = initialSpeed;
    }

    private void Update ( ) {
        if (slowDuration > 0) {
            slowDuration -= Time.deltaTime;

            if (slowDuration <= 0) {
                currentSpeed = initialSpeed;
            }
        }

        if (stunDuration > 0) {
            stunDuration -= Time.deltaTime;

            if (stunDuration > 0) return;
        }
    }

    public void TakeHit (Spell spell) {
        currentHealth -= spell.damage;

        affectedBy[spell.element] = true;

        currentSpeed *= spell.slowAmount / 100;
        slowDuration = spell.slowDuration;
        stunDuration = spell.stunDuration;

        if (currentHealth <= 0) {
            Die( );
        }
    }

    protected void Die ( ) {

    }
}