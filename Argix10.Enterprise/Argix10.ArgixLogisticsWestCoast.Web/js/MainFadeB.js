$(document).ready(function(){    
    startAnimation();
    
    var fadeInSpeed = 2000;
    var fadeOutSpeed = 1000;
    var timeOutTime = 2000;

    function startAnimation() {
        setTimeout(fadeIn1, timeOutTime);

        function fadeIn1() {
            $('#divImg1').fadeIn(fadeInSpeed);
            setTimeout(fadeIn2, timeOutTime)
        }

        function fadeIn2() {
            $('#divImg2').fadeIn(fadeInSpeed);
            setTimeout(fadeIn4, timeOutTime)
        }

        //skip
        //function fadeIn3() {
        //    $('#divImg3').fadeIn(fadeInSpeed);
        //    setTimeout(fadeIn4, timeOutTime);
        //}

        function fadeIn4() {
            $('#divImg4').fadeIn(fadeInSpeed);
            setTimeout(fadeIn5, timeOutTime);
        }

        function fadeIn5() {
            $('#divImg5').fadeIn(fadeInSpeed);
            setTimeout(reload, timeOutTime);
        }

        //skip
        //function fadeIn6() {
        //    $('#divImg6').fadeIn(fadeInSpeed);
        //    setTimeout(reload, timeOutTime);
        //}

        function reload() {
            setTimeout(fadeOut1, fadeOutSpeed);
        }

        function fadeOut1(){
            $('#divImg1').fadeOut(fadeOutSpeed);
            setTimeout(fadeOut2, fadeOutSpeed);
        }

        function fadeOut2() {
            $('#divImg2').fadeOut(fadeOutSpeed);
            setTimeout(fadeOut4, fadeOutSpeed);
        }

        function fadeOut4() {
            $('#divImg4').fadeOut(fadeOutSpeed);
            setTimeout(fadeOut5, fadeOutSpeed);
        }

        function fadeOut5() {
            $('#divImg5').fadeOut(fadeOutSpeed);
            setTimeout(startAnimation, timeOutTime);
        }
    }
});

