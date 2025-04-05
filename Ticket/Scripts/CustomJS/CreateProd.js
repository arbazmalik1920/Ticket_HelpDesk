$('#btnCreate').click(function () {
    $.ajax({
        type: 'POST',
        url: '/Product/Create',
        data: $('#createForm').serialize(),
        success: function (response) {
            if (response.success) {
                alert(response.message);
                window.location.href = '/Product/Index';
            } else {
                alert(response.message);
            }
        },
        error: function () {
            $('#message').html('<p style="color: red;">There was an error processing your request. Please try again later.</p>');
        }
    });
});