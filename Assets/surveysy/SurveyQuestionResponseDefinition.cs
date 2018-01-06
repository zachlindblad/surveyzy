using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surveysy
{
    [Serializable]
    /// Used for Json serialization of response
    public class SurveyQuestionResponseDefinition
    {
        public int id;
        public string[] text;
        public int value = 1;
    }
}
