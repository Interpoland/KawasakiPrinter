import { Component, OnInit, Input } from '@angular/core';
import * as CanvasJS from './canvasjs.min';
//var CanvasJS = require('./canvasjs.min');

@Component({
  selector: 'app-dynamic-line-graph',
  templateUrl: './dynamic-line-graph.component.html',
  styleUrls: ['./dynamic-line-graph.component.css']
})
export class DynamicLineGraphComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    let lineDataPoints = [];
    let y = 0;
    for (var i = 0; i < 10000; i++) {
      y += Math.round(5 + Math.random() * (-5 - 5));
      lineDataPoints.push({ y: y });
    }
    let dynamicLineGraph = new CanvasJS.Chart("chartContainer", {
      zoomEnabled: true,
      animationEnabled: true,
      exportEnabled: true,
      title: {
        text: "Dynamic Line Graph Demo"
      },
      subtitles: [{
        text: "Can Zoom and Pan"
      }],
      dynamicLineData: [
        {
          type: "line",
          lineDataPoints: lineDataPoints
        }]
    });

    dynamicLineGraph.render();
  }
}
