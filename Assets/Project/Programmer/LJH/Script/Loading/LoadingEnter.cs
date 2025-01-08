using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingEnter : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        //  //Comment : 씬 이름 변경해줘야함
        //  if (SceneManager.GetActiveScene().name == "GameSceneSta1")
        //  {
        //      if (other.transform.tag == Tag.Player)
        //          Loading.LoadScene("GameSceneSta1");
        //  }
        //  else if( SceneManager.GetActiveScene().name == "GameSceneSta2")
        //  {
        //      if (other.transform.tag == Tag.Player)
        //          Loading.LoadScene("GameSceneSta2");
        //  }
        //  else if (SceneManager.GetActiveScene().name == "GameSceneSta2")
        //  {
        //      if (other.transform.tag == Tag.Player)
        //          Loading.LoadScene("GameSceneBoss");
        //  }

        //테스트용
        if (other.transform.tag == Tag.Player)
                Loading.LoadScene("GameSceneSta1");
    }

}
