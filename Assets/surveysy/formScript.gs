var cachedActiveForm = FormApp.getActiveForm();
var cachedSurveyInfo = JSON.stringify(getSurveyInfo());

function getQuestionInfo(question)
{
  var info =  {
    "id":question.getId(),
    "title":question.getTitle(),
  };
  Logger.log("got question:"+question.getTitle());
  switch(question.getType())
  {
    case FormApp.ItemType.TEXT:
      info["type"] = "TEXT";
      break;
    case FormApp.ItemType.MULTIPLE_CHOICE:
      info["type"] = "MULTIPLE_CHOICE";
      info["choices"] = question.asMultipleChoiceItem().getChoices().map(function(choice) {return choice.getValue()});
      break;
    case FormApp.ItemType.CHECKBOX:
      info["type"] = "CHECKBOX";
      info["choices"] = question.asCheckboxItem().getChoices().map(function(choice) {return choice.getValue()});
      break;
    case FormApp.ItemType.SCALE:
      info["type"] = "SCALE";
      var scaleItem = question.asScaleItem();
      info["lowBound"] = scaleItem.getLowerBound();
      info["upperBound"] = scaleItem.getUpperBound();
      info["lowLabel"] = scaleItem.getLeftLabel();
      info["upperLabel"] = scaleItem.getRightLabel();
      break;
  }
  return info;
}

function getSurveyInfo()
{
  var form = cachedActiveForm;

  return {"title":form.getTitle(),"questions":form.getItems().map(function(x) { return getQuestionInfo(x); }) };
}


function addItemResponse(form,formResponse,jsonItem)
{
  var formItem = form.getItemById(jsonItem['id']);

  var itemResponse;

  switch(formItem.getType())
  {
    case FormApp.ItemType.TEXT:
      itemResponse = formItem.asTextItem().createResponse(jsonItem['text'][0]);
      break;
    case FormApp.ItemType.MULTIPLE_CHOICE:
      itemResponse = formItem.asMultipleChoiceItem().createResponse(jsonItem['text'][0]);
      break;
    case FormApp.ItemType.CHECKBOX:
      itemResponse = formItem.asCheckboxItem().createResponse(jsonItem['text']);
      break;
    case FormApp.ItemType.SCALE:
      itemResponse = formItem.asScaleItem().createResponse(jsonItem['value']);
      break;
  }
  if(formResponse != null && itemResponse != null)
    formResponse.withItemResponse(itemResponse);
}


function doGet(request){

  Logger.log(cachedSurveyInfo);
  // return JSON text with the appropriate Media Type
  return ContentService.createTextOutput(cachedSurveyInfo).setMimeType(ContentService.MimeType.JSON);

}

function doPost(e) {
  var form = FormApp.getActiveForm();
  var response = form.createResponse();
  var responseJson = JSON.parse(e.postData.contents);

  responseJson['itemResponses'].forEach(function(x) {addItemResponse(form,response,x);});

  response.submit();
}
