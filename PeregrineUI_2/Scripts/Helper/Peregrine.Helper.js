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