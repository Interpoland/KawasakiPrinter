import { Component, OnInit, Input } from '@angular/core';
/* import {AppComponent} from "../app/app.component" */

@Component({
  selector: 'app-table-row-data',
  templateUrl: './table-row-data.component.html',
  styleUrls: ['./table-row-data.component.css']
})
export class TableRowDataComponent implements OnInit {

  constructor() { }

  @Input() tagNames;
  @Input() dataPoint;
  data = []


  ngOnInit() {
  	console.log(this.dataPoint)
  	for (let tagName of this.tagNames){
  		if (this.dataPoint[tagName] !== undefined){
  			this.data.push(this.dataPoint[tagName])
  		}
  		else{
  			this.data.push("-")
  		}
  	}
  	console.log(this.data)	
  }

}
