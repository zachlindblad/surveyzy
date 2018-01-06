using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surveysy
{
    [Serializable]
    public class GoogleFormsLoader {

        /// <summary>
        /// will load google form survey info (questions, title, etc.)
        /// </summary>
        /// <param name="url">Url to load from</param>
        /// <param name="callback">callback returns error string(if exists, and result survey)</param>
        /// <returns></returns>
        public static IEnumerator loadFromUrl(string url, Action<string, SurveyDefinition> callback)
        {
            WWW fetchForm = new WWW(url);
            yield return fetchForm;
            SurveyDefinition result = JsonUtility.FromJson<SurveyDefinition>(fetchForm.text);
            callback.Invoke(null, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Url to send question responses to</param>
        /// <param name="result">JSON string, produced by SurveyResponseDefinition</param>
        /// <param name="callback">callback returns error string(if exists)</param>
        /// <returns></returns>
        public static IEnumerator SendResultForm(string url, string result, Action<string> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            headers.Add("timestamp", (DateTime.Now.ToFileTime()).ToString());
            WWW postForm = new WWW(url, System.Text.Encoding.UTF8.GetBytes(result), headers);
            Debug.Log("sending form");
            yield return postForm;

            callback.Invoke(postForm.error);
        }

    }
}

