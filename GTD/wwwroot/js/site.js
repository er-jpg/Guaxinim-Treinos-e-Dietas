// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// pra fazer as tabela fica monstrona do pantano
jQuery(document).ready(function ($) {
    $('*[data-href]').on('click', function () {
        window.location = $(this).data("href");
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