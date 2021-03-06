﻿var color = "";
var code = "";
var prix = "";
var nom = "";
var longueur = "";
var largeur = "";

$(function () {

    /*$('#carousel').elastislide({
        minItems: 1
    });*/

    $("#ChargePlan").change(function () {
        var idPlan = $(this).val();

        $.ajax({
            method: "POST",
            url: "/ServiceCommercial/Maison/GetPlan",
            contentType: "application/json",
            dataType: "html",
            data: "{'id': '" + idPlan + "'}"
        })
        .done(function (data) {

            $('#AffichePlan').children().css("display", "none");
            $("#AffichePlan").append(data);

        }).error(function (xhr,status, error) {
            console.log("FAIL", xhr);
            console.log("FAIL", status);
            console.log("FAIL", error);
        });
    });


    var x = 10;
    var y = 10;
    var etage = 1;

    $("#button_svg").click(function () {

        //Pour le cas ou plus d'un étage, rénitialisation de x et y, et ajout incrémetation du numéro étage indispensable
        x = 10;
        y = 10;
        var dessin = "<svg class='dessin_svg' id='etage_"+etage+"' style='width:500px;height:500px;'>";

        var taille = parseInt($("#larg").val()) + 1;
        largeur =  parseInt($("#larg").val());
        for (j = 0; j < taille ; j++) {
            for (i = 0; i < $("#long").val() ; i++) {
                var xAfter = x + 40;
                console.log('xAfter', xAfter);
                x = x + 1;
                console.log('x', x);
                dessin += "<a onclick='changeColor(lineLong" + i + j + etage +")' href='#'><line id='lineLong" + i + j + etage +"' x1='" + x + "'x2='" + xAfter + "' y1='" + y + "' y2='" + y + "' stroke='black' stroke-width='5' /></a>";
                //dessin += "<line x1='" + xBefore + "'x2='" + x + "' y1='" + yBefore + "' y2='" + yBefore + "' stroke='white' stroke-width='5' />";
                x = xAfter;
            }
            y = y + 40;
            x = 10;
        }


        x = 10;
        y = 10;
        taille = parseInt($("#long").val()) + 1;
        longueur = parseInt($("#long").val());

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

    $("#effacer").click(function () {

        color = $("#effacer").data("color");

        $("#effacer").removeAttr("data-values");

        console.log('COLOR1', color);
        makeCursor(color);

    });

    $(".module").click(function () {

        var mod = $(this).attr("id");

        console.log('$mod', mod);
        console.log('$mod', $("#"+mod));

        color = $("#" + mod).data("color");
        code = $("#" + mod).data("code");
        nom = $("#" + mod).data("nom");
        prix = $("#" + mod).data("prix");

        console.log('COLOR1', color);
        makeCursor(color);

    });

    function makeCursor(color) {

        var test = $("#AffichePlan");
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

        //console.log("PLAN COMPLET", $plan);

        //un étage contient une liste de PositionModule, contenus dans lesModules
        var etage = {};
        var lesModules = [];

        //un plan est composé de plusieurs étages, contenus dans lesEtages
        var planDTO = {};
        var lesEtages = [];


        var i = 0;
        //parcours chaque plan d'etage
        $plan.each(function (k, v) {
            var idEtage = $(v).attr("id");
            idEtage = idEtage.split("_");

            //On construit un étage
            $(v).children().each(function (key, value) {

                //console.log("Value", value);
                var currentData = $(value).children().data("values");
                var idPosition = $(value).children().data("idposition");

                var x1 = $(value).children().attr("x1");
                var x2 = $(value).children().attr("x2");
                var y1 = $(value).children().attr("y1");
                var y2 = $(value).children().attr("y2");
                var lineId = $(value).children().attr("id");

                if (typeof (currentData) != "undefined") {

                    if (idPosition === undefined) {
                        var positionModule = {
                            "module": { "id": currentData },
                            "etage": { "id": parseInt(idEtage[1]) },
                            "x1": x1,
                            "y1": y1,
                            "x2": x2,
                            "y2": y2,
                            "lineId": lineId
                        }
                    } else {
                        var positionModule = {
                            "id": idPosition,
                            "module": { "id": currentData },
                            "etage": { "id": parseInt(idEtage[1]) },
                            "x1": x1,
                            "y1": y1,
                            "x2": x2,
                            "y2": y2,
                            "lineId": lineId
                        }
                    }

                    //On a trouvé un nouveau module, on l'ajoute à ceux de cet étage
                    lesModules.push(positionModule);

                }
            });

            //On ajoute les modules à l'étage correspondant
            etage = {
                "id": parseInt(idEtage[1]),
                "lesModules": lesModules
            }

            console.log("-------------ETAGE--------------", etage);

            //On ajoute l'étage que l'on vient de finir
            lesEtages.push(JSON.parse(JSON.stringify(etage)));

            //On vide l'étage en cours pour le prochin tour de boucle
            etage = {};
            lesModules = [];

            //On incrémente pour passer à l'étage suivant
            i++;

        });

        //On ajoute notre liste d'étages au plan
        console.log("p_id", $('.p_id').text());
        if ($('.p_id').text() !== "0") {
            var idPlan = parseInt($('.p_id').text());
            planDTO = {
                "id": idPlan,
                "largeur": parseInt($('.largeur_edit').text()),
                "longueur": parseInt($('.longueur_edit').text()),
                "lesEtages": lesEtages
            }
        } else {
            planDTO = {
                "largeur": largeur,
                "longueur": longueur,
                "lesEtages": lesEtages
            }
        }

        console.log("PLLAN", planDTO)

        $.ajax({
            method: "POST",
            url: $(event.currentTarget).attr('href'),
            contentType: "application/json",
            dataType: "html",
            data: JSON.stringify(planDTO)
        })
        .done(function (data) {
            console.log('réussite');
            $('#AffichePlan').children().css("display", "none");
            $("#AffichePlan").append(data);
            fadeOutAlert();
        })
        .fail(function (data) {
            console.log("fail");
        });

    });

});


function changeColor(id) {
    console.log("clique color", id);

    var bool = false;
    var idTrait = $(id).attr("id");
    if (color === "#000000") {
        bool = true;
        
    } else {
        $(id).attr("data-values", code);
    }
    console.log("prix", prix)
    $(id).attr("data-prix", parseInt(prix));
    $(id).css({ "stroke": color });
    
    console.log("idTrait", idTrait);

    //Apparait dans liste représentant le devis
    console.log("Tbaleau parcours", $("#recap tbody"));

    $("#recap tbody tr").each(function (key, value) {
        console.log('TEST', value);


        console.log('--------Nom---------', nom);
        console.log("-----------------------------------------------------------------", typeof ($(value).attr("id")));
        if (typeof ($(value).attr("id")) != "undefined") {
            console.log('------------boucle----------', $(value).attr("id"));


             if ($(value).attr("id") === nom) {
                    
                trId = $(value).attr("id");
                console.log("trId", trId);
                console.log("aaa", "#" + trId + " :nth-child(2)")
                $td = $("#" + trId + " :nth-child(2)");

                console.log("PRIIIIIIIIIIIIIIX", prix);

                var prix_devis = $("#" + trId + " :nth-child(3)").text();
                console.log("prix_devis", $("#" + trId + " :nth-child(3)").text());

                if (bool === false) {
                    //prix_devis = parseInt(prix_devis) + parseInt(prix);
                    calculTotal(prix,"+");
                } else {
                    if (parseInt(prix_devis) > 0) { calculTotal(prix, "-"); }//prix_devis = parseInt(prix_devis) - parseInt(prix); }
                }
                
                console.log("PRIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIX", prix);

                console.log("---------$td--------", $("#" + trId + " :nth-child(2)"));

                $p = $($td).children();
                
                console.log("coucuouocuocuocuocuococ", $($td).attr("id"));

                tdId = $($td).attr("id");
                console.log("ppp", tdId);
                var quantite = $("#" + tdId + " :nth-child(2)").text();
                
                if (bool === false) {
                    quantite = parseInt(quantite) + 1;
                } else {
                    if (parseInt(quantite) === 0) { $("#" + trId).remove() }
                    if (parseInt(quantite) > 0) { quantite = parseInt(quantite) - 1; }
                    
                }

               
                $("#" + tdId + " :nth-child(2)").text(quantite);
                //$("#" + tdId + " :nth-child(2)").data("prix", prix);

             } else {

                 console.log("---------------------FIND---------------------",typeof($("#devis").find("#" + nom + "_quantite")));
                 var result = $("#devis").find("#" + nom + "_quantite");
                 if (typeof(result[0]) === "undefined") {
                     nomMod = nom;
                     console.log("---------------------DANS LE ELSE---------------------", nomMod);
                     quantite = 1;
                     calculTotal(prix,"+");
                     //prix = 0;
                     html = "<tr id='" + nomMod + "'><td>" + nomMod + "</td><td id='" + nomMod + "_quantite'><p>x</p><p class='quantite'>" + quantite + "</p></td><td>" + prix + "</td></tr>";
                     $("#recap tbody").append(html);
                 }
            }

        } 
    });

    console.log("-------SALOPE------", typeof ($("#recap tbody tr")))

    if($("#recap tbody tr").length <=  0) {
        nomMod = nom;
        console.log("LE PREMIER", nomMod);
        quantite = 1;
        calculTotal(prix,"+");
        //prix = 0;
        html = "<tr id='" + nomMod + "'><td>" + nomMod + "</td><td id='"+nomMod+"_quantite'><p>x</p><p class='quantite'>" + quantite + "</p></td><td>" + prix + "</td></tr>";
        $("#recap tbody").append(html);
    }

}

function fadeOutAlert() {
    $(".alert").fadeOut(6000);
}

function calculTotal(prix_devis, type) {

    console.log("ppppppppppppp", $("#prixTot").text())

    if(type === "+"){
        if ($("#prixTot").text() === "") {
            $("#prixTot").text(prix_devis);
        } else {
            var prixTotal = parseInt($("#prixTot").text());

            console.log("prix_total", prixTotal);
            console.log("prix_devis", prix_devis);

            prixTotal = parseInt(prixTotal) + parseInt(prix_devis);

            console.log("prix_total", prixTotal);

            $("#euros").text("€");
            $("#prixTot").text(prixTotal);
        }
    }else{
        if ($("#prixTot").text() === "") {
            $("#prixTot").text(prix_devis);
        } else {
            var prixTotal = parseInt($("#prixTot").text());

            console.log("prix_total", prixTotal);
            console.log("prix_devis", prix_devis);

            prixTotal = parseInt(prixTotal) - parseInt(prix_devis);

            console.log("prix_total", prixTotal);

            $("#euros").text("€");
            $("#prixTot").text(prixTotal);
        }
    }
}