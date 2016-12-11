var PieChart = function(data, keyTitle, valueTitle){
var margin = {top:40,left:40,right:40,bottom:40};
width = 350;
height = 350;
radius = Math.min(width-100,height-100)/2;
var color = d3.scale.ordinal()
.range(["#e53517", "#6b486b", "#ffbb78","#7ab51d","#6b486b","#e53517","#7ab51d","#ff7f0e","#ffc400"]);
var arc = d3.svg.arc()  
         .outerRadius(radius -130)
         .innerRadius(radius - 10);
var arcOver = d3.svg.arc()  
.outerRadius(radius +50)
.innerRadius(0);
var svg = d3.select("#svgContent").append("svg")
          .attr("width",width)
          .attr("height",height)
          .append("g")
          .attr("transform","translate("+width/2+","+height/2+")");
div = d3.select("body")
.append("div") 
.attr("class", "tooltip");
var pie = d3.layout.pie()
          .sort(null)
          .value(function(d){return d[valueTitle];});
var g = svg.selectAll(".arc")
        .data(pie(data))
        .enter()
        .append("g")
        .attr("class","arc")
         .on("mousemove",function(d){
        	var mouseVal = d3.mouse(this);
        	div.style("display","none");
        	div
        	.html(keyTitle + ":"+ d.data[keyTitle]+"</br>"+ valueTitle + ":"+d.data[valueTitle])
            .style("left", (d3.event.pageX+12) + "px")
            .style("top", (d3.event.pageY-10) + "px")
            .style("opacity", 1)
            .style("display","block")
            .style("color", "white");
        })
        .on("mouseout",function(){div.html(" ").style("display","none");})
        .on("click",function(d){
        	if(d3.select(this).attr("transform") == null){
        	d3.select(this).attr("transform","translate(42,0)");
        	}else{
        		d3.select(this).attr("transform",null);
        	}
        });
        
        
		g.append("path")
		.attr("d",arc)
		.style("fill",function(d){return color(d.data[keyTitle]);});

		            svg.selectAll("text").data(pie(data)).enter()
		             .append("text")
		             .attr("class","label1")
                     .style("color", "white !important")
		             .attr("transform", function(d) {
		      		   var dist=radius+15;
		    		   var winkel=(d.startAngle+d.endAngle)/2;
		    		   var x=dist*Math.sin(winkel)-4;
		    		   var y=-dist*Math.cos(winkel)-4;
		    		   
		    		   return "translate(" + x + "," + y + ")";
		            })
		            .attr("dy", "0.35em")
		            .attr("text-anchor", "middle")
		            
		    	    .text(function(d){
		    	      return d.value;
		    	    });		
};