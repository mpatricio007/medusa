$(function ()
{
    // create the loading window and set autoOpen to false
    var div = $('<div id="loadingScreen"></div>').appendTo('body');
    div.css('background', 'url(../../Styles/img/loading2.gif) no-repeat 5px 8px');
    div.css('padding-left', '25px');        
    $("#loadingScreen").dialog({
        autoOpen: false, // set this to false so we can manually open it
        dialogClass: "loadingScreenWindow",
        closeOnEscape: false,
        draggable: false,
        width: 460,
        minHeight: 50,
        modal: true,
        buttons: {},
        resizable: false,
        open: function (event, ui)
        {
            // scrollbar fix for IE
            $('body').css('overflow', 'hidden');
            $(".loadingScreenWindow .ui-dialog-titlebar-close").css('display','none');
        },
        close: function ()
        {
            // reset overflow
            $('body').css('overflow', 'auto');
        }
    }); // end of dialog
});
function waitingDialog(waiting) { // I choose to allow my loading screen dialog to be customizable, you don't have to
	$("#loadingScreen").html(waiting.message && '' != waiting.message ? waiting.message : 'Aguarde...');
	$("#loadingScreen").dialog('option', 'title', waiting.title && '' != waiting.title ? waiting.title : 'Processando');
	$("#loadingScreen").dialog('open');
}
function closeWaitingDialog() {
	$("#loadingScreen").dialog('close');
}


$('<div id="dialog-message" title="Alerta"><p>sdjfask dhfjash fahsdkfj akjsldh fkajshdfkja hsdkjfh akjlsdhf aklj</p></div>').appendTo('body');
$("#dialog:ui-dialog").dialog("destroy");
$("#dialog-message").dialog({
    modal: true,
    buttons: { Ok: function () { $(this).dialog("close"); } },
    close: function () { $("body").css("overflow", "auto"); } 
    }); 