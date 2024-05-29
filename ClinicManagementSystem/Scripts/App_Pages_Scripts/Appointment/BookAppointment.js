$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

var IsFirstTimeUserPrompt = new bootstrap.Modal(document.getElementById('IsFirstTimeUserPrompt'), {
    backdrop: 'static',
    keyboard: false
});