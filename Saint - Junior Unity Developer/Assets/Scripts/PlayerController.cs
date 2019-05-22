using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private GameObject[] craftMaterials;
    [SerializeField]
    private GameObject currentMaterial;
    [SerializeField]
    private float jumpForce;
    private Vector3 localScaleCraftBlock;
    private Rigidbody rb;
    [SerializeField]
    private int numberOfJumps;
    [SerializeField]
    private float durationJump;
    private string currentTypeMaterial;
	void Start () {
        localScaleCraftBlock = new Vector3(1, 1, 1);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
       
        
	}

    private void MaterialActive(int i)
    {
        currentMaterial.SetActive(false);
        currentMaterial = craftMaterials[i];
        currentMaterial.SetActive(true);
        
    }

    public void Jump()
    {
        rb.DOJump(transform.position, jumpForce, numberOfJumps, durationJump);
    }

    public void Remove()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 10))
        {
           if (hit.collider.tag == "Block")
            {
                Destroy(hit.collider.gameObject);
            }

        }
    }

    public void Craft()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 10))
        {
            if (hit.collider.tag == "Ground" && currentTypeMaterial == "Basement")
            {
                Vector3 hitPoint = hit.point;
                hitPoint.x = Mathf.Round(hitPoint.x);
                hitPoint.z = Mathf.Round(hitPoint.z);

                GameObject obj = Instantiate(currentMaterial, hitPoint, Quaternion.identity);
                obj.transform.localScale = localScaleCraftBlock;
            }
            else if(hit.collider.tag == "Block" && hit.normal.y == 1 && currentTypeMaterial == "Furniture")
            {
                Vector3 hitPoint = hit.collider.transform.position + hit.normal;
                GameObject obj = Instantiate(currentMaterial, hitPoint, Quaternion.identity);
                obj.transform.localScale = localScaleCraftBlock;
            }

        }
    }

    public void FirstSlot()
    {
        MaterialActive(0);
        currentTypeMaterial = "Basement";
    }

    public void SecondSlot()
    {
        MaterialActive(1);
        currentTypeMaterial = "Furniture";
    }
    
}
