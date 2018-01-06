using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surveysy
{
    [Serializable]
    public class SurveyResponseDefinition
    {
        public List<SurveyQuestionResponseDefinition> itemResponses = new List<SurveyQuestionResponseDefinition>();
    }
}
