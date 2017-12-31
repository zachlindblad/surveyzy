using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SurveyResponseDefinition {
    public List<SurveyQuestionResponseDefinition> itemResponses = new List<SurveyQuestionResponseDefinition>();
}
