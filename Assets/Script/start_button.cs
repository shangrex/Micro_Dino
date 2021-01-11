using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class start_button : MonoBehaviour, IPointerClickHandler
{
    public int selectValue = 0;
    public bool isSelected = false; 
    public void OnPointerClick(PointerEventData e){
        Scene cur_scene = SceneManager.GetActiveScene();
        if(gameObject.name == "fox_start_button")
            SceneManager.LoadScene(0);
        if (gameObject.name == "car_start_button")
            SceneManager.LoadScene(2);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "fox_start_button")
        {
            if(selectValue >= 512)
            {
                isSelected = true;
            }
            else
            {
                isSelected = false;
            }
        }
        else if (gameObject.name == "car_start_button")
        {
            if (selectValue < 512)
            {
                isSelected = true;
            }
            else
            {
                isSelected = false;
            }
        }
        if (isSelected)
        {
            GetComponent<Image>().color = new Color(0.7f, 0.4f, 0.4f);
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
