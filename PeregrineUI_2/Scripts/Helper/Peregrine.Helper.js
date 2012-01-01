/*
Author   : Chinh T Cao
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

function paging (page) {
    $.ajax({      
        url: '/Home/ProcessList',
        data: { "page": page },
        success: function (data) {
            $('div.process-list').empty();
            $('div.process-list').append(data);
        },
        error: function (result) {
            alert(result);
        }
    });
}

function searching(x) {
    var searchval = document.getElementById(x).value;

    $.ajax({
        url: '/Home/AjaxSearch',
        data: { "searchpattern": searchval },
        success: function (data) {
            $('div.process-list').empty();
            $('div.process-list').append(data);
        },
        error: function (result) {
            alert(result);
        }
    });
}