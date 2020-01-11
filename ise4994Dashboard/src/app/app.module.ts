import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

/* Data table/graph components. */
import { GraphComponent } from './graph/graph.component';
import { TableRowDataComponent } from './table-row-data/table-row-data.component';
import { TableHeaderComponent } from './table-header/table-header.component';
import { DynamicLineGraphComponent } from './dynamic-line-graph/dynamic-line-graph.component';

/* Site page components. */
import { RouterModule, Routes } from '@angular/router';
import { DataComponent } from './data/data.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { PhysicalAssetsComponent } from './physical-assets/physical-assets.component';
import { JobsComponent } from './jobs/jobs.component';
import { HomeComponent } from './home/home.component';

/* Adding routes to other site pages. */

const appRoutes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'data',
    component: DataComponent
  },
  {
    path: 'equipment',
    component: EquipmentComponent
  },
  {
    path: 'physicalAssets',
    component: PhysicalAssetsComponent
  },
  {
    path: 'jobs',
    component: JobsComponent
  },
  /* Adding in default page path. */
  {
    path: '',
    redirectTo: "/home",
    pathMatch: "full"
  }
];

@NgModule({
  declarations: [
    AppComponent,
    GraphComponent,
    TableRowDataComponent,
    TableHeaderComponent,
    DynamicLineGraphComponent,
    DataComponent,
    EquipmentComponent,
    PhysicalAssetsComponent,
    JobsComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
