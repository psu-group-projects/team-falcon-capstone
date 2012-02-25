/*
Author   : Chinh T Cao
           Anh T Nguyen
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

///////////////////////////////////////// Main Page ////////////////////////////////////////////////////////////

/**/
function Main_page_setup(s_process) {
    // Fix event.layerx and layery broken warning, work like a charm
    var all = $.event.props,
    len = all.length,
    res = [];
    while (len--) {
        var el = all[len];
        if (el != 'layerX' && el != 'layerY') res.push(el);
    }
    $.event.props = res;

    tab_amt = 0;

    // Initialize the default value for current_scrolldown_process

    current_scrolldown_process = '*_*';

    var search_process = s_process;

    MainPageAjaxUpdate(1, 7, search_process);

    // Use enter to send value in input box. Work for IE, Firefox and Chrome
    $('#main_page_search_input').keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            MainPageAjaxUpdate(1, get_selected_sort_arrow_index(), document.getElementById("main_page_search_input").value);
            //current_scrolldown_process = '*_*';
        } else {
            if (document.getElementById("main_page_search_input").value == "") {
                MainPageAjaxUpdate(1, get_selected_sort_arrow_index(), "");
            } else {
                $.ajax({
                    type: "POST",
                    url: '/Home/AutoCompleteUpdate',
                    data: { "search_string": document.getElementById("main_page_search_input").value },
                    success: function (data) {
                        var availableTags = data.split(",");
                        $("#main_page_search_input").autocomplete({
                            source: availableTags,
                            select: (function (event, ui) {
                                MainPageAjaxUpdate(1, get_selected_sort_arrow_index(), ui.item.value);
                            })
                        });
                    },
                    error: function (result) {
                        alert(result);
                    }
                });
            }
        }
    });

    // Automatic refresh the page        
        
    setInterval('MainPageAjaxUpdate(main_page_accumulate_page, main_page_current_sort, document.getElementById("main_page_search_input").value)', Refresh_Rate);
}

function refreshMainPage() {
    MainPageAjaxUpdate(main_page_accumulate_page, main_page_current_sort, document.getElementById("main_page_search_input").value);
}

function toggleFixedInfoTab() {
    if ($("#fixed_info_content").css("display") == "none") {
        $("#fixed_info_content").slideDown(500, function () {
            if (($(".process-list").offset().top + $(".process-list").height() + $("#fixed_info_content").height()) > $(window).height()) {
                $("#fixed_info_page_padding").css("height", $("#fixed_info_content").css("height"));
            }
        });
    } else {
        $("#fixed_info_page_padding").css("height", "0");
        $("#fixed_info_content").slideUp(500);
    }

}

function showFixedInfoTab() {
    if ($("#fixed_info_content").css("display") == "none") {
        $("#fixed_info_content").slideDown(500, function () {
            if (($(".process-list").offset().top + $(".process-list").height() + $("#fixed_info_content").height()) > $(window).height()) {
                $("#fixed_info_page_padding").css("height", $("#fixed_info_content").css("height"));
            }
        });
    } else {
        if (($(".process-list").offset().top + $(".process-list").height() + $("#fixed_info_content").height()) < $(window).height()) {
            $("#fixed_info_page_padding").css("height", "0");
        } else {
            $("#fixed_info_page_padding").css("height", $("#fixed_info_content").css("height"));
        }
    }
}

function get_selected_sort_arrow_index() {
    var up_arrows = $(".up_arrow");
    for (var x = 0; x < up_arrows.length; ++x) {
        if (up_arrows.eq(x).hasClass("arrow_selected")) {
            return x * 2;
        }
    }
    var down_arrows = $(".down_arrow");
    for (x = 0; x < down_arrows.length; ++x) {
        if (down_arrows.eq(x).hasClass("arrow_selected")) {
            return (x * 2) + 1;
        }
    }
    return -1;
}

