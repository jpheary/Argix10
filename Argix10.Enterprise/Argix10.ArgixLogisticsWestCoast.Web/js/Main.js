$(document).ready(function(){    
    startAnimation();
    
    var showSpeed = 400;
    var timeOutTime = 600;

    function startAnimation() {
        setTimeout(animate1, timeOutTime);

        function animate1() {            
            $('#divImg1').show(showSpeed);            
            setTimeout(animate2, timeOutTime)
        }

        function animate2() {
            $('#divImg2').show(showSpeed);
            setTimeout(animate4, timeOutTime)
        }

        //skip
        function animate3() {
            $('#divImg3').show(showSpeed);
            setTimeout(animate4, timeOutTime);
        }

        function animate4() {
            $('#divImg4').show(showSpeed);
            setTimeout(animate5, timeOutTime);
        }

        function animate5() {
            $('#divImg5').show(showSpeed);
            //setTimeout(animate6, 1000);
            setTimeout(reload, timeOutTime);
        }

        //skip
        function animate6() {
            $('#divImg6').show(showSpeed);
            setTimeout(reload, timeOutTime);
        }

        function reload() {
            $('#divImg1').hide(showSpeed);
            $('#divImg2').hide(showSpeed);
            //$('#divImg3').hide(showSpeed);
            $('#divImg4').hide(showSpeed);
            $('#divImg5').hide(showSpeed);
            //$('#divImg6').hide(showSpeed);

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

