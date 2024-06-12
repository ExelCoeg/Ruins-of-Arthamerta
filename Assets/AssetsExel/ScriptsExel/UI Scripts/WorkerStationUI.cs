
using TMPro;
using UnityEngine;

public class WorkerStationUI : MonoBehaviour
{
    public TextMeshProUGUI archerAttackSpeedText;
    public TextMeshProUGUI archerDamageText;
    public TextMeshProUGUI knightAttackSpeedText;
    public TextMeshProUGUI knightDamageText;
    public TextMeshProUGUI villagerMiningTimeToCoinText;
    GameObject playerRecruit;
    public bool closeInput;
    private void Update() {
        archerAttackSpeedText.text = "ATKSPD " + NPCManager.instance.archerAttackSpeed.ToString();
        archerDamageText.text = "Damage " + NPCManager.instance.archerDamage.ToString();
        knightAttackSpeedText.text = "ATKSPD " + NPCManager.instance.knightAttackSpeed.ToString();
        knightDamageText.text = "Damage " + NPCManager.instance.knightDamage.ToString();
        villagerMiningTimeToCoinText.text = "Mine Time " +  $"{NPCManager.instance.villagerMiningTimeToCoin:F2}";
        playerRecruit = GameObject.FindGameObjectWithTag("Player");

    }
    public void ExitUI(){
        gameObject.SetActive(false);
    }
    public void UpgradeKnight(){
        if(LevelManager.instance.knightLevel >= LevelManager.instance.maxLevel) return;
        NPCManager.instance.knightAttackSpeed += 1.5f;
        NPCManager.instance.knightDamage += 1.5f;
        playerRecruit.GetComponent<Coin>().decreaseCoin(2);
        LevelManager.instance.knightLevel++;
    }
    public void UpgradeVillager(){
        if(LevelManager.instance.villagerLevel >= LevelManager.instance.maxLevel) return;
        NPCManager.instance.villagerMiningTimeToCoin -= 0.2f;
        playerRecruit.GetComponent<Coin>().decreaseCoin(2);
       
        LevelManager.instance.villagerLevel++;
    }
    public void UpgradeArcher(){
        if(LevelManager.instance.archerLevel >= LevelManager.instance.maxLevel) return;
        NPCManager.instance.archerAttackSpeed += 1.5f;
        NPCManager.instance.archerDamage += 1.5f;
        playerRecruit.GetComponent<Coin>().decreaseCoin(2);
        LevelManager.instance.archerLevel++;

    }
}
