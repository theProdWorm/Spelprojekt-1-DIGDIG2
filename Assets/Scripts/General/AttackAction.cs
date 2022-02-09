using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 0)]
public class EntityAction : ScriptableObject {
    public float damage;

    [Tooltip("Slow amount (%)")]
    public float slowAmount;

    [Tooltip("Slow duration (seconds)")]
    public float slowDuration;

    [Tooltip("Stun duration (seconds)")]
    public float stunDuration;

    public Element element;
}