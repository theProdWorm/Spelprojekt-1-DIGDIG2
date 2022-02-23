using System.Collections.Generic;
using Aarthificial.Reanimation;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    #region INITIAL STATS
    public float i_hp;
    public float i_spd;
    public float i_dmg;

    public float i_waterRes;
    public float i_earthRes;
    public float i_fireRes;
    public float i_airRes;
    public float i_physRes;
    #endregion

    #region CURRENT STATS
    protected float c_hp;
    protected float c_speed;

    protected float c_waterRes;
    protected float c_earthRes;
    protected float c_fireRes;
    protected float c_airRes;
    protected float c_physRes;

    protected float slowDur;
    protected float stunDur;
    #endregion

    protected Reanimator reanimator;

    protected readonly Dictionary<Element, bool> affectedBy = new Dictionary<Element, bool>( );
    protected readonly List<Effect> effects = new List<Effect>( );

    protected virtual void Start ( ) {
        c_hp = i_hp;
        c_speed = i_spd;

        c_waterRes = i_waterRes;
        c_earthRes = i_earthRes;
        c_fireRes = i_fireRes;
        c_airRes = i_airRes;
        c_physRes = i_physRes;


        reanimator = GetComponent<Reanimator>( );
    }

    protected virtual void Update ( ) {
        if (slowDur > 0) {
            slowDur -= Time.deltaTime;

            if (slowDur <= 0) {
                c_speed = i_spd;
            }
        }

        if (stunDur > 0) {
            stunDur -= Time.deltaTime;

            if (stunDur > 0) return;
        }
    }

    private void TakeHit (Spell spell) {


        c_hp -= spell.damage;

        affectedBy[spell.element] = true;

        c_speed *= spell.slowAmount / 100;
        slowDur = spell.slowDuration;
        stunDur = spell.stunDuration;

        if (c_hp <= 0) {
            Die( );
        }
    }

    /// <summary>
    /// Returns the appropriate elemental resistance according to the input element.
    /// </summary>
    /// <param name="element"></param>
    private float GetER (Element element) => element switch {
        Element.water => c_waterRes,
        Element.earth => c_earthRes,
        Element.fire => c_fireRes,
        Element.air => c_airRes,
        Element.physical => c_physRes,
        _ => c_physRes
    };

    protected void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("Spell")) {
            var spell = collision.GetComponent<Spell>( );

            TakeHit(spell);
        }
    }

    protected void Die ( ) {
        reanimator.Set("root", 1);
    }
}