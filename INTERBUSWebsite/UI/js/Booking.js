﻿var app = angular.module('myApp', ['ngStorage'])

var ctrl = app.controller('myCtrl', function ($scope, $http, $localStorage) {
    //$http.get('http://localhost:52800/api/Booking/commericialsite').then(function (response, req) {
    //    $scope.routes = response.data;

    // })
    var stat = 0;
    $scope.selectedSeats = new Array();
    $scope.selectedSeats.pssngr = new Array();
    $scope.selectedSeats.returnpssngr = new Array();

    $scope.GetAvailableServices = function () {
        $scope.srcId = $localStorage.srcId;
        $scope.destId = $localStorage.destId;
        $scope.way = $localStorage.waytype;

        //make a get request to database and show in a tabular form

        $http.get('http://localhost:52800/api/TicketBooking/GetAvailableServices?srcId=' + $scope.srcId + '&destId=' + $scope.destId).then(function (response, req) {
            $scope.services = response.data;
            $scope.showDiv = true;
        });

    }

    $scope.test = function () {
        var currstyle = document.getElementById('imgTd').style.display;
        document.getElementById('imgTd').style.display = (currstyle == "none") ? "table-cell" : "none";
    }
    $scope.totalseats = 0;
    $scope.count = 0;
    $scope.AddSeats = function (x) {
      
        if ($localStorage.waytype == 1 || ($localStorage.waytype == 2 && stat < 1)) {
            $scope.showDiv = true;
            var item = {
                "SeatId": x
        , "SeatNo": ""
         , "NoOfSeats": $scope.count
        , "Fname": ""
        , "Lname": ""
        , "Age": ""
        , "Sex": ""
        , "Identityproof": ""

            }
            $scope.selectedSeats.pssngr.push(item);

            $scope.count = $scope.selectedSeats.pssngr.length;
            $scope.totalseats = $scope.count;
            $scope.count = 0;
          //  $scope.subtotal = $scope.count * $scope.Booking.Cost;
        }
      
        else if ($localStorage.waytype == 2 && stat == 1) {
          
            var returndata = {
                "SeatId": x
      , "SeatNo": "123"
            }
            $scope.selectedSeats.returnpssngr.push(returndata);
          
            $scope.count = $scope.selectedSeats.returnpssngr.length;
        }
        // $scope.seats = $scope.count;

    }



   
    $scope.savedata = function (selectedSeats) {
        if ($localStorage.waytype == 1) {
            $scope.showDiv = true; 
            var book = { "No_Seats": "5", "cost": "1500", "JourneyType": "1", "passengersList": selectedSeats.pssngr, "Seatcost": "900" };
            //for(int i=0; i<selectedSeats.length; i++){}
            // passengersList: [{ "SeatId": "1", SeatNo: selectedSeats.SelectedSeatId, Fname: seat.pssngr.fname, Lname: seat.pssngr.lname, "Age": "30", "Sex": "0", "Identityproof": "adhar" }]

            //Fname2: Booking.psngr2.Fname2,  //Lname2: Booking.psngr2.Lname2, //Fname3: Booking.psngr3.Fname3, //Lname3: Booking.psngr3.Lname3, //"No_Seats": "5", "cost": "1500", "JourneyType": "1",
            //"passengersList": [{ "SeatId": "1","SeatNo": "A1", "Age": "30", "Sex": "0", "Identityproof": "adhar" },
            //                  ]
            //};
            /*
            {
      "Pnr_ID": 0,  "Pnr_No": null,  "No_Seats": 0,  "cost": 0,  "dateandtime": "0001-01-01T00:00:00",  "src": null,  "dest": null,  "vehicle_No": null,  "PassengerId": 0, "Fname": "srujan",
      "Lname": null,  "Age": 0,  "Sex": 0,  "Identityproof": null,  "TransactionId": 0,  "Transaction_Number": null,  "Amount": 0,  "Paymentmode": 0,  "Gateway_transId": null,
      "PSID": 0,  "SeatNo": null,  "passengersList": [    {  "SeatId": null,  "Fname": null, "Lname": null,   "Age": 30,   "Sex": 0,   "Identityproof": null   },
        {
          "SeatId": null,    "Fname": null,     "Lname": null,    "Age": 33,    "Sex": 0,   "Identityproof": null   }  ]
    }*/
          
            $localStorage.book = book;
            var req = { method: 'POST', url: 'http://localhost:52800/api/TicketBooking/SaveBookingDetails', data: book }

            $http(req).then(function (res) { window.location.href = "TicketPage.html"; });
         
        }
        else if ($localStorage.waytype == 2 && stat <= 1)
        {
         
            var book = { "No_Seats": "5", "cost": "1500", "JourneyType": "1", "passengersList": selectedSeats.pssngr, "ReturnpassengersList": selectedSeats.returnpssngr, "Seatcost": "900" };
            $localStorage.book = book;
            var req = { method: 'POST', url: 'http://localhost:52800/api/TicketBooking/SaveBookingDetails', data: book }
            // $http(req).then(function (res) { window.location.href = "TicketPage.html"; });
         
            stat++;
            if (stat == 1) {
              
                $http.get('http://localhost:52800/api/TicketBooking/GetAvailableServices?srcId=' + $scope.destId + '&destId=' + $scope.srcId).then(function (response, req) {
                    $scope.services = response.data;
                    $scope.showDiv = false;
                   
                })
            } else { window.location.href = "TicketPage.html"; }
        }
    }
    });