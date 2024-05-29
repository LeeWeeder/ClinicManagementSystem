$(document).ready(function () {
    $('#ReasonForVisitDropDownList').change(function () {
        if ($(this).val() == 'Other') {
            $('#OtherReasonForVisitContainer').removeClass('d-none').addClass('d-block');
        } else {
            $('#OtherReasonForVisitContainer').removeClass('d-block').addClass('d-none');
        }
    });

    $('#AppointmentTypeRadioButtonGroup').addClass('btn-group').attr('role', 'group');
    $('#AppointmentTypeRadioButtonGroup input').addClass('btn-check');
    $('#AppointmentTypeRadioButtonGroup label').addClass('btn btn-outline-primary');
});


var IsFirstTimeUserPrompt = new bootstrap.Modal(document.getElementById('IsFirstTimeUserPrompt'), {
    backdrop: 'static',
    keyboard: false
});

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))