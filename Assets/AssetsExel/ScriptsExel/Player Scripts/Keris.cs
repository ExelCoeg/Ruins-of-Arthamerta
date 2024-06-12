
using UnityEngine.UI;
using UnityEngine;

public class Keris : MonoBehaviour
{
    [Header("Life Essence")]
    public int lifeEssence = 0;
    [SerializeField] int maxLifeEssence = 100;

    [Header("Petir Attributes")]
    private bool petirAvailable = true;
    [SerializeField] int petirCost = 20;
    private float petirCooldown;
    public float petirChargeTime;
    float petirChargeTimer;
    [SerializeField] float petirCooldownMax = 5f;
    public float petirOffsetX = 2f;
    public float petirOffsetY = 2f;
    [Header("Expand Attributes")]
    [SerializeField] float holdTime = 0f;

    [SerializeField] float requiredHoldTime = 3f;
    [SerializeField] int expandCost = 100;

    [SerializeField] Vector3 mousePos;

    [SerializeField] GameObject petir;
    [SerializeField] Slider expandBar;
    bool expanding = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetPetirCooldown();
    }
   

    // Update is called once per frame
    void Update()
    {
        
        if(lifeEssence > maxLifeEssence){
            lifeEssence = maxLifeEssence;
        }

        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        petirCooldown -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && petirAvailable && lifeEssence >=  petirCost){
            // panggil fungsi petir
            GetComponent<Animator>().Play("player_strike");
            ResetPetirCooldown();
        }
        if(Input.GetKey(KeyCode.F) && (TerritoryManager.instance.onPointA || TerritoryManager.instance.onPointB)){
            holdTime += Time.deltaTime;
            expandBar.gameObject.SetActive(true);
            GetComponent<Animator>().Play("player_expand");
            expandBar.value = holdTime/requiredHoldTime;
            if(holdTime >= requiredHoldTime){
                Expand();
            }
        }
        else{
            holdTime = 0;
            expandBar.gameObject.SetActive(false);
        }

    }

    public void Expand(){
            // expand muncul bar untuk expand (WORLD SPACE UI)
        if(lifeEssence >= expandCost){
            TerritoryManager.instance.territoryPoints.pointA.gameObject.GetComponent<ExpandTerritory>().Expand();
            reduceLifeEssence(maxLifeEssence);
        }
    }
    
    public void Petir(Vector3 pos){ 
        GameObject petirClone = Instantiate(petir, pos, Quaternion.identity);
        reduceLifeEssence(petirCost);
    }
    public void addLifeEssence(int amount){
        lifeEssence += amount;
    }
    public void reduceLifeEssence(int amount){
        lifeEssence -= amount;
    }
    public void ResetPetirCooldown(){
        petirCooldown = petirCooldownMax;
    }
    public float getMousePosX(){
        return mousePos.x;
    }
    public Slider getExpandBar(){
        return expandBar;
    }
}

