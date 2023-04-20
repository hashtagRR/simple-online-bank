<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="home.Master" CodeFile="Home.aspx.cs" Inherits="Pages_Home" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
<script src="https://www.amcharts.com/lib/3/pie.js"></script>
<link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
<script src="https://www.amcharts.com/lib/3/themes/light.js"></script>
  <script>
      var chart;
var legend;
var selected;

var types = [{
  type: "Restaurents",
  percent: 40,
  color: "#FAFA19"
}, {
  type: "Movies",
  percent: 25,
  color: "#1C65F0"
},{
  type: "Shoppiing",
  percent: 15,
  color: "#DF391B"
},{
  type: "Travel",
  percent: 10,
  color: "#DF1BBF"
},{
  type: "Transport",
  percent: 10,
  color: "#b0de09"
}];

function generateChartData() {
  var chartData = [];
  for (var i = 0; i < types.length; i++) {
    if (i == selected) {
      for (var x = 0; x < types[i].subs.length; x++) {
          chartData.push({  
             type: types[i].subs[x].type,
          percent: types[i].subs[x].percent,
          color: types[i].color,
          pulled: true
        });
      }
    } else {
        chartData.push({     
          type: types[i].type,
        percent: types[i].percent,
        color: types[i].color,
        id: i
      });
    }
  }
  return chartData;
}

AmCharts.makeChart("chartdiv", {
  "type": "pie",
    "dataProvider": generateChartData(),
  "labelText": "[[title]]: [[value]]",
  "balloonText": "[[title]]: [[value]]",
   "titleField": "type",
  "valueField": "percent",
  "outlineColor": "#FFFFFF",
  "outlineAlpha": 0.8,
  "outlineThickness": 2,
  "colorField": "color",
  "pulledField": "pulled",
  "listeners": [{
    "event": "clickSlice",
    "method": function(event) {
      var chart = event.chart;
      if (event.dataItem.dataContext.id != undefined) {
        selected = event.dataItem.dataContext.id;
      } else {
        selected = undefined;
      }
      chart.dataProvider = generateChartData();
      chart.validateData();
    }
  }]
});
  </script>

    <style>

#grid0{

    width:100%;
    height: auto;
       
}
.amcharts-chart-div > a{
    color:white;
}
#grid1{

    width:24%;
    height:auto;
    background: #c6c6c6; /* Old browsers */
background: -moz-linear-gradient(top, #c6c6c6 0%, #fcfcfc 100%); /* FF3.6-15 */
background: -webkit-linear-gradient(top, #c6c6c6 0%,#fcfcfc 100%); /* Chrome10-25,Safari5.1-6 */
background: linear-gradient(to bottom, #c6c6c6 0%,#fcfcfc 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#c6c6c6', endColorstr='#fcfcfc',GradientType=0 ); /* IE6-9 */
border-radius:10px 10px;
box-shadow:#0808083b 4px 5px 10px;
display:inline-grid;
}
#chartdiv {
    margin-top: -121px;
    margin-left: 23px;
  width: 100%;
  height: 500px;
}	
#grid2{

    width:67%;
    height:auto;
        background: #c6c6c6; /* Old browsers */
background: -moz-linear-gradient(top, #c6c6c6 0%, #fcfcfc 100%); /* FF3.6-15 */
background: -webkit-linear-gradient(top, #c6c6c6 0%,#fcfcfc 100%); /* Chrome10-25,Safari5.1-6 */
background: linear-gradient(to bottom, #c6c6c6 0%,#fcfcfc 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#c6c6c6', endColorstr='#fcfcfc',GradientType=0 ); /* IE6-9 */
border-radius:10px 10px;
box-shadow:#0808083b 4px 5px 10px;
display:inline-grid;

}
#space{
    width:20px;
    height:auto;
    display:inline-block

}
</style>

  

    <div class="row" style="margin-top:20px;">
  <div class="col-md-3" id="grid1">
 <h2 style="text-align:center;">Account Summery</h2>
            <table class="table table-dark">
  <tbody>
    <tr>
      <th scope="row"><span class="glyphicon glyphicon-chevron-right"></span></th>
      <td>Cash</td>
      <td>LKR</td>
      <td style="text-align:right;">25,000.00</td>
    </tr>
    <tr>
      <th scope="row"><span class="glyphicon glyphicon-chevron-right"></span></th>
      <td>Credit Card</td>
      <td>LKR</td>
      <td style="text-align:right;">20,000.00</td>
    </tr>
    <tr>
      <th scope="row"><span class="glyphicon glyphicon-chevron-right"></span></th>
      <td>Loans</td>
      <td>LKR</td>
      <td style="text-align:right;">5,000.00</td>
    </tr>
      <tr>
      <th scope="row"><span class="glyphicon glyphicon-chevron-right"></span></th>
      <td>Investment</td>
      <td>LKR</td>
      <td style="text-align:right;">10,000.00</td>
    </tr>
 
  </tbody>
</table>
         
       

  </div>
  <div class="col-md-1"></div>

  <div class="col-md-8" id="grid2">
      <div class="row">
          
      <div class="col-md-3">
           <table class="table table-dark">
             <tbody>
    <tr>
      <th scope="row"><span class="glyphicon glyphicon-chevron-right"></span></th>
      <td style="font-size:15px;">Your Balance</td>
      <td>LKR</td>
      <td style="text-align:right; font-size:20px;">
          <asp:Label ID="lbl_income" runat="server" Text=""></asp:Label>.00</td>
    </tr>
    <tr>
      <th scope="row"><span class="glyphicon glyphicon-chevron-right"></span></th>
      <td style="font-size:15px;">Your Spent</td>
      <td>LKR</td>
      <td style="text-align:right; font-size:20px;">
          <asp:Label ID="lbl_spent" runat="server" Text=""></asp:Label>.00</td>
    </tr>
    <tr>
      <th scope="row"><span class="glyphicon glyphicon-chevron-right"></span></th>
      <td style="font-size:15px;">Last Month Spent</td>
      <td>LKR</td>
      <td style="text-align:right; font-size:20px; ">5,000.00</td>
    </tr>
                 </tbody>
                </table>
      </div>
      <div class="col-md-9">        
   
         
         <div id="chartdiv"></div>			   
     
      

      </div>
      <div class="col-md-1"></div>
      <div class="col-md-3">

      </div>

      </div>
         
  </div>
             
</div>



</asp:Content>


