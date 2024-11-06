using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;
    public float useStamina;
    public float useHealth;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public float damage;

    private Animator animator;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    public override void OnAttackInput()
    {
        if (attacking==false) 
        {
            if (CharacterManager.Instance.player.condition.UseStamina(useStamina) && CharacterManager.Instance.player.condition.UseHealth(useHealth))
            {


                    attacking = true;
                    /*animator.SetTrigger("Attack");*/
                    Invoke("OnCanAttack", attackRate);

                
            }
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance)) 
        {
            if(doesGatherResources&&hit.collider.TryGetComponent(out Resource resource))
            {
                resource.Gather(hit.point, hit.normal);
            }
        }
    }
}
