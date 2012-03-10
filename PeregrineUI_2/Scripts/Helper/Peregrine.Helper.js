/*
Author   : Chinh T Cao
           Anh T Nguyen
           Kyle Paulsen
Version  : 1.0.0
Date     : 12/29/2011
Copyright: Capstone Project Team Falcon 2011 All right reserved
*/

///////////////////////////////////////// Main Page ////////////////////////////////////////////////////////////

/*
function    : Main_page_setup
parameter   : s_process
what does this function do :
    Constructor for the main page.        
*/
function Main_page_setup(s_process) {
    // Fix event.layerx and layery broken warning, work like a charm
    // --->
    var all = $.event.props,
    len = all.length,
    res = [];
    while (len--) {
        var el = all[len];
        if (el != 'layerX' && el != 'layerY') res.push(el);
    }
    $.event.props = res;
    // <---

    // Initialize the default value for current_scrolldown_process
    current_scrolldown_process = '*_*';     

    var search_process = decodeURI(s_process);

    // Initial population of the table
    MainPageAjaxUpdate(1, 7, search_process);

    // Binding key press with search input box.
    // If user hit enter, value of the search input box will be sent to controller by ajax function call to 
    // get the query information using peregrine API
    $('#main_page_search_input').keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            MainPageAjaxUpdate(1, get_selected_sort_arrow_index(), document.getElementById("main_page_search_input").value);
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
                        alert("Main_page_setup_err");
                    }
                });
            }
        }
    });

    // Automatic refresh the page                
    setInterval('MainPageAjaxUpdate(main_page_accumulate_page, main_page_current_sort, document.getElementById("main_page_search_input").value)', Refresh_Rate);
}

/*
function    : Job_partial_page_setup
parameter   : 
what does this function do :
    Job partial page constructor            
*/
function Job_partial_page_setup() {
    $("#show_msg_btn").click(function () {
        show_message(1, $('#fixed_info_tab').data('process_name'));
    });
    $("#job_load_more").bind("click", { curPage: more_info_accumulate_page }, function (event) {
        show_job((event.data.curPage + 1), $('#fixed_info_tab').data('process_name'));
    });
}

/*
function    : Msg_partial_page_setup
parameter   : 
what does this function do :
    Message partial page constructor         
*/
function Msg_partial_page_setup() {
    $("#show_jobs_btn").click(function () {
        show_job(1, $('#fixed_info_tab').data('process_name'));
    });
    $("#msg_load_more").bind("click", { curPage: more_info_accumulate_page }, function (event) {
        show_message((event.data.curPage + 1), $('#fixed_info_tab').data('process_name'));
    });
}

/*
function    : refreshMainPage
parameter   : 
what does this function do :
    Call ajax to update asynchronized the content of processlist partial page in main page. 
    This function is used for automatic updating        
*/
function refreshMainPage() {
    MainPageAjaxUpdate(main_page_accumulate_page, main_page_current_sort, document.getElementById("main_page_search_input").value);
}

/*
function    : toggleFixedInfoTab
parameter   : 
what does this function do :
    This function is used to open or close the job-message detail tab when user click to the "Click a process to view info..." tab       
*/
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

/*
function    : showFixedInfoTab
parameter   : 
what does this function do :
    This function is called when the job-message info tab have opened and users want to change 
        from job view -> message view
        from message biew -> job view  
*/
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

/*
function    : get_selected_sort_arrow_index
parameter   : 
what does this function do :
    This function is used to get what kind of sort that users want to perform        
*/
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

/*
function    : toggleMoreInfo
parameter   : id
what does this function do :
    When user click into any data row in main page table, this function will be run.
    The new job-message tab detail will be filled up with the detail of the clicked process.  
*/
function toggleMoreInfo(id) {
    show_message(1, id);
    $(".table_row_selected").removeClass("table_row_selected");
    $("#" + id).addClass("table_row_selected");
}

/*
function    : ProcessList_partial_page_setup
parameter   : acc_page, sort_type
what does this function do :
       ProcessList partial page constructor 
*/
function ProcessList_partial_page_setup(acc_page, sort_type) {
    main_page_accumulate_page = acc_page;
    main_page_current_sort = sort_type;

    opened_row_id = $('#fixed_info_tab').data('process_name');
    if (opened_row_id != undefined) {
        ExpandedTabUpdate(opened_row_id, msg_or_job, more_info_accumulate_page);
    }
    $(".arrow_bg").bind("click", function (event) {
        Main_page_sorting($(event.currentTarget).attr("id"));
    });
    $("#main_page_load_more").bind("click", { curPage: main_page_accumulate_page, curSort: main_page_current_sort }, function (event) {
        MainPageAjaxUpdate((event.data.curPage + 1), event.data.curSort, document.getElementById('main_page_search_input').value);
    });         
}

