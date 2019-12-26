import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicLineGraphComponent } from './dynamic-line-graph.component';

describe('DynamicLineGraphComponent', () => {
  let component: DynamicLineGraphComponent;
  let fixture: ComponentFixture<DynamicLineGraphComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DynamicLineGraphComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicLineGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
