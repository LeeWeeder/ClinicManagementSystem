$(document).ready(function () {
    $('#AppointmentType input').change(function () {
        if ($(this).val() == 'Doctor appointment') {
            $('#PhysiciansDropDownListContainer').removeClass('d-none').addClass('d-block');
        } else {
            $('#PhysiciansDropDownListContainer').removeClass('d-block').addClass('d-none');
        }
    });
});

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