/*
function    : MainPageAjaxUpdate
parameter   : page, sort_input, process_name
what does this function do :
    Update the content of ProcessList partial page.
    This function is call when users perform sorting, searching or the automatic updating kicks in
*/
function MainPageAjaxUpdate(page, sort_input, process_name) {
    //alert("Input : " + page);
    //alert("Input : " + sort_input);
    //alert("Input : " + process_name);
    $.ajax({
        type: "POST",
        url: '/Home/MainPageAjaxUpdate',
        data: { "page": page, "sort_input": sort_input, "process_name": process_name },
        success: function (data) {         
            $('.process-list').html(data);     
        },
        error: function (result) {
            alert("MainPageAjaxUpdate_err");
        }
    });
}

/*
function    : Main_page_sorting
parameter   : id
what does this function do :
    Find out about sort type (ascending or descending)  and sort columm.
    Then, calculate the s_option
    Then, call MainPageAjaxUpdate        
*/
function Main_page_sorting(id) {
    var s_option;

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

/*
function    : ExpandedTabUpdate
parameter   : process_name, msg_or_job, inside_page
what does this function do :
    If the job-message detail of a process is currently opened, when the page got updated, 
    this function will be call to open this tab again.      
*/
function ExpandedTabUpdate(process_name, msg_or_job, inside_page) {
    $("#" + process_name).addClass("table_row_selected");
    if (msg_or_job == 'Msg') {
        ProcessMsgUpdate(inside_page, process_name);
    }
    else {       
        ProcessJobUpdate(inside_page, process_name);
    }
}

/*
function    : ProcessMsgUpdate
parameter   : page, process_name, show
what does this function do :
    Update the message partial page inside the ProcessList partial page        
*/
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
            alert("process_msg_update_err");
        }
    });
}

/*
function    : ProcessJobUpdate
parameter   : page, process_name, show
what does this function do :
    Update the Job partial page inside the ProcessList partial page                
*/
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
            alert("ProcessJobUpdate_err");
        }
    });
}

/*
function    : show_job
parameter   : page, process_name
what does this function do :
    Show Jobs, hide Messages on clicks        
*/
function show_job(page, process_name) {
    // Save info of the opened tab
    msg_or_job = 'Job';
    ProcessJobUpdate(page, process_name, true);
}

/*
function    : show_message
parameter   : page, process_name
what does this function do :
    Show Messages, hide Jobs on clicks        
*/
function show_message(page, process_name) {
    // Save info of the opened tab
    msg_or_job = 'Msg';
    ProcessMsgUpdate(page, process_name, true);
}


///////////////////////////////////////// Message Inquiry Page ////////////////////////////////////////////////////////////

/*
function    : Msg_Inquiry_setup
parameter   : refresh_rate
what does this function do :
    MsgInquiry page constructor
*/
function Msg_Inquiry_setup(refresh_rate) {
    // Fix event.layerx and layery broken warning, work like a charm
    // -->
    var all = $.event.props,
        len = all.length,
        res = [];
    while (len--) {
        var el = all[len];
        if (el != 'layerX' && el != 'layerY') res.push(el);
    }
    $.event.props = res;
    // <--

    // Set up the initial stage of the message inquiry page
    MsgInquiryUpdate(
                            "1",    // current accumulative page
                            "7",    // sort_option
                            "-1",   // msg_priority
                            "",     // processname
                            "0"     // SU_SD_msg
                    );
    
    // Automatic update
    setInterval("updateMsgs()", Refresh_Rate);

    // Binding key press with search input box.
    // If user hit enter, value of the search input box will be sent to controller by ajax function call to 
    // get the query information using peregrine API
    $('#process_name_input').keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            MsgInquiryUpdate(1, msg_inquiry_current_sort, $('#process_prio_input').val(), $('#process_name_input').val(), (document.getElementById('SU_SD_Checkbox').checked + 0));
        } else {
            if (document.getElementById("process_name_input").value == "") {
                MsgInquiryUpdate(1, msg_inquiry_current_sort, $('#process_prio_input').val(), $('#process_name_input').val(), (document.getElementById('SU_SD_Checkbox').checked + 0));
            } else {
                $.ajax({
                    type: "POST",
                    url: '/Home/AutoCompleteUpdate',
                    data: { "search_string": document.getElementById("process_name_input").value },
                    success: function (data) {
                        var availableTags = data.split(",");
                        $("#process_name_input").autocomplete({
                            source: availableTags,
                            select: (function (event, ui) {
                                MsgInquiryUpdate(1, msg_inquiry_current_sort, $('#process_prio_input').val(), ui.item.value, (document.getElementById('SU_SD_Checkbox').checked + 0));
                            })
                        });
                    },
                    error: function (result) {
                        alert("Msg_Inquiry_setup_err");
                    }
                });
            }
        }

    });

    // Quick way to quit message detail window
    $('#popwindow').dblclick(function () {
        showpopup("-1", "");
    });
}

