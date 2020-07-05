var categories = ["AI", "Biology", "Computer Science", "IoT"]; // taken from user interest || I will write all SQL queries neccessary at the bottom of javascript file (1)
var allCategories = ["Nursing", "Medicine", "Science", "COVID-19"]; // taken from videoTag (2)
var buzzwords = ["Trending", "Current Events", "Worldwide", "Featured"];

function leftColumn() {
  var outputHTML = "";
  outputHTML += "<table>";
  for (var i = 0; i < categories.length; i++) {
    outputHTML += "<tr>";
    outputHTML += "<td><h3>" + categories[i] + "</h3></td>";
    outputHTML += "<table>";
    for (var j = 0; j < 4; j++) {
      outputHTML += "<tr>";
      outputHTML +=
        "<td><img src='/img/sampleImg1.jpeg' asp-append-version='true' height='243px' width='432px'></td>"; //(3)
      outputHTML +=
        "<td><ul id = 'titleAndDesc'><li class='font-weight-bold'><h3> Title " +
        j +
        "</h3></li>"; //(4)
      outputHTML +=
        "<li><p class='font-weight-light'> Description " + j + "</p></li>"; //(5)
      outputHTML += "</ul></td>";
      outputHTML += "</tr>";
    }

    outputHTML += "</table>";
    outputHTML += "</tr>";
  }
  outputHTML += "<table>";
  document.getElementById("left").innerHTML = outputHTML;
}

function rightColumn() {
  var outputHTML = "";
  outputHTML += "<table>";
  for (var i = 0; i < buzzwords.length; i++) {
    outputHTML += "<tr>";
    outputHTML += "<td><h3>" + buzzwords[i] + "</h3></td>";
    outputHTML += "<table>";
    for (var j = 0; j < 4; j++) {
      outputHTML += "<tr>";
      outputHTML +=
        "<td><img src='/img/sampleImg2.jpg' asp-append-version='true' height='100px' width='183px'></td>";
      outputHTML +=
        "<td><ul id = 'titleAndDesc'><li class='font-weight-bold'><h3> Title " +
        j +
        "</h3></li>";
      outputHTML += "</ul></td>";
      outputHTML += "</tr>";
    }
    outputHTML += "</table>";
    outputHTML += "</tr>";
  }
  outputHTML += "<table>";
  document.getElementById("right").innerHTML = outputHTML;
}

leftColumn();
rightColumn();
/*

--------1--------
SELECT interestField
FROM User.currentUser
ORDER BY RAND()
LIMIT 4;
--------2--------

--------3--------

--------4--------

--------5--------

--------6--------
*/
