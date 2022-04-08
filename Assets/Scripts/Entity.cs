using System.Collections.Generic;
using UnityEngine;

public class SlowEffect {
    public SlowEffect (float amount, float duration) {
        this.amount = amount;
        this.duration = duration;
    }

    public float amount;
    public float duration;
}

public abstract class Entity : MonoBehaviour {
    #region INITIAL STATS
    public float i_hp;
    public float i_mana;
    public float i_manaReg;
    public float i_haste;
    public float i_speed;

    public float i_waterRes;
    public float i_earthRes;
    public float i_fireRes;
    public float i_airRes;
    public float i_physRes;
    #endregion

    #region CURRENT STATS
    [HideInInspector]
    public float c_hp;
    [HideInInspector]
    public float c_mana;
    protected float c_manaReg;
    protected float c_haste;
    protected float c_speed;

    protected float c_waterRes;
    protected float c_earthRes;
    protected float c_fireRes;
    protected float c_airRes;
    protected float c_physRes;
    #endregion

    protected List<SlowEffect> slowEffects;
    protected float stunDur;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected bool stunned;

    protected virtual void Start ( ) {
        #region initialize stats
        c_hp = i_hp;
        c_mana = i_mana;
        c_manaReg = i_manaReg;
        c_haste = i_haste;
        c_speed = i_speed;

        c_waterRes = i_waterRes;
        c_earthRes = i_earthRes;
        c_fireRes = i_fireRes;
        c_airRes = i_airRes;
        c_physRes = i_physRes;
        #endregion

        slowEffects = new List<SlowEffect>( );

        rb = GetComponent<Rigidbody2D>( );
        animator = GetComponent<Animator>( );
    }

    protected virtual void Update ( ) {
        if (c_mana < i_mana) {
            c_mana = Mathf.Clamp(c_mana + c_manaReg * Time.deltaTime, 0, i_mana);
        }

        // reduce slow duration, reset if zero
        if (slowEffects.Count > 0) {
            float newSpeed = i_speed;

            for (int i = 0; i < slowEffects.Count; i++) {
                slowEffects[i].duration -= Time.deltaTime;

                if (slowEffects[i].duration <= 0) {
                    slowEffects.RemoveAt(i--);
                    continue;
                }

                newSpeed -= i_speed * slowEffects[i].amount;
            }

            newSpeed = Mathf.Clamp(newSpeed, i_speed * 0.1f, i_speed);

            c_speed = newSpeed;
        }

        // reduce stun duration, force return if greater than zero
        if (stunDur > 0) {
            stunDur -= Time.deltaTime;

            if (stunDur > 0) {
                stunned = true;
                return;
            }
        }

        stunned = false;
    }

    protected void CastSpell (GameObject spell) {
        Instantiate(spell, transform.position, Quaternion.identity);
    }

    public void TakeHit (Spell spell) {
        float damage = spell.damagePerHit * (20.0f / (20.0f + (float) GetER(spell.dominantElement)));

        c_hp -= damage;
        if (c_hp <= 0) {
            Die( );
        }

        if (!spell.stackSlow) {
            slowEffects.Clear( );
        }

        slowEffects.Add(new SlowEffect(spell.slowAmount, spell.slowDuration));

        stunDur = spell.stunDuration;

        print($"{name} has {c_hp} hp left.");

        spell.hitCDs.Add(new HitCD(this, 1 / spell.hitFrequency));
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

    protected virtual void Die ( ) {
        animator.SetBool("dead", true);

        print($"{name} is dead!");
    }

    protected virtual void OnTriggerStay2D (Collider2D collision) {
        if (collision.CompareTag("Spell")) {
            Spell spell = collision.GetComponent<Spell>( );

            if (spell.CanHit(this))
                TakeHit(spell);

            print($"{name} was hit by {spell.name}!");
        }
    }
}