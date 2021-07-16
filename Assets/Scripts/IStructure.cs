using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IStructure : MonoBehaviour
{
    public StructurePrototype prototype;
    public int level;

    private Interactable interactable;
    private Health health;

    public string Name
    {
        get => prototype.name;
    }

    public string Description
    {
        get => prototype.description;
    }

    public ItemRequirement Requirement
    {
        get => prototype.requirement;
    }

    private void OnLevelUp()
    {
        health.maxHealth = prototype.GetMaxHP(level);
        health.currentHealth = health.maxHealth;
        prototype.fulfillment[level - 1].FulfillRequirements();
        UpdateHintText();
    }

    private void UpdateHintText()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Lv.{0} {1}\n", level, prototype.name);
        sb.AppendFormat("HP: {0} / {1}\n", health.currentHealth, health.maxHealth);
        if (prototype.requirement.CheckLevel(level + 1))
        {
            sb.AppendLine("Press E to upgrade, requirement: ");
            sb.Append(prototype.requirement.ToString(level + 1));
        }

        interactable.Hint = sb.ToString();
    }

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        health = GetComponent<Health>();

        level = 1;
        interactable.showHint = true;
        interactable.interactKey = KeyCode.E;
        health.onHealthChange = (float currentHealth, float maxHealth) =>
        {
            UpdateHintText();
        };
        OnLevelUp();
    }

    public void LevelUp()
    {
        ItemRequirement itemRequirement = prototype.requirement;

        // Check if level is valid
        if (!itemRequirement.CheckLevel(level + 1))
        {
            return;
        }
        
        if (itemRequirement.TryConsumeRequirement(level + 1))
        {
            ++level;
            OnLevelUp();
        }
    }
}