/**/
function Main_partial_page_setup(acc_page, sort_type) {
    main_page_accumulate_page = acc_page;
    main_page_current_sort = sort_type;

    //$("#process_table tr:odd").addClass("odd");
    //$("#process_table tr:not(.odd)").hide();
    //$("#process_table tr:first-child").show();

    opened_row_id = $('#fixed_info_tab').data('process_name');
    if (opened_row_id != undefined) {
        ExpandedTabUpdate(opened_row_id, msg_or_job, more_info_accumulate_page);
    }
             
    // Handle clicking msg page number 
    /*
    $(".process_msg_page-number").live("click", function () {
        show_message(   parseInt($(this).html()),
                        current_scrolldown_process);
    });

    // Handle clicking job page number 
    $(".process_job_page-number").live("click", function () {
        show_job(   parseInt($(this).html()),
                    current_scrolldown_process);
    });*/
}

/**/
/*
function toggleMoreInfo(id) {
    //check to see if requested tr is already open
    var is_closed = ($("#" + id + "_more").css("display") == "none");
    //first close all other trs...
    $(".more_info_rows").hide();
    $(".arrow").removeClass("up");
    //then show the right tr
    if (is_closed) {
        $("#" + id + "_more").fadeIn(500);
        $("#" + id + " .arrow").addClass("up");
        show_message('1', id);
        current_scrolldown_process = id;
    }
    else {
        current_scrolldown_process = '*_*';
    }
}*/

function toggleMoreInfo(id) {
    show_message(1, id);
    $(".table_row_selected").removeClass("table_row_selected");
    $("#" + id).addClass("table_row_selected");
}

