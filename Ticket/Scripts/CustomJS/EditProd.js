

    $('#btnEdit').click(function () {
            const productData = {
            Id: $('#ProductId').val(),
            Name: $('#ProductName').val(),
            Price: $('#ProductPrice').val(),
            Description: $('#ProductDescription').val()
            };
        $.ajax({
            type: 'POST',
            url: '/Product/Edit',
            data: productData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    window.location.href = '/Product/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('An unexpected error occurred.');
            }
        });

    });


