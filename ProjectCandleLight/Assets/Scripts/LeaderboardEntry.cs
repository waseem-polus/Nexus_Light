using UnityEngine;
using TMPro;

[System.Serializable]
public class LeaderboardEntry
{
    public string entryName;

    [Space(10)]
    public TMP_Text recordRank;
    public TMP_Text recordHolderName;
    public TMP_Text recordTime;
}
