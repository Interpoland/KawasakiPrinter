import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.css']
})
export class DataComponent implements OnInit {

  constructor(private http: HttpClient) {
    /* Getting request to recieve data. */
    console.log("http://10.42.0.2:8080/api/data/extruderTemp?startTime=2019-12-15T11:00:00Z&endTime=2019-12-15T12:00:00Z")
    /* let listener = this.http.get("http://10.42.0.2:8080/api/data/extruderTemp?startTime=2019-12-15T11:00:00Z&endTime=2019-12-15T12:00:00Z")
    listener.subscribe((data)=>{console.log(data)}) */

  }

  ngOnInit() {

  }

  convertToNano(localDate) {
    let start = Date.now();
    start.setYear(1970);
    start.setMonth(1);
    start.setDate(1);
    start.setHours(0, 0, 0, 0);
    console.log(localDate-start)
    console.log(localDate)
    console.log(start)


  }

  sendRequest() {
    this.convertToNano(document.getElementById("startTime").value)
    console.log(document.getElementById("startTime").value)
    console.log(document.getElementById("endTime").value)
  }

}
