$(document).ready(function () {
   

    GetRolls();
    getEnquiryList();
    $('#College_Type').keypress(function (e) {
        var key = e.which;

        if (key == 13)  // the enter key code
        {
            var st = this.value;
            if (st == "") {

                alert("Please  Enter College Type");
                $('#College_Type').focus();
            }
            else {
                $('#Remarks').focus();
            }
            return false;
        }
    });


    $('#Remarks').keypress(function (e) {
        var key = e.which;

        if (key == 13)  // the enter key code
        {
            // var st = this.value;
            //if (st == 0) {

            //    alert("Please  Enter Account Name");
            //    $('#Account_Type').focus();
            //}
            //else {
            $('#btnCollegeType').focus();
            // }
            return false;
        }
    });

    $("#Remarks").on('keydown', function (event) {
        if (event.key == "Escape") {
            $('#College_Type').focus();
        }
    });
});


function login() {
    var data = {
        "userid": $("#username").val(),
        "password": $("#password").val()
    };
    $.ajax({
        url: "/User/validateuser",
        type: "POST",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            if (response.Success) {
                $.get("@Url.Action("Home", "User")", function (data) {
                    $('.container').html(data);
                });
            }
            else
                window.location.href = "@Url.Action("Login", "User")";
        },
        error: function () {
            console.log('Login Fail!!!');
        }
    });
}
function GetCourse() {

    $.ajax({
        url: "/MasterPolicy/GetCourseType",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        traditional: true,
        success: function (data) {
            var item = '';
            var coursetype = "<option value='0'>Select Course</option>";
            $.each(data.coursetype, function (i, item) {

                coursetype += "<option value=" + item.clsId + ">" + item.class_Name + "</option>";

            });
            $("#ClsId").html(coursetype).show();

        },
        error: function () {

        }
    });
}
function GetCategoryType() {
    debugger;
    $.ajax({
        url: "/MasterPolicy/GetCategoryType",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        traditional: true,
        success: function (data) {
            var item = '';
            var categoryType = "<option value='0'>Select Category</option>";
            $.each(data.categoryType, function (i, item) {

                categoryType += "<option value=" + item.castid + ">" + item.cast_Name + "</option>";

            });
            $("#Castid").html(categoryType).show();

        },
        error: function () {

        }
    });
}
function getEnquiryList() {
    var Srno = 0;
    //salert('recall');
    $('#tblEnquiry').dataTable().fnDestroy();
    $("#tblEnquiry").dataTable
        ({
            "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
            pagingType: 'full',
            pageLength: 20,
            language: {
                oPaginate: {
                    sNext: '<i class="fa fa-forward"></i>',
                    sPrevious: '<i class="fa fa-backward"></i>',
                    sFirst: '<i class="fa fa-step-backward"></i>',
                    sLast: '<i class="fa fa-step-forward"></i>'
                },
                "sSearch": "", sProcessing: "<img src='/Images/loader.gif'>"
            },
            "bStateSave": true,
            "processing": true, 
            searching: true,
            bFilter: false,
            bInfo: false,
            "bLengthChange": false,
            "ServerSide": true,
            "scrollY": "100px",
            "iDisplayLength": 10,
            "scrollCollapse": false,
            //"oLanguage": {
            //    sProcessing: "<img src='/Images/loader.gif'>", "sSearch": " "
            //},
            "searchPlaceholder": "search",
            "processing": true,
            "ServerSide": true,
            //"sPaginationType": "full_numbers",
            //"scrollY": "400px",
            "iDisplayLength": 10,
            "bAutoWidth": false,
            "searchable": true,
            "responsive": true,
            "scrollX": true,
            orderCellsTop: true,
            "dom": '<"pull-left""to"><"bottom"pli<"refimg">><"clear">',
            dom: 'Bfrtilp<"refimg">',
            "order": [[1, "desc"]],
            buttons: [
                {
                    extend: 'copy',
                    exportOptions: {

                        columns: [2, 3, 4, 5],

                    }
                },
                {
                    extend: 'csv',
                    exportOptions: {

                        columns: [2, 3, 4, 5],

                    }
                },
                {
                    extend: 'excel',
                    exportOptions: {

                        columns: [2, 3, 4, 5],

                    }
                },

                {
                    extend: 'print',
                    exportOptions: {

                        columns: [2, 3, 4, 5],

                    }
                },
                {
                    extend: 'pdf',
                    exportOptions: {

                        columns: [2, 3, 4, 5],

                    }
                },



            ],


            "ajax":
            {

                "url": "/User/GetAdminList",
                "type": "GET"
            },
            "columns":
                [

                    { "data": "Sl No" },
                    {

                        data: null, render: function (data, type, row) {
                            return '<a href="#popup1"  onclick="GetEditEmp(' + data.userRId + ')"> <i class="fa fa-pencil"></i></a>'
                        },
                        className: "center"

                    },
                    {
                        data: null, render: function (data, type, row) {

                            return '<a   onclick="DeletedEmp(' + data.userRId + ')"> <i class="fa fa-trash"></i></a>'
                        },
                        className: "center"

                    },
                    /* { "data": "UserRId" },*/
                    {
                        data: null, render: function (data, type, row) {
                            
                            return (data.isAccount == true) ? '<span  onclick="Accountstatus(' + data.userRId + ',' + data.isAccount + ')"> <i class="fa fa-toggle-on" style="color:green;font-size:20px;"></i></span>' : '<span  onclick="Accountstatus(' + data.userRId + ',' + data.isAccount + ')"> <i class="fa fa-toggle-on" style="color:red;font-size:20px;"></i></span>'


                        },
                        className: "center"

                    }, 
                    {
                        data: null, render: function (data, type, row) {
                          
                            return (data.isActive == true) ? '<span  onclick="statusAccount(' + data.userRId + ',' + data.isActive + ')"> <i class="fa fa-toggle-on" style="color:green;font-size:20px;"></i></span>' : '<span  onclick="statusAccount(' + data.userRId + ',' + data.isActive + ')"> <i class="fa fa-toggle-on" style="color:red;font-size:20px;"></i></span>'
                            
                           
                        },
                        className: "center"

                    }, 
                    { "data": "userName" },
                    { "data": "password" },
                    { "data": "createdBy" },
                    { "data": "createdDate" },
                  /*  { "data": "rollid" }, */
                    
                ],
            "columnDefs": [

                {
                    //"targets": [3],
                    //"visible": false,
                    //"searchable": false
                },
                {
                    "targets": 0,
                    data: null,
                    "render": function (data, type, full, meta) {

                        Srno = Srno + 1;
                        return Srno;
                    }
                },
                {
                    "targets": [0],
                    "bSortable": false,
                },
                { "sWidth": "3.2em", "aTargets": [0] },
                {
                    "targets": [1],
                    "bSortable": false,
                },
                { "sWidth": "3.2em", "aTargets": [1] },
                {
                    "targets": [2],
                    "bSortable": false,
                },
                { "sWidth": "3.2em", "aTargets": [2] },




            ],


            initComplete: function () {
                $("div.refimg").html('<a href="#" onclick="Refreshhh();"><img src="/images/icons/ref.png" /></a>');
            },


        });
}

