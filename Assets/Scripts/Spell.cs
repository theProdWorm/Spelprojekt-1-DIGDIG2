using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 0)]
public class Spell : ScriptableObject {
    public float damage;

    [Tooltip("How much the spell will slow affected enemies (%)")]
    public float slowAmount;

    [Tooltip("How long affected enemies will remain slowed (seconds)")]
    public float slowDuration;

    [Tooltip("How long affected enemies will remain stunned (seconds)")]
    public float stunDuration;

    public Element element;
}