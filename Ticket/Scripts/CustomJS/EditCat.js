$(document).ready(function () {
    $('#editCategoryForm').submit(function (e) {
        e.preventDefault();
        const formData = $(this).serialize();
        const categoryId = $('input[name="Id"]').val();

        $.ajax({
            url: '/Category/Edit/' + categoryId, // Use the full URL with category ID
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert('Category updated successfully!');
                    window.location.href = '/Category/Index'; // Redirect to Index page after success
                } else {
                    alert('Error updating category! ' + response.message);
                }
            },
            error: function () {
                alert('An error occurred while updating the category!');
            }
        });
    });
});






