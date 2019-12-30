import { Component, OnInit, Input } from '@angular/core';
import * as CanvasJS from './canvasjs.min';
//var CanvasJS = require('./canvasjs.min');

@Component({
  selector: 'app-static-line-graph',
  templateUrl: './static-line-graph.component.html',
  styleUrls: ['./static-line-graph.component.css']
})
export class StaticLineGraphComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    let fakeBarGraph = new CanvasJS.Chart("chartContainerFakeBarGraph", {
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
          { y: 50, label: "MP2" }
        ]
      }]
    });

    fakeBarGraph.render();
  }
}
