
using UnityEngine;

public class ChooseJobUI : MonoBehaviour
{
    GameObject playerRecruit;


    void Update()
    {
        playerRecruit = GameObject.FindGameObjectWithTag("Player");
    }

    public void ChooseKnight(){
        playerRecruit.GetComponent<PlayerRecruit>().getSelectedNPC().GetComponent<NPC>().ChangeStatus(Status.Knight);
        playerRecruit.GetComponent<Coin>().decreaseCoin(1);
        gameObject.SetActive(false);
    }
    public void ChooseVillager(){
        playerRecruit.GetComponent<PlayerRecruit>().getSelectedNPC().GetComponent<NPC>().ChangeStatus(Status.Villager);
        playerRecruit.GetComponent<Coin>().decreaseCoin(1);
        gameObject.SetActive(false);
    }
    public void ChooseArcher(){
        playerRecruit.GetComponent<PlayerRecruit>().getSelectedNPC().GetComponent<NPC>().ChangeStatus(Status.Archer);
        playerRecruit.GetComponent<Coin>().decreaseCoin(1);
        gameObject.SetActive(false);
    }
}
