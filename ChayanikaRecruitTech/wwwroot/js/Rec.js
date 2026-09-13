function save() {
    var booksobj = {
        bookid: $('#bookid').val(),
        bookname: $('#bookname').val(),
        bfile: $('#bfile').val(),
    };
    $.ajax({
        url: "/Recruit/RecInsert",
        data: JSON.stringify(booksobj),
        dataType: "json",
        contentType: "application/json",
        type: "POST", 
        success: function (data) {

            if (data.statusType == "success") {
                alert(data.message);

                $('#bookid').val(0);
                $('#bookname').val("");

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


function doc() {
    var uploadfile = $('#bfile').get(0);
    var files = uploadfile.files;
    var filedata = new FormData();
    var bookcode = $('#bookname').val();
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

            if (Result=='Pass') {
                alert('Success');
 
            } 
            else {
                alert(Result);
            }

        }
    })
}