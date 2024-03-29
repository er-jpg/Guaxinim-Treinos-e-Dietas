﻿// Aqui é onde vive a gambiarra, nem sei o que tá mais escrito aqui.
// Write your JavaScript code.

(function ($) {
    $(function () {

        $('.sidenav').sidenav();
        $('.parallax').parallax();

    }); // end of document ready
})(jQuery); // end of jQuery name space


jQuery(document).ready(function ($) {
    // pra fazer as tabela fica monstrona do pantano
    $('*[data-href]').on('click', function () {
        window.location = $(this).data("href");
    });
    // pra encher linguiça dos create com markdown eh aqui
    var textoSemana = "Domingo\n...\n\nSegunda-feira\n...\n\nTerça-feira\n...\n\nQuarta-feira\n...\n\nQuinta-feira\n...\n\nSexta-feira\n...\n\nSábado\n...";
    $('#textareaMd').val(textoSemana);
    $('#textareaMd').height("+=10");
});

// muda a cor do fundo do diário
function mudaCor(x, _this) {
    x.style.backgroundColor = _this.checked ? '#55B97F' : '#E16E70';
}

// modal mágica pra deletar direto da lista

$(".delete-item").click(function () {
    Swal.fire({
        title: 'Tem certeza?',
        text: "Você removerá permanentemente o item.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, remova',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: $(this).data("route"),
                type: 'POST',
                data: { "id": $(this).data("id") },
                success: function () {
                    Swal.fire(
                        'Deletado!',
                        'O item foi removido com sucesso.',
                        'success'
                    )
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    })
});

// box shadow que funciona nos navegador
$(document).on("blur", "#textareaMd", function() {
    if (validateDays($('#textareaMd').val())) {
        $('#btnSave').removeAttr("disabled");
        $('#btnNext').removeAttr("disabled");
        // gambiarra bonita, olha que linda pqp porque navegadro não funciona bem
        $('#textareaMd').css('box-shadow', '0px 0px 0px 0px white');
    }
    else {
        $('#textareaMd').css('box-shadow', '0px 0px 25px 0px rgba(225,110,112,1)');
        $('#btnSave').attr("disabled", true);
        $('#btnNext').attr("disabled", true);
    }
});

// criar nova semana
$("#createWeek").click(function (e) {
    e.preventDefault();
    $.ajax({
        url: $(this).attr("href"), // comma here instead of semicolon   
        success: function () {
            //alert("Adicionado");  // or any other indication if you want to show
            //window.location.reload(true); // dá reload na hora do bagulho
            $('.erro').append("<p>Criando nova semana.</p>");
            setTimeout(location.reload.bind(location), 500); // reload com delay top
        }
    });
});