/**/
function MainPageAjaxUpdate(page, sort_input, process_name) {
    current_scroll_pos = $(window).scrollTop();
    $.ajax({
        type: "POST",
        url: '/Home/MainPageAjaxUpdate',
        data: { "page": page, "sort_input": sort_input, "process_name": process_name },
        success: function (data) {         
            $('.process-list').html(data);     
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
    if (id == "Main_page_pro_name_sort_acc") {
        s_option = "0";
        $("#Main_page_pro_name_sort_acc div").addClass("arrow_selected");
        $("#Main_page_pro_name_sort_desc div").removeClass("arrow_selected");
    }
    else if (id == "Main_page_pro_name_sort_desc") {
        s_option = "1";
        $("#Main_page_pro_name_sort_acc div").removeClass("arrow_selected");
        $("#Main_page_pro_name_sort_desc div").addClass("arrow_selected");
    }
    else if (id == "Main_page_last_msg_sort_acc") {
        s_option = "2";
        $("#Main_page_last_msg_sort_acc div").addClass("arrow_selected");
        $("#Main_page_last_msg_sort_desc div").removeClass("arrow_selected");
    }
    else if (id == "Main_page_last_msg_sort_desc") {
        s_option = "3";
        $("#Main_page_last_msg_sort_acc div").removeClass("arrow_selected");
        $("#Main_page_last_msg_sort_desc div").addClass("arrow_selected");
    }
    else if (id == "Main_page_msg_date_sort_acc") {
        s_option = "4";
        $("#Main_page_msg_date_sort_acc div").addClass("arrow_selected");
        $("#Main_page_msg_date_sort_desc div").removeClass("arrow_selected");
    }
    else if (id == "Main_page_msg_date_sort_desc") {
        s_option = "5";
        $("#Main_page_msg_date_sort_acc div").removeClass("arrow_selected");
        $("#Main_page_msg_date_sort_desc div").addClass("arrow_selected");
    }
    else if (id == "Main_page_pro_state_sort_acc") {
        s_option = "6";
        $("#Main_page_pro_state_sort_acc div").addClass("arrow_selected");
        $("#Main_page_pro_state_sort_desc div").removeClass("arrow_selected");
    }
    else if (id == "Main_page_pro_state_sort_desc") {
        s_option = "7";
        $("#Main_page_pro_state_sort_acc div").removeClass("arrow_selected");
        $("#Main_page_pro_state_sort_desc div").addClass("arrow_selected");
    }
    else {
        alert("SORTING ERROR");
    }

    // Make ajax call to controller to update the table
    MainPageAjaxUpdate(     main_page_accumulate_page,        // Page numberth
                            s_option,                         // Sort option
                            document.getElementById("main_page_search_input").value);
    current_scrolldown_process = '*_*';
}

/**/
function ExpandedTabUpdate(process_name, msg_or_job, inside_page) {
    $("#" + process_name).addClass("table_row_selected");
    if (msg_or_job == 'Msg') {
        //show_message(inside_page, process_name);
        ProcessMsgUpdate(inside_page, process_name);
    }
    else {
        //show_job(inside_page, process_name);
        ProcessJobUpdate(inside_page, process_name);
    }
}

/**/
function ProcessMsgUpdate(page, process_name, show) {
    var id = process_name.split("_")[1];
    $.ajax({
        type: "POST",
        url: '/Home/ProcessMsgUpdate',
        data: { "page": page, "processID": id },
        success: function (data) {
            $('#fixed_info_content').html(data);
            var real_project_name = $("#" + process_name + " td").eq(0).html();
            if (real_project_name !== null) {
                $('#fixed_info_tab').html(real_project_name);
            }

            //store process name (with the id)
            $('#fixed_info_tab').data("process_name", process_name);

            if (show === true) {
                showFixedInfoTab();
            }
        },
        error: function (result) {
            alert(result);
        }
    });
}

/**/
function ProcessJobUpdate(page, process_name, show) {
    var id = process_name.split("_")[1];
    $.ajax({
        type: "POST",
        url: '/Home/ProcessJobUpdate',
        data: { "page": page, "processID": id },
        success: function (data) {
            $('#fixed_info_content').html(data);
            var real_project_name = $("#" + process_name + " td").eq(0).html();
            if (real_project_name !== null) {
                $('#fixed_info_tab').html(real_project_name);
            }

            //store process name (with the id)
            $('#fixed_info_tab').data("process_name", process_name);

            if (show === true) {
                showFixedInfoTab();
            }
        },
        error: function (result) {
            alert(result);
        }
    });
}

// Show Jobs, hide Messages on clicks
function show_job(page, process_name) {

    // Save info of the opened tab
    msg_or_job = 'Job';
    //inside_page = page;

    // Show jobs and hide messages
    //document.getElementById(process_name + 'message').style.display = 'none';
    //document.getElementById(process_name + 'JobBtn').style.display = 'none';
    //document.getElementById(process_name + 'job').style.display = 'block';
    //document.getElementById(process_name + 'MsgBtn').style.display = 'block';
    ProcessJobUpdate(page, process_name, true);
}

// Show Messages, hide Jobs on clicks
function show_message(page, process_name) {

    // Save info of the opened tab
    msg_or_job = 'Msg';
    //inside_page = page;

    // Show messages and hide jobs
    /*
    document.getElementById(process_name + 'job').style.display = 'none';
    document.getElementById(process_name + 'MsgBtn').style.display = 'none';
    document.getElementById(process_name + 'message').style.display = 'block';
    document.getElementById(process_name + 'JobBtn').style.display = 'block';
    */
    ProcessMsgUpdate(page, process_name, true);
}


///////////////////////////////////////// Message Inquiry Page ////////////////////////////////////////////////////////////

function Msg_Inquiry_setup(refresh_rate) {
    // Fix event.layerx and layery broken warning, work like a charm
    var all = $.event.props,
        len = all.length,
        res = [];
    while (len--) {
        var el = all[len];
        if (el != 'layerX' && el != 'layerY') res.push(el);
    }
    $.event.props = res;

    // Set up the initial stage of the message inquiry page
    MsgInquiryUpdate(
                            "1",    // current page number = 1
                            "9",    // sort_option = 5 which is sort by date descending
                            "0",    // msg_priority = 0 which will show every message with every possible priority
                            "",     // processname = "" which will search for all message
                            "0"     // SU_SD_msg = 0 which will not search for startup and shutdown message
                        );

    // Handle clicking page number 
    /*
    $(".msg_inquiry_page-number").live("click", function () {
        Msg_inquiry_collect_info(
                                        $(this).html(),
                                        msg_inquiry_current_sort,
                                        document.getElementById("process_prio_input").value,
                                        document.getElementById("process_name_input").value
                                    );
    });*/

    // Automatic update
    setInterval(function () {
        Msg_inquiry_collect_info(
                                    msg_inquiry_accumulate_page,
                                    msg_inquiry_current_sort,
                                    document.getElementById("process_prio_input").value,
                                    document.getElementById("process_name_input").value
                                );
    }, Refresh_Rate);

    // Use enter to send value in input box. Work for IE, Firefox and Chrome
    $('#process_name_input').keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            Msg_inquiry_collect_info(
                                            '1',                                                    
                                            msg_inquiry_current_sort,                               
                                            document.getElementById("process_prio_input").value,    
                                            document.getElementById("process_name_input").value    
                                         );
        }
        else {
            $.ajax({
                type: "POST",
                url: '/Home/AutoCompleteUpdate',
                data: { "search_string": "Fal" },
                success: function (data) {
                    var availableTags = data.split(",");
                    $("#process_name_input").autocomplete({
                        source: availableTags
                    });
                },
                error: function (result) {
                    alert(result);
                }
            });
        }

    });

    // Quick way to quit message detail window
    $('#popwindow').dblclick(function () {
        showpopup("-1", "");
    });
}

