using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surveysy
{
    public class SurveyQuestionPanel : MonoBehaviour {
        private SurveyQuestionDefinition mydef;
        public Text questionTitle;
        public InputField textInput;

        //multiple choice elements
        public GameObject choiceFrame;
        public GameObject choiceTemplate;
        public GameObject checkTemplate;
        public GameObject choiceParent;
        public ToggleGroup toggleGroup;
        //rangeElements
        public GameObject rangeFrame;
        public Text lowLabel;
        public Text highLabel;
        public GameObject rangeTemplate;
        public Transform rangeParent;

        public Animator myAnimator;

        public void load(SurveyQuestionDefinition def)
        {
            mydef = def;
            questionTitle.text = def.title;
            switch(def.type)
            {
                case "TEXT":
                    textInput.gameObject.SetActive(true);
                    break;
                case "MULTIPLE_CHOICE":
                    choiceFrame.gameObject.SetActive(true);
                    loadChoices(def.choices,choiceTemplate);
                    break;
                case "CHECKBOX":
                    choiceFrame.gameObject.SetActive(true);
                    loadChoices(def.choices,checkTemplate);
                    break;
                case "SCALE":
                    rangeFrame.SetActive(true);
                    loadRange(def.lowLabel,def.upperLabel,def.lowBound,def.upperBound);
                    break;
            }
        }

        public void loadRange(string lowname, string highname, int minVal, int maxVal)
        {
            lowLabel.text = lowname;
            highLabel.text = highname;
            for(int i=0;i<=maxVal-minVal;i++)
            {
                GameObject nrange = Instantiate(rangeTemplate);
                nrange.GetComponentInChildren<Text>().text = (i+minVal).ToString();
                nrange.transform.SetParent(rangeParent);
                nrange.SetActive(true);
            }
        }

        public void loadChoices(string[] choices, GameObject itemTemp)
        {
            foreach (string choice in choices)
            {
                GameObject nchoice = Instantiate(itemTemp);
                nchoice.GetComponentInChildren<Text>().text = choice;
                nchoice.transform.SetParent(choiceParent.transform, false);
                nchoice.SetActive(true);

            }
        }

        public void fadeIn()
        {
            gameObject.SetActive(true);
            myAnimator.Play("FadeQuestionIn");
        }

        public void fadeOut()
        {
            myAnimator.Play("FadeQuestionOut");
        }
        public void fadeOutComplete()
        {
            gameObject.SetActive(false);
        }

        private string[] getMultipleChoiceResponse()
        {
            List<string> selectedToggles = new List<string>();
            foreach(Transform toggle in choiceParent.transform)
            {
                if(toggle.GetComponent<Toggle>().isOn)
                    selectedToggles.Add(toggle.gameObject.GetComponentInChildren<Text>().text);
            }
            return selectedToggles.ToArray();
        }

        private int getScaleResponse()
        {
            foreach (Transform toggle in rangeParent.transform)
            {
                if (toggle.GetComponent<Toggle>().isOn)
                    return int.Parse(toggle.gameObject.GetComponentInChildren<Text>().text);
            }
            return 1;
        }

        public SurveyQuestionResponseDefinition getResponse()
        {
            SurveyQuestionResponseDefinition response = new SurveyQuestionResponseDefinition();
            response.id = mydef.id;
            switch (mydef.type)
            {
                case "TEXT":
                    response.text = new string[] { textInput.text };
                    break;
                case "MULTIPLE_CHOICE":
                case "CHECKBOX":
                    response.text = getMultipleChoiceResponse();
                    break;
                case "SCALE":
                    response.value = getScaleResponse();
                    break;
            }
            return response;
        }
    }
}
