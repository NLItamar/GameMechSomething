using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalProgression : MonoBehaviour
{
    public Text journalText;
    public Text endText;

    int caseSwitch;

    private void Start()
    {
        caseSwitch = 0;
    }

    public void JournalAddText()
    {
        caseSwitch++;

        switch (caseSwitch)
        {
            case 1:
                journalText.text = "First entry added, three more to go";
                break;
            case 2:
                journalText.text = journalText.text + System.Environment.NewLine + "Second entry added, two more to go";
                break;
            case 3:
                journalText.text = journalText.text + System.Environment.NewLine + "Third entry added, one more to go";
                break;
            case 4:
                journalText.text = journalText.text + System.Environment.NewLine + "Fourth entry added, now I can escape!";
                EndingLevel();
                break;
            default:
                journalText.text = "Nothing in here now";
                break;
        }
    }

    public void EndingLevel()
    {
        //ends the game in a sense
        endText.gameObject.SetActive(true);        
    }
}
