$(".none").click(function (e) {
    e.preventDefault();
       $('#divPartial').empty();
    $('#divPartial').val("");
    var redirecturl = $(this).attr('href');
    // alert(redirecturl);
    //  $("#pageval").val(redirecturl);
    //alert($("#pageval").val());
    $.ajax({
        url: redirecturl,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html'

    })
       .success(function (result) {
           $('#divPartial').empty();
           $('#divPartial').val("");
           $('#divPartial').show();
           //  alert(result);
           $('#divPartial').html(result);
       })
       .error(function (xhr, status) {
           // alert("2");
       });
});