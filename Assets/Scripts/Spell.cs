using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 0)]
public class Spell : ScriptableObject {
    [Tooltip("The amount of initial damage the spell does.")]
    public float damage;

    [Tooltip("How much the spell will slow affected enemies (%)")]
    public float slowAmount;

    [Tooltip("How long affected enemies will remain slowed (seconds)")]
    public float slowDuration;

    [Tooltip("How long affected enemies will remain stunned (seconds)")]
    public float stunDuration;

    public Element element;

    [Tooltip("How much of the element is applied to affected enemies.")]
    public float elementMagnitude;
}