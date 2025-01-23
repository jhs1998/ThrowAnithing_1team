using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    [SerializeField] GameObject player;
    SceneField nextStage; //�������� �̵��� ����
    SceneField randomHiddenRoom; //��й� ����� ����

    Vector3 beforeTeleportPos; //���� ������ ��й����� ����Ÿ�� �� ��ġ
    Vector3 afterTeleportPos; //�ֵ�Ƽ�� ������ ����ź �� ��ġ

    [SerializeField] bool isSceneAdditive; //T�ֵ�Ƽ�� ���� ��������, F�ƴ�



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "PortalHidden")
        {
            int randomNum = Random.Range(1, 100);

            //��Ƽ �� �ε�, LoadSceneMode.Additive
            PortalSceneNumber portalSceneNumber = other.GetComponent<PortalSceneNumber>();
            //randomHiddenRoom = portalSceneNumber.hiddenSceneArr[Random.Range(0, portalSceneNumber.hiddenSceneArr.Length)];
            //ChangeScene(randomHiddenRoom); //0���� 1���� 2���Ĩ
            if (randomNum <= 40)
            {
                randomHiddenRoom = portalSceneNumber.hiddenSceneArr[0];
            }
            else if (randomNum > 40 && randomNum <= 80)
            {
                randomHiddenRoom = portalSceneNumber.hiddenSceneArr[1];
            }
            else if(randomNum > 80)
            {
                randomHiddenRoom = portalSceneNumber.hiddenSceneArr[2];
            }
            ChangeScene(randomHiddenRoom); //0���� 1���� 2���Ĩ
            other.gameObject.SetActive(false);
        }


        if (other.tag == Tag.Portal)
        {
            //���߾� �ε����϶�
            if (isSceneAdditive)
            {

                //�÷��̾� �������� �ִ� �ڸ��� ��������
                Vector3 beforePos = new Vector3(beforeTeleportPos.x, beforeTeleportPos.y, beforeTeleportPos.z);
                transform.position = beforeTeleportPos;
                // ��ü �� Ž��
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene loadedScene = SceneManager.GetSceneAt(i);
                    // Ž���� ���� ���� �� �̸��� ���� ��
                    if (loadedScene.name == randomHiddenRoom.SceneName)
                    {
                        SceneManager.UnloadSceneAsync(loadedScene);
                    }
                }
                ////����� ���� ���� ������ �� ��ε��() ���� -> ����, ���� �ʴ°����� �ذ�
                //Scene additiveScene = SceneManager.GetSceneByName(randomHiddenRoom);

                // SceneManager.UnloadScene(additiveScene);

                //���� false�� �ٲ�
                isSceneAdditive = false;

            }
            //�������� �̵��Ҷ�
            else
            {

                //���� �� �̵�, LoadSceneMode.Single
                //nextStage = other.GetComponent<PortalSceneNumber>().nextScene;
                //SceneManager.LoadScene(nextStage);
                ToStage._ToStage();


            }

        }



    }

    public void ChangeScene(SceneField SceneName)
    {
        //�ֵ�Ƽ��� ���� �� ����
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        isSceneAdditive = true;

        //�÷��̾� ������ ��ġ ����
        beforeTeleportPos = player.transform.position;
        afterTeleportPos = new Vector3(400, 1, 400);

        //�÷��̾� ���������� �ֵ�Ƽ�� ������ ��ġ �̵�
        transform.position = afterTeleportPos;

    }

}
