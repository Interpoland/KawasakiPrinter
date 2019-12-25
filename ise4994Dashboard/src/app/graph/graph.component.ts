import { Component, OnInit, Input} from '@angular/core';
import * as CanvasJS from './canvasjs.min';
//var CanvasJS = require('./canvasjs.min');

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {

     constructor() { }
     @Input() dataPoint;

     ngOnInit() {
		let chart = new CanvasJS.Chart("chartContainer", {
		animationEnabled: true,
		exportEnabled: true,
		title: {
			text: "Example Printer Status Chart"
		},
		data: [{
			type: "column",
			dataPoints: [
				{ y: 71, label: "Hyrel" },
				{ y: 55, label: "MP1" },
				{ y: 50, label: "MP2" },
				{ y: 65, label: "MP3" }
			]
		}]
	});

	chart.render();
    }
}
