using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    public KeyCode[ ] leftKeys;
    public KeyCode[ ] rightKeys;
    public KeyCode[ ] downKeys;
    public KeyCode[ ] upKeys;
    private KeyCode[ ][ ] directionKeys;
    private readonly List<Direction> inputDirs = new List<Direction>( );

    public KeyCode[ ] castKeys;
    public KeyCode[ ] clearKeys;

    [HideInInspector]
    public Direction facing;
    [HideInInspector]
    public Direction deltaFacing;

    public GameObject[ ] elementIcons;

    private readonly List<Element> selectedElements = new List<Element>(4);
    private bool usedSelected;

    private Spellbook spellbook;

    protected override void Start ( ) {
        base.Start( );

        directionKeys = new KeyCode[ ][ ] { leftKeys, rightKeys, downKeys, upKeys };

        spellbook = FindObjectOfType<Spellbook>( );
    }

    protected override void Update ( ) {
        base.Update( );

        Move( );

        SelectElement( );

        if (KeyDown(castKeys))
            CastSpell( );

        if (KeyDown(clearKeys))
            selectedElements.Clear( );

        for (int i = 0; i < 4; i++) {
            elementIcons[i].SetActive(selectedElements.Contains((Element) i));
        }
    }

    private void Move ( ) {
        GetMoveInput( );

        facing = inputDirs.Count != 0 ? inputDirs[^1] : Direction.none;

        Vector3 _move = LMTools.GetVector(facing) * c_speed * Time.deltaTime;

        transform.position += _move;

        if (facing != Direction.none) {
            animator.SetInteger("facing", (int) facing);
            deltaFacing = facing;
        }
    }

    private void CastSpell ( ) {
        GameObject spellInstance = spellbook.GetSpell(selectedElements);
        
        if (spellInstance is null) {
            string n_combo = "";
            for (int i = 0; i < selectedElements.Count; i++) {
                n_combo += selectedElements[i];

                if (i < selectedElements.Count - 1)
                    n_combo += " + ";
            }

            if (n_combo != "") print("combo does not exist: " + n_combo);

            return;
        }

        var spell = spellInstance.GetComponent<Spell>( );

        if (c_mana < spell.manaCost) {

            // indicate to the player that they don't have enough mana to cast the spell
            print("Not enough mana!");
            return;
        }

        Instantiate(spellInstance, transform.position, Quaternion.identity);

        c_mana -= spell.manaCost;

        usedSelected = true;
    }

    protected override void Die ( ) {
        print("PLAYER DEDED");
    }

    #region Handle input
    /// <summary>
    /// Checks for inputs 1-4 and selects the corresponding element, adding it to the selectedElements list.
    /// </summary>
    private void SelectElement ( ) {
        for (int i = 0; i < 4; i++) {
            if (Input.GetKeyDown((KeyCode) (i + 49))) {
                if (usedSelected) {
                    selectedElements.Clear( );
                    usedSelected = false;
                }
                else if (selectedElements.Contains((Element) i))
                    break;

                selectedElements.Add((Element) i);
            }
        }
    }

    /// <summary>
    /// Adds or removes Direction elements from the inputDirs list according to input, using all assigned inputs.
    /// </summary>
    private void GetMoveInput ( ) {
        for (int i = 0; i < directionKeys.Length; i++) {
            if (KeyDown(directionKeys[i])) {
                inputDirs.Add((Direction) i);
            }
            else if (KeyUp(directionKeys[i]) && inputDirs.Contains((Direction) i)) {
                inputDirs.RemoveAll(x => x == (Direction) i);
            }
        }
    }

    private bool KeyDown (KeyCode[ ] keyArray) {
        for (int i = 0; i < keyArray.Length; i++) {
            if (Input.GetKeyDown(keyArray[i]))
                return true;
        }
        return false;
    }

    private bool KeyUp (KeyCode[ ] keyArray) {
        for (int i = 0; i < keyArray.Length; i++) {
            if (Input.GetKeyUp(keyArray[i]))
                return true;
        }
        return false;
    }
    #endregion
}