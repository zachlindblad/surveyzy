using surveysy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surveysy
{
    public class SurveyPanel : MonoBehaviour
    {
        public string googleSurveyUrl;
        public bool displayOnReady;
        public bool submitOnComplete;

        public Text surveyTitle;
        public Transform questionParent;
        public GameObject surveyPanelRoot;

        public GameObject questionTemplate;

        public int onQuestion = -1;

        public bool isReady = false;

        private List<SurveyQuestionPanel> questions = new List<SurveyQuestionPanel>();

        public void Awake()
        {
            print("in awake");
            if (!string.IsNullOrEmpty(googleSurveyUrl))
            {
                StartCoroutine(GoogleFormsLoader.loadFromUrl(googleSurveyUrl, onSurveyLoaded));
            }
        }

        public void onSurveyLoaded(string error, SurveyDefinition result)
        {
            loadSurveyDefinition(result);
            
            if (displayOnReady)
            {
                openSurvey();
            }
            isReady = true;
        }

        public void openSurvey()
        {
            surveyPanelRoot.SetActive(true);
            advanceQuestion();
        }

        public void loadSurveyDefinition(SurveyDefinition def)
        {
            surveyTitle.text = def.title;
            questions = new List<SurveyQuestionPanel>();
            for(int i = 0; i < def.questions.Length; i++)
            {
                GameObject nqPanel = Instantiate(questionTemplate);
                questions.Add(nqPanel.GetComponent<SurveyQuestionPanel>());
                questions[questions.Count-1].load(def.questions[i]);
                nqPanel.transform.SetParent(questionParent,false);
            }
        }

        public void advanceQuestion()
        {
            if(onQuestion>=0)
            {
                questions[onQuestion].fadeOut();
            }
            onQuestion++;
            if(onQuestion<questions.Count)
            {
                questions[onQuestion].fadeIn();
            }
            else
            {
                if(submitOnComplete)
                {
                    submitSurveyResponse();
                }
                else
                {
                    closeSurvey();
                }
            }
        }

        public void closeSurvey()
        {
            surveyPanelRoot.SetActive(false);
            onQuestion = -1;
            foreach(SurveyQuestionPanel panel in questions)
            {
                panel.fadeOut();
            }
        }

        public void submitSurveyResponse()
        {
            SurveyResponseDefinition response = new SurveyResponseDefinition();
            foreach(SurveyQuestionPanel panel in questions)
            {
                response.itemResponses.Add(panel.getResponse());
            }
            StartCoroutine(GoogleFormsLoader.SendResultForm(googleSurveyUrl, JsonUtility.ToJson(response),(error)=> { closeSurvey(); }));
        }


    }
}