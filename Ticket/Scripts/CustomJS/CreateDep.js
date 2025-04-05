$(document).ready(function () {
    $('#createForm').submit(function (e) {
        e.preventDefault();
        const formData = $(this).serialize();

        $.ajax({
            type: 'POST',
            url: '/Department/Create',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert('Department created successfully!');
                    window.location.href = '/Department/Index';
                } else {
                    alert('Error creating department.');
                }
            }
        });
    });
});