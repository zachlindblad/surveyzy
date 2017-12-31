using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surveysy{
    [Serializable]
    public class SurveyQuestionDefinition
    {
        public string title;
        public string type;
        public int id;

        public string[] choices;

        public int lowBound;
        public int upperBound;
        public string lowLabel;
        public string upperLabel;
    }
}

