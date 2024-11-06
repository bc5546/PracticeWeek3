using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamagable
{
    // Start is called before the first frame update
    public ConditionUI conditionUI;

    Condition health { get { return conditionUI.health; } }

    Condition hunger { get { return conditionUI.hunger; } }

    Condition stamina { get { return conditionUI.stamina; } }

    Condition mana { get { return conditionUI.mana; } }

    public float noHungerHeathDecay;

    public event Action onTakeDamage;
    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        health.Add(health.passiveValue * Time.deltaTime);
        mana.Add(health.passiveValue * Time.deltaTime);

        if(hunger.curValue == 0)
        {
            health.Subtract(noHungerHeathDecay * Time.deltaTime);
        }

        if(health.curValue == 0)
        {
            Die();
        }
    }

    public void Heal(float value)
    {
        health.Add(value);
    }

    public void Eat(float value) 
    {
        hunger.Add(value);
    }
    public void Die()
    {
        Debug.Log("»ç¸Á");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
        if (health.curValue == 0)
        {
            Die();
        }
    }

    public void UseSkill(float skillCost)
    {
        mana.Subtract(skillCost);
    }

    public void UseManaPotion(float manaAmount)
    {
        mana.Add(manaAmount);
    }

    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount < 0f)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }

    public bool UseHealth(float amount)
    {
        if (health.curValue - amount < 0f)
        {
            return false;
        }

        health.Subtract(amount);
        return true;
    }
}
