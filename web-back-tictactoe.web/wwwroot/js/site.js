var interval;

function EmailConfirmation(email) {

    interval = setInterval(() => {
        CheckEmailConfirmationStatus(email);
    },
        1000);
}
function CheckEmailConfirmationStatus(email) {
    $.get("/CheckEmailConfirmationStatus?email=" + email,
        function (data) {
            if (data === "OK") {
                if (interval !== null) {
                    clearInterval();
                    window.location.href = "/GameInvitation?email=" + email;
                }
            } else {
                console.log(data);
            }
        });
}