$(document).ready(function() {
		 
var output="";
$('#json-call-btn').click(function(){


	$.ajax({
		contentType: 'application/json; charset=utf-8',
		data: '',
		url: "../api/Service/1",
		type: "DELETE",
		crossDomain: true,
		headers: {'X-Requested-With': 'XMLHttpRequest'},
		success: function(data) {
//alert(data);
		//var x=JSON.parse(data);//convert string to json object
		
	
		  /*     $.each(data, function (index, item) {
	
            
   
output=output+"<h1 >"+item.Id+"<br/>"+item.Name+"</h1>";

  });*/
		    alert(data+" is deleted");
  //$('#json-my-response').html(output);
 
  }

        });//end of ajax request
		
		

		});
		



});


