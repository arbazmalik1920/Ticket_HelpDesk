$('#createForm').submit(function (e) {
    e.preventDefault();  // Prevent form submission and page refresh

    const formData = $(this).serialize();  // Serialize form data

    $.ajax({
        url: '/Category/Create',  // Your controller action URL
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                alert(response.message);  // Display success message
                window.location.href = '/Category/Index';  // Redirect to index page
            } else {
                alert(response.message);  // Display failure message
            }
        },
        error: function (xhr, status, error) {
            console.error("Status:", status);
            console.error("Error:", error);
            alert('An error occurred while creating the category.');
        }
    });
});