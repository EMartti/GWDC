using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiPlayerlevel : MonoBehaviour
{

    public TextMeshProUGUI levelText;
    public string playerLevel;
    public int playerLevelInt;

    // Start is called before the first frame update
    void Start()
    {
        playerLevelInt = PlayerStats.Instance.playerLevel;
        playerLevel = playerLevelInt.ToString();
        levelText.text = playerLevel;
    }

    // Update is called once per frame
    public void levelUpUi(int playerNewLevel)
    {
        string playerNewLevelS = playerNewLevel.ToString();
        levelText.text = playerNewLevelS;
    }
}
