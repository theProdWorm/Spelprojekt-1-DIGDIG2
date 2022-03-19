using UnityEngine;

[CreateAssetMenu(fileName = "New spell", menuName = "Spell")]
public class Spell : ScriptableObject {
    [Tooltip("Some spells hit multiple times. This describes damage done per hit, not in total.")]
    public float damagePerHit;

    [Tooltip("Area of effect in world coordinates.")]
    public float aoe;

    [Tooltip("Amount slowed in percentage. Set to zero if the spell does not apply slowness.")]
    public float slowAmount;

    [Tooltip("Duration of the slow effect in seconds. Set to zero if the spell does not apply slowness.")]
    public float slowDuration;

    [Tooltip(@"True: Keep current slowness effects but apply this as well. The duration of stacked slow effects is calculated individually.

False: Override existing slowness effects; only this slowness will be prominent.")]
    public bool stackSlow;

    [Tooltip("Duration of the stun effect. Set to zero if the spell does not stun.")]
    public float stunDuration;

    [Tooltip("Elemental damage type.")]
    public Element dominantElement;

    [Tooltip("Elements used to cast the spell.")]
    public Element[ ] combo;

    [Tooltip("Always includes a copy of this ScriptableObject, a script describing the spell's active behaviour, and a Collider2D.")]
    public GameObject activated;
}