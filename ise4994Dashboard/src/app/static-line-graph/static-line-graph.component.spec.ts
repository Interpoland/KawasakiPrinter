import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StaticLineGraphComponent } from './static-line-graph.component';

describe('StaticLineGraphComponent', () => {
  let component: StaticLineGraphComponent;
  let fixture: ComponentFixture<StaticLineGraphComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StaticLineGraphComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StaticLineGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
