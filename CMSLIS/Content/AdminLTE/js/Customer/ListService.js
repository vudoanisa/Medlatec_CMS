(function ($) {
    'use strict';
    var datatableInit = function () {
        $('#sorting-table').dataTable(
            {
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            }
        );
    };
    $(function () {
        datatableInit();
    });

}).apply(this, [jQuery]);



$(document).ready(function () {
    $('#checkBoxAll').click(function () {
        if ($(this).is(":checked")) {
            $(".chkCheckBoxId").prop("checked", true)
        }
        else {
            $(".chkCheckBoxId").prop("checked", false)
        }
    });

     


    $('.select2').select2();

    var check = false;

    $(":button").click(function (event) {

       
        if ($(this).prop("id") == "SaveListService") {
            check = true;
        }
        if ($(this).prop("id") == "cmdSearch") {
            check = true;
        }

    });

    $('form').submit(function (e) {
        if (!check) {
            e.preventDefault();
            return false;
        }
    });

    $("#SERVICE_CODE").keydown(function () {
        var key_enter = 13; // 13 = Enter
        if (key_enter == event.keyCode) {
            event.keyCode = 0;
            $("#SERVICE_NAME").focus();
        }
    });

    $("#SERVICE_NAME").keydown(function () {
        var key_enter = 13; // 13 = Enter
        if (key_enter == event.keyCode) {
            event.keyCode = 0;
            $("#SERVICE_NAME_ENG").focus();
        }
    });

    $("#SERVICE_NAME_ENG").keydown(function () {
        var key_enter = 13; // 13 = Enter
        if (key_enter == event.keyCode) {
            event.keyCode = 0;
            $("#SERVICE_UNIT").focus();
        }
    });

    $("#SERVICE_PRICE").keydown(function () {
        var key_enter = 13; // 13 = Enter
        if (key_enter == event.keyCode) {
            event.keyCode = 0;
            $("#SERVICE_PRICE_INSURANCE").focus();
        }
    });

    $("#SERVICE_PRICE_INSURANCE").keydown(function () {
        var key_enter = 13; // 13 = Enter
        if (key_enter == event.keyCode) {
            event.keyCode = 0;
            $("#SERVICE_RETURN_DATE").focus();
        }
    });

    $("#SERVICE_RETURN_DATE").keydown(function () {
        var key_enter = 13; // 13 = Enter
        if (key_enter == event.keyCode) {
            event.keyCode = 0;
            $("#SERVICE_NOTE").focus();
        }
    });

    $("#SERVICE_NOTE").keydown(function () {
        var key_enter = 13; // 13 = Enter
        if (key_enter == event.keyCode) {
            event.keyCode = 0;
            $("#SERVICETYPE").focus();
        }
    });
     

});


//To get selected value an text of dropdownlist
function SelectedSearch(ddlObject) {
    //Selected value of dropdownlist
    var selectedValue = ddlObject.value;
    //Selected text of dropdownlist
    var selectedText = ddlObject.options[ddlObject.selectedIndex].innerHTML;
    document.getElementById('cmdSearch').click();

}

function SelectedValue(ddlObject) {
    //Selected value of dropdownlist
    var selectedValue = ddlObject.value;
    //Selected text of dropdownlist
    var selectedText = ddlObject.options[ddlObject.selectedIndex].innerHTML;
    $("#SERVICE_PRICE").focus();
}


function SelectedValue1(ddlObject) {
    //Selected value of dropdownlist
    var selectedValue = ddlObject.value;
    //Selected text of dropdownlist
    var selectedText = ddlObject.options[ddlObject.selectedIndex].innerHTML;
    $("#SaveListService").focus();
}

function ConfirmDelete() {



    var forgeryId = $("#forgeryToken").val();
    var selectedIDs = new Array();
    $('input:checkBox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    if (selectedIDs == '') {
        $.alert({
            title: 'Thông báo!',
            content: 'Mời chọn bản ghi để xóa!',
        });
        return false;
    } else {
        $.confirm({
            title: 'Xác nhận!',
            content: 'Bạn có chắc thực hiện không?',
            buttons: {
                specialKey: {
                    text: 'Đồng ý',

                    action: function () {

                        $.ajax({
                            type: "POST",
                            url: "/CatalogSystem/ListServiceDelete",
                            data: JSON.stringify(selectedIDs),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            headers: {
                                'VerificationToken': forgeryId
                            }
                        }).done(function (msg) {

                            toastr.success(msg, 'Thông báo');
                            window.setTimeout(function () { location.reload() }, 3000);
                        }).fail(function (data) {
                            $.alert({
                                title: 'Thông báo!',
                                content: 'Có lỗi trong quá trình xóa bản ghi!',
                            });
                        });
                    }
                },
                alphabet: {
                    text: 'Bỏ qua',
                    action: function () {

                    }
                }
            }
        });
        return true;
    }



     
}



function ConfirmPublic() {


    var forgeryId = $("#forgeryToken").val();
    var selectedIDs = new Array();
    $('input:checkBox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    if (selectedIDs == '') {
        $.alert({
            title: 'Thông báo!',
            content: 'Mời chọn bản ghi để duyệt!',
        });
        return false;
    } else {
        $.confirm({
            title: 'Xác nhận!',
            content: 'Bạn có chắc thực hiện không?',
            buttons: {
                specialKey: {
                    text: 'Đồng ý',

                    action: function () {

                        $.ajax({
                            type: "POST",
                            url: "/CatalogSystem/ListServicePublic",
                            data: JSON.stringify(selectedIDs),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            headers: {
                                'VerificationToken': forgeryId
                            }
                        }).done(function (msg) {

                            toastr.success(msg, 'Thông báo');
                            window.setTimeout(function () { location.reload() }, 3000);
                        }).fail(function (data) {
                            $.alert({
                                title: 'Thông báo!',
                                content: 'Có lỗi trong quá trình duyệt bản ghi!',
                            });
                        });
                    }
                },
                alphabet: {
                    text: 'Bỏ qua',
                    action: function () {

                    }
                }
            }
        });
        return true;
    }

     
}

