$(document).ready(function () {
    $('#btnLogin').click(function (e) {
        e.preventDefault();

        const username = $('#username').val().trim();
        const password = $('#password').val().trim();

        if (!username || !password) {
            alert('Please enter both username and password.');
            return;
        }

        // AJAX
        $.ajax({
            url: '/Account/ValidateLogin',
            type: 'POST',
            data: { username: username, password: password },
            success: function (response) {
                if (response.isValid) {
                    alert('Login successful!');
                    window.location.href = '/Home/Index';
                } else {
                    alert('Invalid username or password.');
                }
            },
            error: function () {
                alert('An error occurred while processing your request.');
            }
        });
    });
  
});