function EnquirySaveData() {

    if (isValidationFirst() == true) {

        var EnquiryObj =
        {
            EnId: $('#EnId').val(),
            Name: $('#Name').val(),
            Email_id: $('#Email_id').val(),
            Mobile_no: $('#Mobile_no').val(),
            Alter_no: $('#Alter_no').val(),
            ClsId: $('#ClsId').val(),
            CastId: $('#Castid').val(),
            Enquiry_detail: $('#Enquiry_detail').val(),
            CreatedBy: 0,
            CreatedDate: null
        };

        $.ajax({
            url: "/CollegeOperation/EnquiryDetailsInserted",
            type: "POST",
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(EnquiryObj),
            success: function (data) {
                if (data.statusType == "success") {
                    alert(data.message);
                    window.location.href = "/CollegeOperation/Enquiry";

                }
                else if (data.statusType == "exists") {
                    alert(data.message);
                }
                else {
                    alert(data.message);
                }
            },
            error: function () {

            }
        });
    }
}

function GetEditEnquiry(id) {

    $.ajax({
        url: "/CollegeOperation/GetEditEnquiryData/" + id + "",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        traditional: true,
        success: function (data) {
            // debugger;
            /* $.each(data.datalist, function (i, item) {*/

            $('#EnId').val(data.datalist.enId);
            $('#Name').val(data.datalist.name);
            $('#Email_id').val(data.datalist.email_id);
            $('#Mobile_no').val(data.datalist.mobile_no);
            $('#Alter_no').val(data.datalist.alter_no);
            $('#ClsId').val(data.datalist.clsId);
            $('#CastId').val(data.datalist.castId);
            $('#Enquiry_detail').val(data.datalist.enquiry_detail);

            /*  });*/

        },
        error: function () {
            alert("Found Error");
        }
    });

}
function isValidationFirst() {

    var flage1 = false;
    var College_Type = $('#College_Type').val();
    if (College_Type == '')// validation function for the text box
    {
        alert(" Please Enter College Type")
        $('#College_Type').focus();
    }
    else {
        flage1 = true;

    }
    return flage1;
}

