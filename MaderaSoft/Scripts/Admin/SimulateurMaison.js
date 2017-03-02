var color = "";
var code = "";

$(function () {

    var x = 10;
    var y = 10;
    var etage = 1;

    $("#button_svg").click(function () {

        //Pour le cas ou plus d'un étage, rénitialisation de x et y, et ajout incrémetation du numéro étage indispensable
        x = 10;
        y = 10;
        var dessin = "<svg class='dessin_svg' id='etage"+etage+"' style='width:200px;height:200px;'>";

        var taille = parseInt($("#larg").val()) + 1
        for (j = 0; j < taille ; j++) {
            for (i = 0; i < $("#long").val() ; i++) {
                var xAfter = x + 40;
                x = x + 1;
                dessin += "<a onclick='changeColor(lineLong" + i + j + etage +")' href='#'><line id='lineLong" + i + j + etage +"' x1='" + x + "'x2='" + xAfter + "' y1='" + y + "' y2='" + y + "' stroke='black' stroke-width='5' /></a>";
                //dessin += "<line x1='" + xBefore + "'x2='" + x + "' y1='" + yBefore + "' y2='" + yBefore + "' stroke='white' stroke-width='5' />";
                x = xAfter;
            }
            y = y + 40;
            x = 10;
        }


        x = 10;
        y = 10;
        taille = parseInt($("#long").val()) + 1

        for (i = 0; i < taille ; i++) {
            for (j = 0; j < $("#larg").val() ; j++) {
                var yAfter = y + 40;
                y = y + 1;
                dessin += "<a onclick='changeColor(lineLarg" + j + i + etage + ")' href='#'><line id='lineLarg" + j + i + etage + "' x1='" + x + "'x2='" + x + "' y1='" + y + "' y2='" + yAfter + "' stroke='black' stroke-width='5' /></a>";
                //dessin += "<line x1='" + xBefore + "'x2='" + x + "' y1='" + yBefore + "' y2='" + yBefore + "' stroke='white' stroke-width='5' />";
                y = yAfter;
            }
            x = x + 40;
            y = 10;
        }

        dessin += "</svg>";
        $("#dessin").append(dessin);

        etage = etage + 1

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

    $(".save_svg").click(function (event) {
        event.preventDefault();

        var $plan = $(".dessin_svg");
        
        console.log("PLAN COMPLET", $plan);

        var arrayPlans = [];
        var composantPlan = {};
        var etages = [];

        var i = 0;
        //parcours chaque plan d'etage
        $plan.each(function (k, v) {

            console.log("V", v);
            
            $(v).children().each(function (key, value) {
                
                console.log("Value", value);
                var currentData = $(value).children().data("values");
                var x1 = $(value).children().attr("x1");
                var x2 = $(value).children().attr("x2");
                var y1 = $(value).children().attr("y1");
                var y2 = $(value).children().attr("y2");
                var lineId = $(value).children().attr("id");
                console.log("currentData", currentData);
                console.log("x1", x1);
                console.log("x2", x2);
                console.log("y1", y1);
                console.log("y2", y2);
                console.log("lineId", lineId);
               
                if (typeof (currentData) != "undefined") {

                    var module = {
                        "id": currentData,
                        "x1": x1,
                        "y1": y1,
                        "x2": x2,
                        "y2": y2
                    }

                    console.log("---------i-------", i);

                    etages.push(module);
  
                }
            });

            console.log("-------------ETAGE--------------", etages);

            composantPlan[i] = etages;
            etages = [];
            i++;
            console.log("composantPlan", composantPlan);
            
        });
        arrayPlans.push(composantPlan);
        var jsonObject = {
            "listComposants": arrayPlans
        };

        console.log("jsonObject", jsonObject);

        $.ajax({
            method: "POST",
            url: $(event.currentTarget).attr('href'),
            contentType: "application/json",
            dataType:"json",
            data: JSON.stringify(jsonObject)
        })
        .done(function (data) {
            console.log('réussite');
        })
        .fail(function (data) {
            console.log("fail");
        });

    });

});


function changeColor(id) {
    console.log("clique color", id);
    $(id).css({ "stroke": color });
    $(id).attr("data-values",code);
}