$(document).ready(function () {
    $('#AppointmentType input').change(function () {
       if ($(this).val() == 'Doctor appointment') {
           $('#PhysiciansDropDownListContainer').removeClass('d-none').addClass('d-block');
        } else {
           $('#PhysiciansDropDownListContainer').removeClass('d-block').addClass('d-none');
        }
    });
});


var IsFirstTimeUserPrompt = new bootstrap.Modal(document.getElementById('IsFirstTimeUserPrompt'), {
    backdrop: 'static',
    keyboard: false
});

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

function validateDate(sender, args) {
    var dateEntered = new Date(args.Value);
    if (dateEntered.getDay() == 6 || dateEntered.getDay() == 0) {
        args.IsValid = false;
    } else {
        args.IsValid = true;
    }
}

function validateTime(sender, args) {
    var time = new Date('1970-01-01T' + args.Value + 'Z');
    var hour = time.getUTCHours();

    if (hour >= 9 && hour <= 17) {
        args.IsValid = true;
    } else {
        args.IsValid = false;
    }
}