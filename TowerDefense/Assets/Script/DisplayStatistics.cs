using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayStatistics : MonoBehaviour
{
    private GameObject PlayerStatistics;

    [SerializeField]
    private GameObject enemyADefeatedGO;

    [SerializeField]
    private GameObject enemyBDefeatedGO;

    [SerializeField]
    private GameObject TowerADefeatedGO;

    [SerializeField]
    private GameObject TowerBDefeatedGO;

    void OnEnable()
    {
        PlayerStatistics = GameObject.FindGameObjectWithTag("PlayerStats");
        enemyADefeatedGO.GetComponent<TextMeshProUGUI>().text = PlayerStatistics.GetComponent<RecordAchievmentValues>().numberOfEnemyADefeated.ToString();
        enemyBDefeatedGO.GetComponent<TextMeshProUGUI>().text = PlayerStatistics.GetComponent<RecordAchievmentValues>().numberOfEnemyBDefeated.ToString();
        TowerADefeatedGO.GetComponent<TextMeshProUGUI>().text = PlayerStatistics.GetComponent<RecordAchievmentValues>().numberOfTowerAPlaced.ToString();
        TowerBDefeatedGO.GetComponent<TextMeshProUGUI>().text = PlayerStatistics.GetComponent<RecordAchievmentValues>().numberOfTowerBPlaced.ToString();
    }
}
