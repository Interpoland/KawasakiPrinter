import { Component, OnInit, Input } from '@angular/core';
import * as CanvasJS from './canvasjs.min';
//var CanvasJS = require('./canvasjs.min');

@Component({
  selector: 'app-dynamic-line-graph',
  templateUrl: './dynamic-line-graph.component.html',
  styleUrls: ['./dynamic-line-graph.component.css']
})

export class DynamicLineGraphComponent implements OnInit {
  dynamicLine;
  dataPoints;

  constructor() { }

  ngOnInit() {
    this.dataPoints = [];
    for (var i = 0; i < 10; i++) {
      this.dataPoints.push({
        x: i, y: (Math.random())
      });
    }
    this.dynamicLine = new CanvasJS.Chart("chartContainerDynamicLine", {
      zoomEnabled: true,
      animationEnabled: true,
      exportEnabled: true,
      title: {
        text: "Temperature Line Demo - 10000 DataPoints"
      },
      data: [
        {
          type: "line",
          dataPoints: this.dataPoints
        }]
    });

    this.dynamicLine.render();
  }
}
