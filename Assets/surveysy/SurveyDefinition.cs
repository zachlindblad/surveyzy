using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surveysy
{
    [Serializable]
    public class SurveyDefinition
    {
        public string title;
        public SurveyQuestionDefinition[] questions;
    }

}
