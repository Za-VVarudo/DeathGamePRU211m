using Assets.Scripts.Utils;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject hitbox;
    private EnemyHitBoxController hitboxController;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentScale = transform.localScale;
        transform.localScale = new Vector2(currentScale.x * 1.1412f, currentScale.y * 1.1412f);

        hitbox = transform.Find(Constant.ENEMY_HITBOX).gameObject;
        hitboxController = hitbox.GetComponent<EnemyHitBoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        switch(tag)
        {
            case "PlayerWeapon":
                hitboxController.HPUtil.DamageCalculation(Constant.DAMAGE_DEAL);
                break;
        }
    }
}