/*
function    : Msg_list_partial_page_setup
parameter   : 
what does this function do :
    MessageList partial page constructor        
*/
function Msg_list_partial_page_setup() {
    $(".arrow_bg").bind("click", function (event) {
        Msg_inquiry_sorting($(event.currentTarget).attr("id"));
    });
    $("#msg_load_more").bind("click", { curPage: msg_inquiry_accumulate_page, curSort: msg_inquiry_current_sort }, function (event) {
        MsgInquiryUpdate((event.data.curPage + 1), event.data.curSort, document.getElementById('process_prio_input').value, document.getElementById('process_name_input').value, (document.getElementById('SU_SD_Checkbox').checked + 0));
    });
}

/*
function    : updateMsgs
parameter   : 
what does this function do :
    This function will call MsgInquiryUpdate          
*/
function updateMsgs() {
    MsgInquiryUpdate(msg_inquiry_accumulate_page, msg_inquiry_current_sort, $('#process_prio_input').val(), $('#process_name_input').val(), (document.getElementById('SU_SD_Checkbox').checked + 0));
}

/*
function    : MsgInquiryUpdate
parameter   : page_number, sort_option, msg_priority, process_name, SU_SD_msg
what does this function do :
    Update the content of MessageList partial page.
    This function is call when users perform sorting, searching, choosing msg_priority and start-up-shut-down msg
    or the automatic updating kicks in        
*/
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
            $('div.message-list').html(data);
        },
        error: function (result) {
            alert("MsgInquiryUpdate_err");
        }
    });
}

/*
function    : GetFullDetailMessage
parameter   : msg_id, pro_name
what does this function do :
    This function is called when use want to see the full detail version of long messages       
*/
function GetFullDetailMessage(msg_id, pro_name) {
    $.ajax({
        type: "POST",
        url: '/Home/MsgInq_getfulldetail',
        data: { "msg_id": msg_id },
        dataType: "html",
        success: function (data) {
            var header_title = "Process: " + pro_name + " | Message_id : " + msg_id;
            $("#message_modal").html(data);
            $("#message_modal").dialog({ "title": header_title });
        },
        error: function (result) {
            alert("GetFullDetailMessage_err");
        }
    });
}

/*
function    : findPos
parameter   : obj
what does this function do :
    This function is used for pop-up window.
    Get current location of an object       
*/
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

/*
function    : showpopup
parameter   : msg_id, process_name, top_value
what does this function do :
    Open the long detail version of a message by calling GetFullDetailMessage           
*/
function showpopup(msg_id, process_name, top_value) {
    GetFullDetailMessage(msg_id, process_name);
}

/*
function    : Msg_inquiry_sorting
parameter   : chkboxname
what does this function do :
    Find out about sort type (ascending or descending)  and sort columm.
    Then, calculate the s_option
    Then, call MsgInquiryUpdate           
*/
function Msg_inquiry_sorting(chkboxname) {   
    var s_option;

    if (chkboxname == "Msg_inq_context_sort_acc") {
        s_option = "0";
        $("#Msg_inq_context_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_context_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_context_sort_desc") {
        s_option = "1";
        $("#Msg_inq_context_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_context_sort_desc div").addClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_name_sort_acc") {
        s_option = "2";
        $("#Msg_inq_name_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_name_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_name_sort_desc") {
        s_option = "3";
        $("#Msg_inq_name_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_name_sort_desc div").addClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_prio_sort_acc") {
        s_option = "4";
        $("#Msg_inq_prio_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_prio_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_prio_sort_desc") {
        s_option = "5";
        $("#Msg_inq_prio_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_prio_sort_desc div").addClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_date_sort_acc") {
        s_option = "6";
        $("#Msg_inq_date_sort_acc div").addClass("arrow_selected");
        $("#Msg_inq_date_sort_desc div").removeClass("arrow_selected");
    } else if (chkboxname == "Msg_inq_date_sort_desc") {
        s_option = "7";
        $("#Msg_inq_date_sort_acc div").removeClass("arrow_selected");
        $("#Msg_inq_date_sort_desc div").addClass("arrow_selected");
    } else {
        alert("SORTING ERROR");
    }

    MsgInquiryUpdate(msg_inquiry_accumulate_page, s_option, document.getElementById('process_prio_input').value, document.getElementById('process_name_input').value, (document.getElementById('SU_SD_Checkbox').checked + 0))
}