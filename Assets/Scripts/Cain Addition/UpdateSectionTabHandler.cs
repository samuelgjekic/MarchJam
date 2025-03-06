
using UnityEngine;
using UnityEngine.UI;


public class UpdateSectionTabHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject PlayerPanel;
    [SerializeField]
    private GameObject SlingShotPanel;
    [SerializeField]
    private GameObject SkillPanel;

    [SerializeField]
    private GameObject InactivePlayerButton;
    [SerializeField]
    private GameObject InactiveSlingShotButton;
    [SerializeField]
    private GameObject InactiveSkillButton;




    void Start()
    {
        PlayerPanel.gameObject.SetActive(true);
        SlingShotPanel.gameObject.SetActive(false);
        SkillPanel.gameObject.SetActive(false);

        //button
        InactivePlayerButton.gameObject.SetActive(false);
        InactiveSkillButton.gameObject.SetActive(true);
        InactiveSlingShotButton.gameObject.SetActive(true);
        
    }



    public void SlingShotUpdatePanel()
    {
        PlayerPanel.gameObject.SetActive(false);
        SkillPanel.gameObject.SetActive(false);
        SlingShotPanel.gameObject.SetActive(true);

        //button
        InactivePlayerButton.gameObject.SetActive(true);
        InactiveSkillButton.gameObject.SetActive(true);
        InactiveSlingShotButton.gameObject.SetActive(false);
    }

    public void SkillUpdatePanel()
    {
        SlingShotPanel.gameObject.SetActive(false);
        PlayerPanel.gameObject.SetActive(false);
        SkillPanel.gameObject.SetActive(true);

        //button
        InactivePlayerButton.gameObject.SetActive(true);
        InactiveSkillButton.gameObject.SetActive(false);
        InactiveSlingShotButton.gameObject.SetActive(true);

    }

    public void PlayerUpdatePanel()
    {
        SlingShotPanel.gameObject.SetActive(false);
        SkillPanel.gameObject.SetActive(false);
        PlayerPanel.gameObject.SetActive(true);

        //button
        InactivePlayerButton.gameObject.SetActive(false);
        InactiveSkillButton.gameObject.SetActive(true);
        InactiveSlingShotButton.gameObject.SetActive(true);

    }





}