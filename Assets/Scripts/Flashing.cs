using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flashing : MonoBehaviour
{

    #region Editor Settings

    [Tooltip("Materials for the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("More Materials for the flash.")]
    [SerializeField] private Material nextFlashMaterial;

    [Tooltip("More Materials for the flash.")]
    [SerializeField] private Material nextFlashMaterial2;

    [Tooltip("More Materials for the flash.")]
    [SerializeField] private Material nextFlashMaterial3;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    # endregion

    #region Private Fields

    private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private Coroutine flashRoutine;
    
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


        originalMaterial = spriteRenderer.material;
        
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = nextFlashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = nextFlashMaterial2;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = nextFlashMaterial3;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
