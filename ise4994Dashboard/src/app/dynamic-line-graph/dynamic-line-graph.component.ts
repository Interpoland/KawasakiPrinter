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
    let dataPoints = [];
    let y = 0;
    for (var i = 0; i < 10000; i++) {
      y += Math.round(Math.random() * (-5));
      dataPoints.push({ y: y });
    }
    let dynamicLine = new CanvasJS.Chart("chartContainerDynamicLine", {
      zoomEnabled: true,
      animationEnabled: true,
      exportEnabled: true,
      title: {
        text: "Performance Demo - 10000 DataPoints"
      },
      subtitles: [{
        text: "Try Zooming and Panning"
      }],
      data: [
        {
          type: "line",
          dataPoints: dataPoints
        }]
    });

    dynamicLine.render();
  }
}
