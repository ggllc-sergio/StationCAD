
// document ready
$(document).ready( function () {
        
    // attach jquery plugin to validate address
    $("input[group='email']").mailgun_validator({
        api_key: 'pubkey-98eda3cdea4b3bf74327832f679f6690',
        in_progress: validation_in_progress,
        success: validation_success,
        error: validation_error,
    });

});



// while the lookup is performing
function validation_in_progress() {
    $('#status').html("<img src='loading.gif' height='16'/>");
}



// if email successfull validated
function validation_success(data) {
    $('#status').html(get_suggestion_str(data['is_valid'], data['did_you_mean']));
}



// if email is invalid
function validation_error(error_message) {
    $('#status').html(error_message);
}



// suggest a valid email
function get_suggestion_str(is_valid, alternate) {
    if (is_valid) {
        var result = '<span class="success">Address is valid.</span>';
        if (alternate) {
            result += '<span class="warning"> (Though did you mean <em>' + alternate + '</em>?)</span>';
        }
        return result
    } else if (alternate) {
        return '<span class="warning">Did you mean <em>' + alternate + '</em>?</span>';
    } else {
        return '<span class="error">Address is invalid.</span>';
    }
}