/**/
//function GoToMainPage(processName) {
//    window.location.href = "Index";
//    MainPageAjaxUpdate(1, 1, processName)
//}

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
            var header_title = "Process: " + pro_name + " | Message_id : " + msg_id;
            document.getElementById("message_modal").innerHTML = data;
            $("#message_modal").dialog({ "title": header_title });
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

function findPos(obj) {
    var curtop = 0;
    if (obj.offsetParent) {   
        curtop = obj.offsetTop
        while (obj == obj.offsetParent) {          
            curtop += obj.offsetTop
        }
    }
    return curtop;
}

/**/
function showpopup(msg_id, process_name, top_value) {
    GetFullDetailMessage(msg_id, process_name);
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
    if (chkboxname == "Msg_inq_msg_id_sort_acc") {
        s_option = "0";
        $("#Msg_inq_msg_id_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_msg_id_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_msg_id_sort_desc") {
        s_option = "1";
        $("#Msg_inq_msg_id_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_msg_id_sort_desc div").addClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_context_sort_acc") {
        s_option = "2";
        $("#Msg_inq_context_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_context_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_context_sort_desc") {
        s_option = "3";
        $("#Msg_inq_context_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_context_sort_desc div").addClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_name_sort_acc") {
        s_option = "4";
        $("#Msg_inq_name_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_name_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_name_sort_desc") {
        s_option = "5";
        $("#Msg_inq_name_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_name_sort_desc div").addClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_prio_sort_acc") {
        s_option = "6";
        $("#Msg_inq_prio_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_prio_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_prio_sort_desc") {
        s_option = "7";
        $("#Msg_inq_prio_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_prio_sort_desc div").addClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_date_sort_acc") {
        s_option = "8";
        $("#Msg_inq_date_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_date_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_date_sort_desc") {
        s_option = "9";
        $("#Msg_inq_date_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_date_sort_desc div").addClass("arrow_selected");
    } else {
        alert("SORTING ERROR");
    }

    MsgInquiryUpdate(msg_inquiry_accumulate_page, s_option, document.getElementById('process_prio_input').value, document.getElementById('process_name_input').value, (document.getElementById('SU_SD_Checkbox').checked + 0))
}


function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}