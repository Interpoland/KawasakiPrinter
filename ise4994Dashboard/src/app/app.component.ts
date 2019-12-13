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
 			value: 5
 		},
 		{
 			time: new Date(),
 			value: 6
 		},
 		{
 			time: new Date(),
 			value: 7
 		},
 		{
 			time: new Date(),
 			value: 8
 		},
 	]
 }
  title = 'ise4994Dashboard';
}
