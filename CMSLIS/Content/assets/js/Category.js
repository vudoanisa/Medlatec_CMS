$(document).ready(function () {
    $('#checkBoxAll').click(function () {
        if ($(this).is(":checked")) {
            $(".chkCheckBoxId").prop("checked", true)
        }
        else {
            $(".chkCheckBoxId").prop("checked", false)
        }
    });

    //$("#CategoryDelete").click(function () {
    //    var selectedIDs = new Array();
    //    $('input:checkBox.checkBox').each(function () {
    //        if ($(this).prop('checked')) {
    //            selectedIDs.push($(this).val());
    //        }
    //    });
    //    if (selectedIDs == '') {
    //        alert("Mời chọn bản ghi để xóa");
    //    } else {
    //        var options = {};
    //        options.url = "/CatalogSystem/CategoryDelete";
    //        options.type = "POST";
    //        options.data = JSON.stringify(selectedIDs);
    //        options.contentType = "application/json";
    //        options.dataType = "json";
    //        options.success = function (msg) {
    //            alert(msg);
    //            location.reload();
    //        };
    //        options.error = function () {
    //            alert("Có lỗi trong quá trình xóa bản ghi");
    //        };
    //        $.ajax(options);
    //    }
    //});

    //$("#CategoryPublic").click(function () {
    //    var selectedIDs = new Array();
    //    $('input:checkBox.checkBox').each(function () {
    //        if ($(this).prop('checked')) {
    //            selectedIDs.push($(this).val());
    //        }
    //    });
    //    if (selectedIDs == '') {
    //        alert("Mời chọn bản ghi để xóa");
    //    } else {
    //        var options = {};
    //        options.url = "/CatalogSystem/CategoryPublic";
    //        options.type = "POST";
    //        options.data = JSON.stringify(selectedIDs);
    //        options.contentType = "application/json";
    //        options.dataType = "json";
    //        options.success = function (msg) {
    //            alert(msg);
    //            location.reload();
    //        };
    //        options.error = function () {
    //            alert("Có lỗi trong quá trình xóa bản ghi");
    //        };
    //        $.ajax(options);
    //    }
    //});

});


//To get selected value an text of dropdownlist
function SelectedValue(ddlObject) {
    //Selected value of dropdownlist
    var selectedValue = ddlObject.value;
    //Selected text of dropdownlist
    var selectedText = ddlObject.options[ddlObject.selectedIndex].innerHTML;
    document.getElementById('cmdSearch').click();

}




function ConfirmDelete() {
    var x = confirm("Bạn có chắc không?");
    if (x) {
        var selectedIDs = new Array();
        $('input:checkBox.checkBox').each(function () {
            if ($(this).prop('checked')) {
                selectedIDs.push($(this).val());
            }
        });
        if (selectedIDs == '') {
            alert("Mời chọn bản ghi để xóa");
        } else {
            var options = {};
            options.url = "/CatalogSystem/CategoryDelete";
            options.type = "POST";
            options.data = JSON.stringify(selectedIDs);
            options.contentType = "application/json";
            options.dataType = "json";
            options.success = function (msg) {
                alert(msg);
                location.reload();
            };
            options.error = function () {
                alert("Có lỗi trong quá trình xóa bản ghi");
            };
            $.ajax(options);
        }
        return true;
    }

    else
        return false;
}



function ConfirmPublic() {
    var x = confirm("Bạn có chắc không?");
    if (x) {
        var selectedIDs = new Array();
        $('input:checkBox.checkBox').each(function () {
            if ($(this).prop('checked')) {
                selectedIDs.push($(this).val());
            }
        });
        if (selectedIDs == '') {
            alert("Mời chọn bản ghi để xóa");
        } else {
            var options = {};
            options.url = "/CatalogSystem/CategoryPublic";
            options.type = "POST";
            options.data = JSON.stringify(selectedIDs);
            options.contentType = "application/json";
            options.dataType = "json";
            options.success = function (msg) {
                alert(msg);
                location.reload();
            };
            options.error = function () {
                alert("Có lỗi trong quá trình xóa bản ghi");
            };
            $.ajax(options);
        }
        return true;
    }

    else
        return false;
}

