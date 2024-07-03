using UnityEngine;

public class ChestScript : MonoBehaviour, IInteractable
{
    [SerializeField] int coinValue;
    [SerializeField] int lifeEssenceValue;
    [SerializeField] GameObject FIcon;
    [ContextMenu("chest")]
    private void Update()
    {
        Interact();
    }
    public void Interact()
    {
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 2f), 3f, LayerMask.GetMask("Player"));
        if (player != null)
        {
            FIcon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Coin>().increaseCoin(coinValue);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Keris>().addLifeEssence(lifeEssenceValue);
                GetComponent<Animator>().Play("OpenChest");
                Invoke("DestroyThis", 1.5f);
            }


        }
        else
        {
            FIcon.SetActive(false);
        }

        //invoke destroy
    }
    void DestroyThis()
    {
        Destroy(gameObject);
    }

}
