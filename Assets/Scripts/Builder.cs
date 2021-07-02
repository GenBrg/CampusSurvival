using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

/**
 *  Builder script that handles structure placement
 *  @author Jiasheng Zhou
 */
public class Builder : MonoBehaviour
{
    public GameObject structureGhost;
    public GameObject structure;
    public Material structureGhostValidMaterial;
    public Material structureGhostInvalidMaterial;

    private const float kMaxBuildDistance = 5.0f;
    private const float kRotateSpeed = 30.0f;
    private const float kMaxAllowedTiltAngle = 45.0f;
    private float rotation = 0.0f;
    private LayerMask groundLayer;
    private Camera playerCamera;
    private bool locationValid;
    private Backpack backpack;
    private Rigidbody structureGhostRigidbody;

    public bool Active
    {
        get => structureGhost != null && structure != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();

        // TODO Instantiate upon building UI button
        StartBuild(structureGhost, structure);

        groundLayer = LayerMask.NameToLayer("Ground");
        backpack = Backpack.Instance;
    }

    void StartBuild(GameObject structureGhost, GameObject structure)
    {
        this.structureGhost = Instantiate(structureGhost);
        this.structure = structure;
        structureGhostRigidbody = structureGhost.GetComponent<Rigidbody>();
    }

    void CancelBuild()
    {
        if (this.structureGhost != null)
        {
            Destroy(structureGhost);
        }
        this.structure = null;
    }

    void TryBuild()
    {
        if (CheckRequirement())
        {
            Instantiate(structure, structureGhost.transform.position, structureGhost.transform.rotation);
            CancelBuild();
        }
    }

    bool CheckRequirement()
    {
        if (!locationValid)
        {
            return false;
        }

        // TODO Check requirements
        return true;
    }

    void SetGhostMaterial(Material material)
    {
        foreach (MeshRenderer meshRenderer in structureGhost.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = material;
        }
    }

    void ShowGhost(RaycastHit raycastHit)
    {
        locationValid = true;

        structureGhost.SetActive(true);
        structureGhost.transform.position = raycastHit.point;

        // Align model normal
        structureGhost.transform.rotation = Quaternion.identity;
        structureGhost.transform.rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
        structureGhost.transform.Rotate(Vector3.up, rotation, Space.Self);

        //structureGhostRigidbody.MovePosition(raycastHit.point);
        //structureGhostRigidbody.MoveRotation(Quaternion.identity);

        // TODO check colliding & color
        if (structureGhost.GetComponent<ActiveCollider>().isColliding)
        {
            locationValid = false;
            SetGhostMaterial(structureGhostInvalidMaterial);
        }
        else
        {
            SetGhostMaterial(structureGhostValidMaterial);
        }
    }

    void HideGhost()
    {
        locationValid = false;
        structureGhost.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
        {
            return;
        }

        List<RaycastHit> raycastHits = new List<RaycastHit>(Physics.RaycastAll(new Ray(playerCamera.transform.position, playerCamera.transform.forward), kMaxBuildDistance));

        if (raycastHits.Count == 0)
        {
            HideGhost();
        }
        else
        {
            raycastHits.RemoveAll((hit) => hit.transform.root == structureGhost.transform);
            raycastHits.Sort((hit1, hit2) => {
                float dist = hit1.distance - hit2.distance;
                if (dist < 0.0f)
                {
                    return -1;
                }
                else if (dist > 0.0f)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            if (raycastHits.Count > 0 && raycastHits[0].collider.gameObject.layer == groundLayer
                && Vector3.Angle(Vector3.up, raycastHits[0].normal) <= kMaxAllowedTiltAngle)
            {
                ShowGhost(raycastHits[0]);
            }
            else
            {
                HideGhost();
            }
        }

        if (structureGhost.activeInHierarchy && Input.GetButton("Rotate"))
        {
            rotation += (Time.deltaTime * kRotateSpeed) % 360.0f;
        }

        if (Input.GetMouseButton(1))
        {
            CancelBuild();
        } 
        else if (Input.GetMouseButton(0))
        {
            TryBuild();
        }



        //bool hit = Physics.Raycast(new Ray(playerCamera.transform.position, playerCamera.transform.forward), out RaycastHit raycastHit, kMaxBuildDistance);
        
        //if (
        //    hit
        //    && raycastHit.collider.gameObject.layer == groundLayer
        //    && Vector3.Angle(Vector3.up, raycastHit.normal) <= kMaxAllowedTiltAngle
        //    )
        //{
        //    structureGhost.SetActive(true);
        //    structureGhost.transform.position = raycastHit.point;

        //    if (structureGhost.GetComponent<ActiveCollider>().isColliding)
        //    {
        //        structureGhost.SetActive(false);
        //        return;
        //    }

        //    // Rotate model
        //    if (Input.GetButton("Rotate"))
        //    {
        //        rotation += (Time.deltaTime * kRotateSpeed) % 360.0f;
        //    }

        //    // Align model normal
        //    structureGhost.transform.rotation = Quaternion.identity;
        //    structureGhost.transform.rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
        //    structureGhost.transform.Rotate(Vector3.up, rotation, Space.Self);
        //}
        //else
        //{
        //    if (hit && raycastHit.collider.transform.root == structureGhost.transform)
        //    {
        //        return;
        //    }

        //    structureGhost.SetActive(false);
        //}
    }
}
