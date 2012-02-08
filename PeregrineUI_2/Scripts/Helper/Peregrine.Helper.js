/*
Author   : Chinh T Cao
           Anh T Nguyen
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

function MainPageAjaxSearch(page, SortingType, SearchPattern) {
    $.ajax({
        type: "POST",
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
    document.getElementById("current_scrolldown_process").innerHTML = '';
}


function MainPageAjaxUpdate(page, SortingType, SearchPattern) {
    $.ajax({
        type: "POST",
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


function ExpandedTabUpdate(process_name, msg_or_job, inside_page) {
    
    if (msg_or_job == 'Msg') {
        show_message(inside_page, process_name);
    }
    else {
        show_job(inside_page, process_name);
    }
}

function ProcessMsgUpdate(page, process_name) {
    $.ajax({
        type: "POST",
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
        type: "POST",
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

// Show Jobs, hide Messages on clicks
function show_job(page, process_name) {
    // Update the current value of current_scrolldown_process
    //document.getElementById("current_scrolldown_process").innerHTML = process_name;

    // Save info of the opened tab
    document.getElementById("msg_or_job").innerHTML = 'Job';
    document.getElementById("inside_page").innerHTML = page;

    // Show jobs and hide messages
    document.getElementById(process_name + 'message').style.display = 'none';
    document.getElementById(process_name + 'JobBtn').style.display = 'none';
    document.getElementById(process_name + 'job').style.display = 'block';
    document.getElementById(process_name + 'MsgBtn').style.display = 'block';
    ProcessJobUpdate(page, process_name);
}

// Show Messages, hide Jobs on clicks
function show_message(page, process_name) {
    // Update the current value of current_scrolldown_process

    // Save info of the opened tab
    document.getElementById("msg_or_job").innerHTML = 'Msg';
    document.getElementById("inside_page").innerHTML = page;

    // Show messages and hide jobs
    document.getElementById(process_name + 'job').style.display = 'none';
    document.getElementById(process_name + 'MsgBtn').style.display = 'none';
    document.getElementById(process_name + 'message').style.display = 'block';
    document.getElementById(process_name + 'JobBtn').style.display = 'block';
    ProcessMsgUpdate(page, process_name);
}


// Message inquiry page helper api
function MsgInquiryUpdate(page_number, sort_option, msg_priority, process_name, SU_SD_msg) {
    $.ajax({
        type: "POST",
        url: '/Home/MsgInquiryUpdate',
        data:
            { "page_number": page_number,
                "sort_option": sort_option,
                "msg_priority": msg_priority,
                "process_name": process_name,
                "SU_SD_msg": SU_SD_msg
            },
        success: function (data) {
            $('div.message-list').empty();
            $('div.message-list').append(data);
        },
        error: function (result) {
            alert(result);
        }
    });
}

function GetFullDetailMessage(msg_id, pro_name) {
    $.ajax({
        type: "POST",
        url: '/Home/MsgInq_getfulldetail',
        data: { "msg_id": msg_id },
        success: function (data) {        
            document.getElementById("popwindow_header").innerHTML = pro_name;
            document.getElementById("popwindow_message").innerHTML = data;
        },
        error: function (result) {
            alert(result);
        }
    });
}