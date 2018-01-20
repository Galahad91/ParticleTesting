using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrossHairManager : MonoBehaviour
{
    public static CrossHairManager instance = null;

    public Texture2D crosshairHitTexture;
    public Texture2D crosshairDefaultTexture;
    public Transform crosshairPos;
    public float crossHairDistance;
    public float offsetX;
    public float offsetY;
    public float size;

    [HideInInspector] public Vector3 screenPos;
    [HideInInspector] public bool isEnemyHit = false;
    [HideInInspector] bool cursorLocked = true;

	void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Check if and enemy gets hit
    public IEnumerator EnemyHit()
    {
        isEnemyHit = true;
        yield return new WaitForSeconds(0.3f);
        isEnemyHit = false;
    }

    //Draw Crosshair
    private void OnGUI()
    {
        screenPos = Camera.main.WorldToScreenPoint(crosshairPos.position);
        screenPos.y = Screen.height / 2 - offsetY - size/2;
        screenPos.x = Screen.width/2 + offsetX - size/2;

        if (isEnemyHit)
            GUI.DrawTexture(new Rect(screenPos.x, screenPos.y, size, size), crosshairHitTexture);
        else
            GUI.DrawTexture(new Rect(screenPos.x, screenPos.y, size, size), crosshairDefaultTexture);
    }

    void Update ()
    {
        crosshairPos.position = (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2 + offsetX, Screen.height / 2 + offsetY, crossHairDistance)));
        crosshairPos.DOLookAt(Camera.main.transform.position,0.1f);

        if (Input.GetButtonDown("Cancel") && cursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            cursorLocked = false;
        }
        if(Input.GetMouseButtonDown(0) && !cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = true;
        }
	}
}
