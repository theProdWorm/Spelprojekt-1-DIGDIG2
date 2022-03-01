using UnityEngine;
using Aarthificial.Reanimation;

[CreateAssetMenu(fileName = "New spell", menuName = "Spell")]
public class Spell : ScriptableObject {
    public float damage;

    [Tooltip("(%)")]
    public float slowAmount;

    [Tooltip("(seconds)")]
    public float slowDuration;

    public bool stackSlow;

    [Tooltip("(seconds)")]
    public float stunDuration;

    public Element element;
}