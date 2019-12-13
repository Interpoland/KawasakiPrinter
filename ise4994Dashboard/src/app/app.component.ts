import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
 allData = {
 	tagNames: ["xData", "yData", "zData"],
 	generalData:[
 		{
 			time: new Date(),
 			xData: 5
 		},
 		{
 			time: new Date(),
 			zData: 6
 		},
 		{
 			time: new Date(),
 			xData: 7,
         zData: 8
 		},
 		{
 			time: new Date(),
 			yData: 8
 		}
 	]
 }
  title = 'ise4994Dashboard';

  constructor() { }


}