function DeletedEnquiry(id) {
    $.ajax({
        url: "/CollegeOperation/DeletedEnquiryData/" + id + "",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        traditional: true,
        success: function (data) {
            if (data.statusType == "success") {
                getCollegeList();
            }
            else {
                alert(data.message);
            }


        },
        error: function () {
            alert("Found Error");
        }
    });
}
//function GetCourseType() {

//    $.ajax({
//        url: "/MasterPolicy/GetCourseType",
//        type: "GET",
//        dataType: "json",
//        contentType: 'application/json',
//        traditional: true,
//        success: function (data) {
//            var item = '';
//            var coursetype = "<option value='0'>All Course Type</option>";
//            $.each(data.coursetype, function (i, item) {

//                coursetype += "<option value=" + item.clsId + ">" + item.class_Name + "</option>";

//            });
//            $("#courseType").html(coursetype).show();

//        },
//        error: function () {

//        }
//    });
//}

function Clear() {
    $('#Cltid').val(0);
    $('#College_Type').val('');
    $('#Remarks').val('');
}

function openbtn() {
    var divsToHide = document.getElementsByClassName("overlay"); //divsToHide is an array
    for (var i = 0; i < divsToHide.length; i++) {
        //  divsToHide[i].style.visibility = "hidden"; // or
        divsToHide[i].style.display = "block"; // depending on what you're doing
        divsToHide[i].style.visibility = "visible";
        divsToHide[i].style.opacity = "1"; 
    }
}

function closebtn() {
    var divsToHide = document.getElementsByClassName("overlay"); //divsToHide is an array
    for (var i = 0; i < divsToHide.length; i++) {
      //  divsToHide[i].style.visibility = "hidden"; // or
        divsToHide[i].style.display = "none"; // depending on what you're doing
        divsToHide[i].style.visibility = "hidden";
        divsToHide[i].style.opacity = "0"; 
    }
}

function empsave() {
   // var mail = $('#empEmail').val(); 
  //  var str = ".pdf";
  //  var filename = mail + str;
    //const myFileInput = document.querySelector('input[type="file"]');
  //  var filename = myFileInput.files[0].name;
 //   console.log(filename);
    var empobj = {
        empId: $('#empId').val(),
        empName: $('#empName').val(),
        empEmail: $('#empEmail').val(),
        empMobileNo: $('#empMobileNo').val(),
        empGender: $('#empGender').val(),
        empState: $('#empState').val(),
        empCity: $('#empCity').val(),
        empEmpTypes: $('#empEmpTypes').val(),
        empExpYears: $('#empExpYears').val(),
        empApplyFor: $('#empApplyFor').val(),
        empQualification: $('#empQualification').val(),
        empResume: $('#empMobileNo').val(),
    };
    $.ajax({
        url: "/Recruit/EmpInsert",
        data: JSON.stringify(empobj),
        dataType: "json",
        contentType: "application/json",
        type: "POST",
        success: function (data) {

            if (data.statusType == "success") {
                alert(data.message);

                $('#empId').val("0"),
                $('#empName').val(""),
                $('#empEmail').val(""),
                $('#empMobileNo').val(""),
                $('#empGender').val(""),
                $('#empState').val(""),
                $('#empCity').val(""),
                $('#empEmpTypes').val(""),
                $('#empExpYears').val(""),
                $('#empApplyFor').val(""),
                $('#empQualification').val(""),
                $('#empResume').val("")
                getEnquiryList();
                closebtn();

            }
            else if (data.statusType == "exists") {
                alert(data.message);
            }
            else {
                alert(data.message);
            }

        }
    });
}


