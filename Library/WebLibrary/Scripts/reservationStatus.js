
function notReservedBooks() {

    $.ajax({
        url: "/Book/BooksAvailables",
        type: "GET",
        success: function (result) {
            $(".table").html(result).html($(".table"));
        },
        error: function (error) {
            return error;
        }
    });

}
function allBooks() {

    $.ajax({
        url: "/Book/Index",
        type: "GET",
        success: function (result) {

            $(".table").html(result).html($(".table"))

        },
        error: function (error) {
            return error;
        }
    });

}