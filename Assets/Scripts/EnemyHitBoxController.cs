using Assets.Scripts.Utils;
using UnityEngine;

public class EnemyHitBoxController : MonoBehaviour
{
    //[SerializeField]
    private int maxHP = 3;

    private GameObject hpBarOverlay;
    public HPUtil HPUtil { private set; get; }
    // Start is called before the first frame update
    void Start()
    {
        hpBarOverlay = transform.Find(Constant.HP_BAR_CURRENT)?.gameObject;

        HPUtil = new HPUtil(hpBarOverlay, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (HPUtil.CurrentHP == 0)
        {
            Destroy(transform.parent.gameObject, 0.1f);
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HPUtil.DamageCalculation(Constant.DAMAGE_DEAL);
        }
    }
}
