    var digits = $("div.cube") ;      
    var interval = 1000;
    var delay = 800;
    var digit1 = parseInt($(digits).eq(2).find(".f2 span")[0].innerHTML);      
    var digit2 = parseInt($(digits).eq(3).find(".f2 span")[0].innerHTML);             
    var digit3 = parseInt($(digits).eq(1).find(".f2 span")[0].innerHTML);             
    
  
    setInterval(function(){
      
        
        $(digits).eq(3).addClass("flip");                      
        setTimeout(function(){            
            $(digits).eq(3).find(".f1 span")[0].innerHTML = digit2;
            if(digit2 == 9){
              digit2 = -1;
            }
            $(digits).eq(3).find(".f2 span")[0].innerHTML = ++digit2;
            $(digits).eq(3).removeClass("flip");  
            
         }, interval/2);                                                            
     
      
        if(digit2 == 9){
            
          if(digit1 == 0){
            
            setTimeout(function(){
              $(digits).eq(1).addClass("flip");                      
               setTimeout(function(){            
                  $(digits).eq(1).find(".f1 span")[0].innerHTML = digit3;
                  if(digit3 == 5){
                    digit3 = -1;
                  }
                  $(digits).eq(1).find(".f2 span")[0].innerHTML = ++digit3;
                  $(digits).eq(1).removeClass("flip");              
               }, delay);             
               }, interval/2);
          }
          
          
            setTimeout(function(){   

              $(digits).eq(2).addClass("flip");                      
               setTimeout(function(){            
                  $(digits).eq(2).find(".f1 span")[0].innerHTML = digit1;
                  if(digit1 == 5){
                    digit1 = -1;
                  }
                  $(digits).eq(2).find(".f2 span")[0].innerHTML = ++digit1;
                  $(digits).eq(2).removeClass("flip");  

               }, interval/2);     

            }, delay); 
        }
      
      
      }, interval);