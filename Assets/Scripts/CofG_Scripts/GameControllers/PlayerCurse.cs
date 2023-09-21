using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.AbstractClasses;

public class PlayerCurse : MonoSingleton<PlayerCurse>
{
    private OnScreenText _onScreenText;

    public bool hasCurseImmunity { get; private set; }

    public enum Curses 
    { 
        NotCursed, 
        CurseOfUnknown, 
        CurseOfTheLost, 
        CurseOfDarkness, 
        CurseOfTheDungeon, 
        CurseOfDispair 
    }
    public Curses currentCurseEffect { get; private set; }

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        _onScreenText = player.GetPlayerComponents().onScreenText;
    }

    public void ToggleCurseImmunity(bool immunity)
    {
        hasCurseImmunity = immunity;
    }

    public void RunCurseCheck()
    {
        if (!hasCurseImmunity)
        {
            int applyCurse = Random.Range(1, 6 + (int)PlayerStats.Instance.data.luck);

            if (applyCurse == 1)
            {
                int whichCurse = Random.Range(1, 6);

                switch (whichCurse)
                {
                    // Curse of Unknown will block item scrolls from being visible and will block compass icons on map
                    case 1:
                        ChangeCurrentCurse(Curses.CurseOfUnknown, "Curse of the Unknown");
                        break;

                    // Curse of the Lost will make the map unviewable 
                    case 2:
                        ChangeCurrentCurse(Curses.CurseOfTheLost, "Curse of the Lost");
                        break;

                    // Curse of darkness will create a dense shadowy fog around the player
                    case 3:
                        ChangeCurrentCurse(Curses.CurseOfDarkness, "Curse of Darkness");
                        break;

                    // Curse of the dungeon will double the size of the current dungeon build
                    case 4:
                        ChangeCurrentCurse(Curses.CurseOfTheDungeon, "Curse of the Labyrinth");
                        break;

                    // Curse of dispair will give all enemy more health
                    case 5:
                        ChangeCurrentCurse(Curses.CurseOfDispair, "Curse of Despair");
                        break;
                }
            }
        }
    }

    private void ChangeCurrentCurse(Curses curse, string description)
    {
        currentCurseEffect = curse;
        _onScreenText.PrintText(description, true);
    }

    public string CheckCurrentCurseStatus()
    {
        switch (currentCurseEffect)
        {
            case Curses.CurseOfUnknown:
                return "Curse of Lost Knowledge";

            case Curses.CurseOfTheLost:
                return "Curse of the Wanderer";

            case Curses.CurseOfDarkness:
                return "Curse of Faded Sight";

            case Curses.CurseOfTheDungeon:
                return "Curse of the Labyrinth";

            case Curses.CurseOfDispair:
                return "Curse of Dispair";
        }
        return "The Gods Smile Upon You";
    }

    public void RemoveCurse()
    {
        currentCurseEffect = Curses.NotCursed;
    }
}
