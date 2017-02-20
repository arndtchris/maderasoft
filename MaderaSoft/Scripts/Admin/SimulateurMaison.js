var color = "";
var code = "";

$(function () {

    var x = 10;
    var y = 110;
    var dessin = "<svg id='dessin_svg' style='width:1000px;height:1000px;'>";

    $("#button_svg").click(function () {

        var taille = parseInt($("#larg").val()) + 1
        for (j = 0; j < taille ; j++) {
            for (i = 0; i < $("#long").val() ; i++) {
                var xAfter = x + 40;
                x = x + 1;
                dessin += "<a onclick='changeColor(lineLong" + i + j + ")' href='#'><line id='lineLong" + i + j + "' x1='" + x + "'x2='" + xAfter + "' y1='" + y + "' y2='" + y + "' stroke='black' stroke-width='5' /></a>";
                //dessin += "<line x1='" + xBefore + "'x2='" + x + "' y1='" + yBefore + "' y2='" + yBefore + "' stroke='white' stroke-width='5' />";
                x = xAfter;
            }
            y = y + 40;
            x = 10;
        }


        x = 10;
        y = 110;
        taille = parseInt($("#long").val()) + 1

        for (i = 0; i < taille ; i++) {
            for (j = 0; j < $("#larg").val() ; j++) {
                var yAfter = y + 40;
                y = y + 1;
                dessin += "<a onclick='changeColor(lineLarg" + j + i + ")' href='#'><line id='lineLarg" + j + i + "' x1='" + x + "'x2='" + x + "' y1='" + y + "' y2='" + yAfter + "' stroke='black' stroke-width='5' /></a>";
                //dessin += "<line x1='" + xBefore + "'x2='" + x + "' y1='" + yBefore + "' y2='" + yBefore + "' stroke='white' stroke-width='5' />";
                y = yAfter;
            }
            x = x + 40;
            y = 110;
        }

        dessin += "</svg>";
        $("#dessin").append(dessin);

    });


    // Par la suite, une fonction pour tout les composants récupérer dynamiquement
    $("#porte_svg").click(function () {

        var $por = $("#porte_svg");

        color = $por.data("color");
        code = $por.data("code");
        console.log('COLOR1', color);
        makeCursor(color);

    });

    $("#fenetre_svg").click(function () {

        var $fen = $("#fenetre_svg");

        color = $fen.data("color");
        code = $fen.data("code");
        console.log('COLOR1', color);
        makeCursor(color);

    });

    function makeCursor(color) {
        var test = $("#test");
        var cursor = document.createElement('canvas'),
            ctx = cursor.getContext('2d');

        cursor.width = 16;
        cursor.height = 16;

        console.log("COLOR", color);
        ctx.strokeStyle = color;

        ctx.lineWidth = 4;
        ctx.lineCap = 'round';

        ctx.moveTo(2, 12);
        ctx.lineTo(2, 2);
        ctx.lineTo(12, 2);
        ctx.moveTo(2, 2);
        ctx.lineTo(30, 30)
        ctx.stroke();

        test.css('cursor', 'url(' + cursor.toDataURL() + '), auto ');
    }

    $(".save_svg").click(function () {
        event.preventDefault();
        var $plan = $("#dessin_svg");
        
        var arrayComposant = [];
        var obj = {};

        $plan.children().each(function (key, value) {

            var currentData = $(value).children().data("values");
            if (typeof (currentData) != "undefined") {

                var result = $.grep(arrayComposant, function (e) { return e.id == currentData; });

                if (result.length == 0) {
                    // not found
                    var obj = {
                        "id": currentData,
                        "quantite": 1
                    };
                    arrayComposant.push(obj);

                } else {
                    // multiple items found
                    var index = result.length - 1
                    var quantite = arrayComposant[index].quantite;
                    quantite++;
                    arrayComposant[index].quantite = quantite;
                }
            }
        });

        console.log("MON TABLEAU", arrayComposant);

        var p = JSON.stringify($plan);

        var jsonObject = {
            "listComposants": arrayComposant
        };

        console.log("jsonObject", jsonObject);


        var jsonObj = {
            "Name": "Rami",
            "Roles": [{ "RoleName": "Admin", "Description": "Admin Role" }, { "RoleName": "User", "Description": "User Role" }]
        };

        console.log("jsonObject", jsonObj);

        $.ajax({
            method: "POST",
            url: $(event.currentTarget).attr('href'),
            contentType: "application/json",
            dataType:"json",
            data: JSON.stringify(jsonObject)
        });
        /*.done(function (data) {
            console.log('réussite');
        })
        .fail(function (data) {
            console.log("fail");
        });*/

    });

});


function changeColor(id) {
    console.log("clique color", id);
    $(id).css({ "stroke": color });
    $(id).attr("data-values",code);
}