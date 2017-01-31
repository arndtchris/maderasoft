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

    $("#save_svg").click(function () {
        var $plan = $("#dessin").children();

        //Appel AJAX
        console.log("PLAN", $plan.children());

        var obj = {};

        $plan.children().each(function (key, value) {

           /* if ($.isEmptyObject(obj)) {
                obj[value] = 1
            } else {
                if ($.inArray(value, obj) > -1) {
                    obj[value] = i + 1
                }
            }*/

            console.log("KEY", key);
            console.log("VALUE", $(value).first().childNodes());
        });

        $.post("/Simulateur/Maison/Edit", function ($plan) {
            alert("Votre plan est bien enregistré, vous allez bientôt rentré en contact avec un commercial de notre équipe");
        })
        .fail(function () {
            alert("Une erreur est survenue");
        });

    });







});


function changeColor(id) {
    console.log("clique color", id);
    $(id).css({ "stroke": color });
    $(id).attr("values",code);
}