function uplaodresume() {
    var uploadfile = $('#empResume').get(0);
    var files = uploadfile.files;
   // console.log(files);
    var filedata = new FormData(); 
    var bookcode = $('#empEmail').val();
    for (var i = 0; i < files.length; i++) {
        filedata.append(bookcode, files[i])
    }
    $.ajax({
        url: "/Recruit/Uploadfile",
        type: "POST",
        data: filedata,
        processData: false,
        contentType: false,
        success: function (Result) {

            if (Result == 'Pass') {
                alert('Your Resume has been Successfully Upload!');

            }
            else {
                alert(Result);
            }

        }
    })
}

function DownloadFile(fname) {
   console.log(fname);
    var fileName = "7302.pdf";
    $.ajax({
        url: "/Recruit/DownloadFile",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { fileName : fileName },
            type: "GET",
            success: function (r) {
                //Convert Base64 string to Byte Array.
                var bytes = Base64ToBytes(r.d);

                //Convert Byte Array to BLOB.
                var blob = new Blob([bytes], { type: "application/octetstream" });

                //Check the Browser type and download the File.
                var isIE = false || !!document.documentMode;
                if (isIE) {
                    window.navigator.msSaveBlob(blob, fileName);
                } else {
                    var url = window.URL || window.webkitURL;
                    link = url.createObjectURL(blob);
                    var a = $("<a />");
                    a.attr("download", fileName);
                    a.attr("href", link);
                    $("body").append(a);
                    a[0].click();
                    $("body").remove(a);
                }
            }
        });
}
function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};



function GetRolls() {

    $.ajax({
        url: "/User/GetRolls",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        traditional: true,
        success: function (data) {
            var item = '';
            console.log(data);
            var headtype = "<option value='0'>Select Roll Type</option>";
            $.each(data.headtype, function (i, item) {

                headtype += "<option value=" + item.rolltitle + ">" + item.rolltitle + "</option>";

            });
            $("#rollid").html(headtype).show();

        },
        error: function () {

        }
    });
} 

function NewAdmin() {
    var act;
    var lfckv = document.getElementById("isactive").checked; 
    if (lfckv == true) {
       act = 1;

    } else {
       act = 0;
    }
   
    var empobj = {
        UserRId: $('#UserRId').val(),
        UserName: $('#UserName').val(),
        Password: $('#Password').val(),
        Rollid: $('#rollid').val(),
        isActive: lfckv,
        CreatedBy: $('#rollid').val(),
        IsAccount: 1,
    };
    console.log(empobj);
    $.ajax({
        url: "/User/NewAdmin",
        data: JSON.stringify(empobj),
        dataType: "json",
        contentType: "application/json",
        type: "POST",
        success: function (data) {

            if (data.statusType == "success") {
                alert(data.message);
 
               // getEnquiryList();
                closebtn();

            }
            else if (data.statusType == "exists") {
                alert(data.message);
            }
            else {
                alert(data.message);
            }

        }
    });
}


function Accountstatus(id, status) {

    var st;
    if (status == false) {
        st = true;
    } else {
        st = false;
    }
   // alert(st);
    console.log(st);
        $.ajax({
            url: "/User/AccountStatus?UserRId=" + id + "&IsActive=" + st + "",
            type: "GET",
            dataType: "json",
            contentType: 'application/json',
            traditional: true,
            success: function (data) {
                if (data.statusType == "success") {
                   // alert(data.message);
                    getEnquiryList();
                }
                else if (data.statusType == "exists") {
                    alert(data.message);
                }
                else {
                    alert(data.message);
                }
            },
            error: function () {

            }
        });   
     
}
function statusAccount(id, statusacc) {
    var stac;
    if (statusacc == false) {
        stac = true;
    } else {
        stac = false;
    }

    $.ajax({
        url: "/User/ActiveAccount?UserRId=" + id + "&IsActive=" + stac + "",
        type: "GET",
        dataType: "json",
        contentType: 'application/json',
        traditional: true,
        success: function (data) {
            if (data.statusType == "success") {
               // alert(data.message);
                getEnquiryList();
            }
            else if (data.statusType == "exists") {
                alert(data.message);
            }
            else {
                alert(data.message);
            }
        },
        error: function () {

        }
    });

}
function DeletedEmp(id) {
    alert(id);
}