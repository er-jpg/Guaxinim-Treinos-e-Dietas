$("#cbTreinoCompleto").on('change', function () {
    if (this.checked) {

        var id = parseInt($("#diarioid").val);

        $.ajax({
            type: "POST",
            url: '/Diario/CompleteTreino',
            data: { id: id },
            beforeSend: function () {
                $('body').append('<div id="ajaxBusy"><img src="img/login.jpg"></div>');
            },
            success: function() {
                alert('wow!');
                $("#cbTreinoCompleto").css('background-color', 'orange');
            },
            error: function () {
                alert('não funciona man...');
            }
        });
    }
});