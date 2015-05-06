


$(document).ready(function () {

    //alert('in doc ready');

    //resize the iFrame to fit content
    try {
        var h = $(".contentWrap").height();
        window.parent.document.getElementById("mapFrame").style.height = h + 30 + "px";
    } catch (e) {

    } 
    
    


});


