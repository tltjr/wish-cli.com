$('#contact-submit').click(function (e) {
    e.preventDefault();
    $('.help-inline').hide();
    $('#boxes').removeClass('hidden');

    var hasError = false;
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    var email = $('#register').val();
    if (email == '') {
        $('.input').addClass('error');
        $('.help-inline').html('Email is Required');
        $('.help-inline').show();
        hasError = true;
    }
    else if (!emailReg.test(email)) {
        $('.input').addClass('error');
        $('.help-inline').html('Invalid Email');
        $('.help-inline').show();
        hasError = true;
    }
    if (!hasError) {

        $('.input').removeClass('error');

        var id = '#dialog';

        //Get the screen height and width
        var mheight = $(document).height();
        var mwidth = $(document).width();

        //Set heigth and width to mask to fill up the whole screen
        $('#mask').css({ 'width': mwidth, 'height': mheight });

        //transition effect		
        $('#mask').fadeIn(1000);
        $('#mask').fadeTo("slow", 0.8);

        var height = $(window).height();
        var width = $('#top').width();

        var scrollOffset = $(window).scrollTop();
        var top = height / 2 - ($(id).height() / 2) + scrollOffset;
        var left = width / 2 - $(id).width() / 2;

        //Set the popup window to center
        $(id).css('top', top);
        $(id).css('left', left);

        //transition effect
        $(id).fadeIn(2000);

        $("#header_text").html('Thanks! Want to save 15% AND get an early invitation?');
        $("#paragraph_text").html('Share our product using the links below.');

        //if close button is clicked
        $('.window .close').click(function (e) {
            //Cancel the link behavior
            e.preventDefault();

            $('#mask').hide();
            $('.window').hide();
        });

        //if mask is clicked
        $('#mask').click(function () {
            $(this).hide();
            $('.window').hide();
        });
    }

});
