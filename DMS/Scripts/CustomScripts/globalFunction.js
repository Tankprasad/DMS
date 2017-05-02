

var successCheck = false;

var addNew = function (addHtmlInnerId, createActionName, CreatecontrollerName, areaName) {
    $(addHtmlInnerId).modal({ backdrop: 'static', keyboard: false });
    //var url = "/" + '"' + CreatecontrollerName + '"' + "/" + '"' + createActionName + '"';

    $.ajax({
        url: "/" + areaName + "/" + CreatecontrollerName + "/" + createActionName,
        type: 'GET',
        data: {

        },
        success: function (result) {
            $(addHtmlInnerId).html(result);
            $(addHtmlInnerId).modal('show');
        },
        error: function () {
        }
    });
};
//update innerHtml with data sbt

var update = function (value, editHtmlInnerId, EditActionName, EditcontrollerName, areaName) {
    
    $(editHtmlInnerId).modal({ backdrop: 'static', keyboard: false });
   

    $.ajax({
        url: "/" + areaName + "/" + EditcontrollerName + "/" + EditActionName,
        type: 'GET',
        data:
                      {
                          id: value,
                      },
        success: function (result) {
            $(editHtmlInnerId).html(result);
            $(editHtmlInnerId).modal('show');
        },
        error: function () {
        }
    });
};
//delete html popup by sbt
var deleteRow = function (id, name, actionName, controllerName, partialActionName,areaName, appendHtmlId) {

    //if (id<0 || id==null) {
    //    return;  
    //}
    var url = "/CommonAjaxRequest/DeleteConformation";
    $.ajax({
        url: url,
        type: 'GET',
        data:
                      {
                          id: id,
                          name: name,
                          actionName: actionName,
                          controllerName: controllerName,
                          partialActionName: partialActionName,
                          areaName:areaName,
                          appendHtmlId: appendHtmlId

                      },
        success: function (result) {
            $("#delete-conformation").html(result);
            $("#delete-conformation").modal('show');
            successCheck = true;
        },
        error: function () {
            successCheck = false;
        }
    });
}

//search by sundar all types of module
var search = function (val,page,searchHtmlInnerId, actionName, controllerName, areaName) {
    $.ajax({
        url: "/" + areaName + "/" + controllerName + "/" + actionName,
        type: 'GET',
        data:
                      {
                          name: val,
                          page: page,
                      },
        success: function (result) {
            $(searchHtmlInnerId).empty();
            $(searchHtmlInnerId).html(result);
        },
        error: function () {
        }
    });
};

//message alter or poup

var bootstrap_alert = function (elem, message, timeout, classMesssage) {
    if (successCheck) {
        $(elem).show().html('<div class=alert-success fade in"" style="height:40px;"><button type="button" class="close" data-dismiss="alert" >&times;</button><span>' + message + '</span></div>');

    }
    else {
        $(elem).show().html('<div class=alert-danger fade in""style="height:40px;"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button><span>' + message + '</span></div>');

    }

    //if (timeout || timeout === 0) {
    //    setTimeout(function () {
    //        $(elem).alert('close');
    //    }, timeout);
    //}
};

//post data in httppost in controller
var postData = function (formId, popupHtmlDivId, actionName, controllerName, areaName, htmlAppendListId, partialAction) {

    var form = true;// $(formId);

    if (form) {
        $.ajax({
            type: "POST",
            //url: '@Url.Action(' + actionName + ',' + controllerName + ')',
            url: "/" + areaName + "/" + controllerName + "/" + actionName,
            data: $(formId).serialize(),
            datatype: "html",
            success: function (data) {
                successCheck = true;
                $(popupHtmlDivId).modal('hide');
                bootstrap_alert('#form_errors', data, 3000, successCheck)
                search('','', htmlAppendListId, partialAction, controllerName,areaName);
            },
            error: function (result) {
                successCheck = false;
                bootstrap_alert('#form_errors', data, 3000, successCheck)

            }
        });
    }
}