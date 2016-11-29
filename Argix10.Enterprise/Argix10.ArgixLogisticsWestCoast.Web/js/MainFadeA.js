$(document).ready(function(){  

    startAnimation();
    
    var fadeInSpeed = 2000;
    var fadeOutSpeed = 1000;
    var timeOutTime = 2000;

    function startAnimation() {
        setTimeout(animate1, timeOutTime);

        function animate1() {            
            $('#divImg1').fadeIn(fadeInSpeed);
            setTimeout(animate2, timeOutTime)
        }

        function animate2() {
            $('#divImg2').fadeIn(fadeInSpeed);
            setTimeout(animate3, timeOutTime)
        }

        //skip
        function animate3() {
            $('#divImg4').fadeIn(fadeInSpeed);
            setTimeout(animate4, timeOutTime);
        }

        function animate4() {
            $('#divImg7').fadeIn(fadeInSpeed);
            setTimeout(animate5, timeOutTime);
        }

 
        function animate5() {
            $('#divImg8').fadeIn(fadeInSpeed);
            setTimeout(animate6, 1000);
           
        }

function animate6() {
	  $('#divImg6').fadeIn(fadeInSpeed);
            $('#divImg5').fadeIn(fadeInSpeed);
           setTimeout(reload, timeOutTime);
        }

        //skip
       

        function reload() {
            $('#divImg2').fadeOut(fadeOutSpeed);
            $('#divImg7').fadeOut(fadeOutSpeed);
            $('#divImg4').fadeOut(fadeOutSpeed);
            $('#divImg8').fadeOut(fadeOutSpeed);
            $('#divImg5').fadeOut(fadeOutSpeed);
            //$('#divImg6').fadeOut(fadeOutSpeed);

            setTimeout(startAnimation, timeOutTime);
        }        
    }

    
    //$('#contentDiv').children().eq(0).show();
    //$('#contentDiv').children().eq(1).hide();
    //$('#contentDiv').children().eq(2).hide();

    //$("#divImg2").hide(500);
    //$("#divImg3").hide(500);
       
    //$('#contentDiv').animate({
    //    left: '0px'
    //}, 500, 'easeOutQuint');                              
   

});

