using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class start_button : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData e){
        Scene cur_scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
