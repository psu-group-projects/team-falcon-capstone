/*
Author   : Chinh T Cao
           Anh T Nguyen
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

///////////////////////////////////////// Main Page ////////////////////////////////////////////////////////////

/**/
function MainPageAjaxUpdate(page, sort_input, SearchPattern) {
    $.ajax({
        type: "POST",
        url: '/Home/MainPageAjaxUpdate',
        data: { "page": page, "sort_input": sort_input, "SearchPattern": SearchPattern },
        success: function (data) {
            $('div.process-list').empty();
            $('div.process-list').append(data);
        },
        error: function (result) {
            alert(result);
        }
    });
}

/**/
function Main_page_sorting(id) {
    var s_option;

    // If we check this checkbox, turnoff the other sorting checkbox
    if (id == "Main_page_pro_name_sort_acc") s_option = "0";
    else if (id == "Main_page_pro_name_sort_desc") s_option = "1";
    else if (id == "Main_page_last_msg_sort_acc") s_option = "2";
    else if (id == "Main_page_last_msg_sort_desc") s_option = "3";
    else if (id == "Main_page_msg_date_sort_acc") s_option = "4";
    else if (id == "Main_page_msg_date_sort_desc") s_option = "5";
    else if (id == "Main_page_pro_state_sort_acc") s_option = "6";
    else if (id == "Main_page_pro_state_sort_desc") s_option = "7";
    else {
        alert("SORTING ERROR");
    }

    // Make ajax call to controller to update the table
    MainPageAjaxUpdate(parseInt("1"),      // Page number
                            s_option,           // Sort option
                            document.getElementById("main_page_search_input").value);
    current_scrolldown_process = '*_*';
}

/**/
function ExpandedTabUpdate(process_name, msg_or_job, inside_page) {
    
    if (msg_or_job == 'Msg') {
        show_message(inside_page, process_name);
    }
    else {
        show_job(inside_page, process_name);
    }
}

/**/
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

/**/
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
    msg_or_job = 'Job';
    inside_page = page;

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
    msg_or_job = 'Msg';
    inside_page = page;

    // Show messages and hide jobs
    document.getElementById(process_name + 'job').style.display = 'none';
    document.getElementById(process_name + 'MsgBtn').style.display = 'none';
    document.getElementById(process_name + 'message').style.display = 'block';
    document.getElementById(process_name + 'JobBtn').style.display = 'block';
    ProcessMsgUpdate(page, process_name);
}


///////////////////////////////////////// Message Inquiry Page ////////////////////////////////////////////////////////////

/**/
function MsgInquiryUpdate(page_number, sort_option, msg_priority, process_name, SU_SD_msg) {
    $.ajax({
        type: "POST",
        url: '/Home/MsgInquiryUpdate',
        data:
            {   "page_number": page_number,
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

/**/
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

/**/
function Msg_inquiry_collect_info(page_number, sort_option, process_priority, process_name) {
    // Getting the current state of the SU_SD_Checkbox checkbox
    var state = document.getElementById("SU_SD_Checkbox").checked;

    // Run MsgInquiryUpdate with the additional value of SU_SD_Checkbox
    if (state) {
        MsgInquiryUpdate(page_number, sort_option, process_priority, process_name, '1');
    }
    else {
        MsgInquiryUpdate(page_number, sort_option, process_priority, process_name, '0');
    }
}

/**/
function showpopup(msg_id, process_name) {
    if (document.getElementById("popwindow").className == 'popperHid') {
        // Update the div
        GetFullDetailMessage(msg_id, process_name);
        document.getElementById("popwindow").className = 'popperShow';
    } else {
        if (msg_id == "-1")
            document.getElementById("popwindow").className = 'popperHid';
        else {
            // Update the div
            GetFullDetailMessage(msg_id, process_name);
        }
    }
    return false;
}

/**/
function Change_SU_SD_Status(sort_option, process_priority, process_name) {
    // Getting the current state of the SU_SD_Checkbox checkbox
    var state = document.getElementById("SU_SD_Checkbox").checked;

    // Run MsgInquiryUpdate with the additional value of SU_SD_Checkbox
    if (state) {
        MsgInquiryUpdate('1', sort_option, process_priority, process_name, '1');
    }
    else {
        MsgInquiryUpdate('1', sort_option, process_priority, process_name, '0');
    }
}

/**/
function Msg_inquiry_sorting(chkboxname) {   
    var s_option;

    // If we check this checkbox, turnoff the other sorting checkbox
    if (chkboxname == "Msg_inq_msg_id_sort_acc") s_option = "0";
    else if (chkboxname == "Msg_inq_msg_id_sort_desc") s_option = "1";
    else if (chkboxname == "Msg_inq_context_sort_acc") s_option = "2";
    else if (chkboxname == "Msg_inq_context_sort_desc") s_option = "3";
    else if (chkboxname == "Msg_inq_name_sort_acc") s_option = "4";
    else if (chkboxname == "Msg_inq_name_sort_desc") s_option = "5";
    else if (chkboxname == "Msg_inq_prio_sort_acc") s_option = "6";
    else if (chkboxname == "Msg_inq_prio_sort_desc") s_option = "7";
    else if (chkboxname == "Msg_inq_date_sort_acc") s_option = "8";
    else if (chkboxname == "Msg_inq_date_sort_desc") s_option = "9";
    else {
        alert("SORTING ERROR");
    }

    // Make ajax call to controller to update the table
    Msg_inquiry_collect_info(       "1",            // Page number
                                    s_option,       // Sort option
                                    document.getElementById("process_prio_input").value,
                                    document.getElementById("process_name_input").value);
}