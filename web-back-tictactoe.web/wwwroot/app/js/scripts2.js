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