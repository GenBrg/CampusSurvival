using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class StructureOption : MonoBehaviour
{
    public StructurePrototype structure;

    private string description;
    private Builder builder;

    private TextMeshProUGUI titleUI;
    private TextMeshProUGUI descriptionUI;

    private void Start()
    {
        GameObject structureDescription = GameObject.Find("Structure Description");
        TextMeshProUGUI[] tmps = structureDescription.GetComponentsInChildren<TextMeshProUGUI>();
        titleUI = tmps[0];
        descriptionUI = tmps[1];

        StringBuilder sb = new StringBuilder(structure.description);
        sb.AppendLine();
        sb.AppendLine(structure.requirement.ToString(1));
        description = sb.ToString();

        builder = CharacterMovement.Instance.gameObject.GetComponent<Builder>();
    }

    public void OnClick()
    {
        // Show structure ghost
        if (structure.requirement.CheckRequirement(1))
        {
            StructureUIController.Instance.CloseStructureUI();
            builder.StartBuild(structure);
        }
    }

    public void OnMouseHover()
    {
        // Show description
        titleUI.text = structure.name;
        descriptionUI.text = description;
    }
}
