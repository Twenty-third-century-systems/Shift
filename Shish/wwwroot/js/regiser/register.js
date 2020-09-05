$(document).ready(function () {
    let countrySelect = $('#country');
    countrySelect.change(function () {
        if (countrySelect.val() !== 'ZWE') {
            $('#idMode').text('Passport ID number')
        } else {
            $('#idMode').text('National ID number')
        }

        $.ajax({
            type: 'Get',
            url: '/' + countrySelect.val() + '/Cities',
            success: function (data) {
                let citySelect = $('#city');
                citySelect.find('option').remove();
                citySelect.append('<option selected="selected"></option>');
                data.forEach((city, index) => {
                    let op = '<option value="' + city.id + '">' + city.name + '</option>';
                    citySelect.append(op);
                });
            },
        });
    });
});