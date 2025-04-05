$(document).ready(function () {
    $('#editForm').submit(function (e) {
        e.preventDefault();
        const formData = $(this).serialize();

        $.ajax({
            type: 'POST',
            url: '/Department/Edit',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert('Department updated successfully!');
                    window.location.href = '/Department/Index';
                } else {
                    alert('Error updating department.');
                }
            }
        });
    });
});