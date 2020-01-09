import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GraphComponent } from './graph/graph.component';
import { TableRowDataComponent } from './table-row-data/table-row-data.component';
import { TableHeaderComponent } from './table-header/table-header.component';
import { DynamicLineGraphComponent } from './dynamic-line-graph/dynamic-line-graph.component';

@NgModule({
  declarations: [
    AppComponent,
    GraphComponent,
    TableRowDataComponent,
    TableHeaderComponent,
    DynamicLineGraphComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
