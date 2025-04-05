$(document).ready(function () {
    $('#editSubCategoryForm').submit(function (event) {
        event.preventDefault();

        var formData = $(this).serialize(); // Get form data

        $.ajax({
            url: '/SubCategory/Edit', // URL to the controller action
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/SubCategory/Index'; // Redirect to SubCategory index page
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('AJAX Error:', error);
                alert('Error: ' + error);
            }
        });
    });
});
