using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitCD {
    public Transform transform;
    public float cd;

    public HitCD (Transform transform, float cd) {
        this.transform = transform;
        this.cd = cd;
    }
}

public abstract class Spell : MonoBehaviour {
    [Tooltip("This describes damage done per hit, not in total.")]
    public float damagePerHit;

    [Tooltip("The maximum amount of times this spell can deal damage to a single enemy in one second.")]
    public float hitFrequency;

    [Tooltip("The amount of mana required to cast the spell.")]
    public float manaCost;

    [Tooltip("Amount slowed in percentage. Set to zero if the spell does not apply slowness.")]
    public float slowAmount;

    [Tooltip("Duration of the slow effect in seconds. Set to zero if the spell does not apply slowness.")]
    public float slowDuration;

    [Tooltip(@"True: Keep current slowness effects but apply this as well. The duration of stacked slow effects is calculated individually.

False: Override existing slowness effects; only this slowness will be prominent.")]
    public bool stackSlow;

    [Tooltip("Duration of the stun effect. Set to zero if the spell does not stun.")]
    public float stunDuration;

    [Tooltip("The amount of time in seconds that the spell remains on the field.")]
    public float lifespan;

    [Tooltip("Elemental damage type.")]
    public Element dominantElement;

    [Tooltip("Elements used to cast the spell.")]
    public Element[ ] combo;

    [HideInInspector]
    public List<HitCD> hitCDs = new List<HitCD>( );

    private float lifetime;

    protected virtual void Update ( ) {
        for (int i = 0; i < hitCDs.Count; i++) {
            if (hitCDs[i].cd > 0) {
                hitCDs[i].cd -= Time.deltaTime;

                if (hitCDs[i].cd <= 0)
                    hitCDs.RemoveAt(i);
            }
        }

        if (lifetime < lifespan) {
            lifetime += Time.deltaTime;

            if (lifetime >= lifespan)
                Destroy(gameObject);
        }
    }

    public bool CanHit (Transform transform) {
        var hitCD = hitCDs.FirstOrDefault(x => x.transform == transform);

        return hitCD is null;
    }
}