using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] GameObject damageTextPrefab;

    private static DamageTextManager instance;

    public static DamageTextManager Instance { get { return instance; } }

    private List<GameObject> activeDamageTexts = new List<GameObject>();

    private void Awake(){
        activeDamageTexts.Capacity = 10;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
    }

    public void ShowDamageText(GameObject enemyPosition, float damage){

        if(activeDamageTexts.Count < activeDamageTexts.Capacity){
            // Create a new damage text object dynamically
            GameObject damageTextObject = Instantiate(damageTextPrefab, FindObjectOfType<Canvas>().transform);
            activeDamageTexts.Add(damageTextObject);
            TextMeshProUGUI damageText = damageTextObject.GetComponent<TextMeshProUGUI>();
            if (damageText != null)
            {
                Vector3 damageTextPosition = enemyPosition.transform.position + Vector3.up * 1.5f; // Adjust the position offset as needed
                damageTextObject.transform.position = Camera.main.WorldToScreenPoint(damageTextPosition);
                damageText.text = damage.ToString();
                damageText.gameObject.SetActive(true);
                StartCoroutine(HideDamageText(damageTextObject));
            }
            else{
                Destroy(damageTextObject); // Destroy the text object if the Text component is not found
            }
        }else if(activeDamageTexts.Count != 0){
            Destroy(activeDamageTexts.First());
            activeDamageTexts.RemoveAt(0);
            ShowDamageText(enemyPosition, damage);
        }
            
    }

    private IEnumerator HideDamageText(GameObject damageTextObject)
    {
        yield return new WaitForSeconds(0.5f); // Adjust the duration as needed
        Destroy(damageTextObject);
    }
}