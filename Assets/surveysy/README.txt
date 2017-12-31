Basic setup:
1. Create a new form on https://docs.google.com/forms
  -currently supported question types include multiple choice, checkbox, text response, and linear
  -"correct answers" and "other" responses on multiple choice/checkbox not currently supported
2. Click on the "..." menu in the upper right of your form, and select "Script Editor"
3. Copy the contents of "formScript.gs" into the new script and save
4. Click "publish->Deploy as Web app"
5. IMPORTANT: make sure to select "anyone, even anonymous" from the access dropdown, then publish
6. Once published, go back to "publish->Deploy as web app", and copy the app URL
7. Back in Unity, either open the example scene, or copy the surveyCanvas prefab into your scene
8. in the surveyCanvas object, paste the app URL into the "google Survey URL" field
9. IMPORTANT: if the string "/u/<ANY NUMBER>" is in your url, you must remove the "/u/<ANY NUMBER>"
10. click play, and the survey should display!
