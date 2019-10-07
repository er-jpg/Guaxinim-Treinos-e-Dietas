// Aqui é onde vive a gambiarra, nem sei o que tá mais escrito aqui.
// Write your JavaScript code.

jQuery(document).ready(function ($) {
    // pra fazer as tabela fica monstrona do pantano
    $('*[data-href]').on('click', function () {
        window.location = $(this).data("href");
    });
    // pra encher linguiça dos create com markdown eh aqui
    var textoSemana = "Domingo\n...\n\nSegunda-feira\n...\n\nTerça-feira\n...\n\nQuarta-feira\n...\n\nQuinta-feira\n...\n\nSexta-feira\n...\n\nSábado\n..."
    $('#textareaMd').val(textoSemana);

    // magia da text area pra ela ir crescendo de tamanho conforme vão escrevendo
    $('#textareaMd').each(function () {
        this.setAttribute('style', 'height:' + (this.scrollHeight) + 'px;overflow-y:hidden;');
    }).on('input', function () {
        this.style.height = 'auto';
        this.style.height = (this.scrollHeight) + 'px';
    });

});

// muda a cor do fundo do diário
function mudaCor(x, _this) {
    x.style.backgroundColor = _this.checked ? '#55B97F' : '#E16E70';
}

// modal mágica pra deletar direto da lista
$(document).on("click", "#abre-mol", function () {
     var hiddenId = $(this).data('id'),
         hiddenAction = $(this).data('route'),
         hiddenName = $(this).data('name');
     $("#hiddenid").val( hiddenId );
     $("#hidden-name").val( hiddenName );
    document.getElementById('formModal').action = hiddenAction;
});

// agora que é vou sofrer
// TODO: colocar a sombra vermelha ou sem sombra!
$(document).on("blur", "#textareaMd", function() {
    if (validateDays($('#textareaMd').val())) {
        $('#btnSave').removeAttr("disabled");	
    }
    else {
        $('#btnSave').attr("disabled", true);        
    }
});