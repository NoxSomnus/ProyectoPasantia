$(document).ready(function () {
    $('#login').click(function () {
        window.location.href = '@Url.Action("MenuAdministrador", "Admin")';
    });
});