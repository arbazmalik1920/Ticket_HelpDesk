$(document).ready(function () {
    $('#createForm').submit(function (event) {
        event.preventDefault(); // Prevent the default form submission

        var formData = $(this).serialize(); // Get form data

        $.ajax({
            url: '/SubCategory/Create', // URL to the controller action
            type: 'POST',
            data: formData,
            success: function (response) {
                console.log(response); // Log the response
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/SubCategory/Index'; // Redirect to SubCategory index page
                } else {
                    alert(response.message); // Show error message
                }
            },
            error: function (xhr, status, error) {
                console.error('AJAX Error:', error); // Log any AJAX errors
                alert('Error: ' + error);
            }
        });
    });
});