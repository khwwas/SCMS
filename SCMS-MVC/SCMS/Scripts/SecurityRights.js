
function SetUserRights() {

    var currentUrl = document.URL.replace(document.URL.split('/')[0] + "/" + document.URL.split('/')[1] + "/" + document.URL.split('/')[2], "..");
    currentUrl = currentUrl.split('?')[0];

    var rightsArr = $("a[href='" + currentUrl + "']").attr("data-rights").split(',');
    var canAdd = rightsArr[0].replace("True", "").replace("False", "none");
    var canEdit = rightsArr[1].replace("True", "").replace("False", "none");
    var canDelete = rightsArr[2].replace("True", "").replace("False", "none");
    var canPrint = rightsArr[3].replace("True", "").replace("False", "none");
    var canImport = rightsArr[4].replace("True", "").replace("False", "none");

    if ($("input[value='Save']").val() != undefined) {
        $("input[value='Save']").css("display", canAdd);
    }

    if ($("input[value='New']").val() != undefined) {
        $("input[value='New']").css("display", canAdd);
    }

    if ($("input[value='Cancel']").val() != undefined) {
        $("input[value='Cancel']").css("display", canAdd);
    }

    if ($("img[alt='Delete']").val() != undefined) {
        $("img[alt='Delete']").css("display", canDelete);
    }

    if ($("img[alt='Edit']").val() != undefined) {
        $("img[alt='Edit']").css("display", canEdit);
    }

    if ($("input[value='Import']").val() != undefined) {
        $("input[value='Import']").css("display", canImport);
    }

    if (canAdd == "none" && $("#rightsLabel").html() == undefined) {
        $("input[value='Cancel']").after("<div id='rightsLabel'><b><font color='red'>You are not allowed to add new records!<font></b></div>");
    }
}

function ShowHideSaveButton() {
    if ($("#rightsLabel").html() != undefined) {
        $("#rightsLabel").remove();
    }
    $("input[value='Save']").css("display", "");
}

