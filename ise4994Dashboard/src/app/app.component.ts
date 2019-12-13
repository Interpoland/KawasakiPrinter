import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
 allData = {
 	tagName:"xPostion",
 	allData:[
 		{
 			time: new Date(),
 			xPosData: 5
 		},
 		{
 			time: new Date(),
 			xPosData: 6
 		},
 		{
 			time: new Date(),
 			xPosData: 7
 		},
 		{
 			time: new Date(),
 			xPosData: 8
 		},
 	]
 }
  title = 'ise4994Dashboard';
}
