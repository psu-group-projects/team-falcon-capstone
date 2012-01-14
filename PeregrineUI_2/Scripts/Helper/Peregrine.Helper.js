/*
Author   : Chinh T Cao
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

function MainPageAjaxUpdate(page, SortingType, SearchPattern) {
    $.ajax({
        url: '/Home/MainPageAjaxUpdate',
        data: { "page": page, "SortingType": SortingType, "SearchPattern": SearchPattern },
        success: function (data) {
            $('div.process-list').empty();
            $('div.process-list').append(data);
        },
        error: function (result) {
            alert(result);
        }
    });
}

function ProcessMsgUpdate(page, process_name) {
    $.ajax({
        url: '/Home/ProcessMsgUpdate',
        data: { "page": page, "processName": process_name },
        success: function (data) {
            $('div.' + process_name + 'message').empty();
            $('div.' + process_name + 'message').append(data);
        },
        error: function (result) {
            alert(result);
        }
    });
}

function ProcessJobUpdate(page, process_name) {
    $.ajax({
        url: '/Home/ProcessJobUpdate',
        data: { "page": page, "processName": process_name },
        success: function (data) {
            $('div.' + process_name + 'job').empty();
            $('div.' + process_name + 'job').append(data);
        },
        error: function (result) {
            alert(result);
        }
    });
}