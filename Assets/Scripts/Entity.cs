using System.Collections.Generic;
using Aarthificial.Reanimation;
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
    public float i_speed;

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
    #endregion

    protected List<SlowEffect> slowEffects;
    protected float stunDur;

    protected Reanimator reanimator;

    protected const float affectDur = 3.0f;

    protected readonly Dictionary<Element, float> affectedBy = new Dictionary<Element, float>( );

    protected bool shouldReturn;

    protected virtual void Start ( ) {
        c_hp = i_hp;
        c_speed = i_speed;

        c_waterRes = i_waterRes;
        c_earthRes = i_earthRes;
        c_fireRes = i_fireRes;
        c_airRes = i_airRes;
        c_physRes = i_physRes;

        slowEffects = new List<SlowEffect>( );

        reanimator = GetComponent<Reanimator>( );

        for (int i = 0; i < 5; i++) {
            affectedBy.Add((Element) i, 0.0f);
        }
    }

    protected virtual void Update ( ) {
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
                shouldReturn = true;
                return;
            }
        }

        for (int i = 0; i < 5; i++) {
            print((Element) i);
            if (affectedBy[(Element) i] > 0)
                affectedBy[(Element) i] -= Mathf.Clamp(Time.deltaTime, 0, affectDur);
        }

        shouldReturn = false;
    }

    protected Vector2 GetDomAxis (Vector2 diagonal) {
        float angle = Mathf.Atan2(diagonal.y, diagonal.x);

        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        // return a new vector with only the dominant axis
        return Mathf.Abs(x) > Mathf.Abs(y) ? new Vector2(x, 0) : new Vector2(0, y);
    }

    public void TakeHit (Spell spell) {
        float damage = spell.damage * (20.0f / (20.0f + (float) GetER(spell.element)));

        c_hp -= damage;
        if (c_hp <= 0) {
            Die( );
        }

        affectedBy[spell.element] = affectDur;

        if (!spell.stackSlow) {
            slowEffects.Clear( );
        }

        slowEffects.Add(new SlowEffect(spell.slowAmount, spell.slowDuration));

        stunDur = spell.stunDuration;

        print($"{name} has {c_hp} hp and {c_speed} speed");
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

    protected virtual void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("Spell")) {
            var spell = collision.GetComponent<Spell>( );

            TakeHit(spell);
        }
    }

    protected virtual void Die ( ) {
        reanimator.Set("root", 1);

        print($"{name} is dead!");
    }
}