$(function () {
    $('.editModal').bind('click', function (event) {
        event.preventDefault();
        $.ajax({
            method: "GET",
            url: $(event.currentTarget).attr('href'),
        })
        .done(function (data) {
            $("#editModal .modal-content").append(data);
            $.validator.unobtrusive.parse("form");
            $("#editModal").modal("show");
        })
        .fail(function (data) {
            console.log("fail");
        });
    });

    // Le contenu de la modal est chargé en Ajax 
    // lorsque celle-ci est refermée il faut la vider, sinon les différents contenus chargés s'enchaînent
    $(".modal").on("hidden.bs.modal", function () {
        $("#editModal .modal-content").html("");
    });

    $(".alert").fadeOut(6000);

    $.validator.unobtrusive.parse("form");

});