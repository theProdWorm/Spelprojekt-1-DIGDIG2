using UnityEngine;
using Aarthificial.Reanimation;

[CreateAssetMenu(fileName = "New spell", menuName = "Spell")]
public class Spell : ScriptableObject {
    public float damage;

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
}